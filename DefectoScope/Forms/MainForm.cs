using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Kogerent;
using Timer = System.Timers.Timer;

namespace DefectoScope
{
    public partial class MainForm : Form
    {
        #region Конструктор

        public MainForm()
        {

#pragma warning disable 219
            #region Данные по валу

            //Диаметр вала в миллиметрах
            const double rollerDiameter = 790;
            // Длина вала в миллиметрах
            const double rollerLength = rollerDiameter * Math.PI;
            // Количество стробов на 1 оборот вала
            const int rollerStrobes = 10000;
            // Прошедших мм вала за 1 строб
            const float rollerStrobeEveryNMm = (float)(rollerLength / rollerStrobes);
            // Максимальная скорость вала (мм/с)
            const double rollerMaxSpeed = 380 * 1000 / 60.0f;
            // Реальная максимальная частота стробов с данным валом
            const double rollerRealStrobesFreq = rollerMaxSpeed / rollerStrobeEveryNMm;
            // Максимальная частота стробов при которой контроллер еще справляется (нет пропусков)
            const int controllerMaxStrobesFreq = 850;
            // Частота делителя, чтобы контроллер справлялся
            const int encoderDivider = (int)(rollerRealStrobesFreq / controllerMaxStrobesFreq) + 1;
            // Прошедших мм вала за 1 строб с учетом делителя
            var strobeEveryNMm = rollerStrobeEveryNMm * encoderDivider;
            #endregion
#pragma warning restore 219

            //var s = File.ReadAllLines("C:\\cab.txt");
            //var i = s.Select(byte.Parse).ToArray();

            //var sma = KUtils.SimpleMovingAverage(i, 100);

            //File.WriteAllLines("C:\\cab2.txt", sma.Select(x => x.ToString()));

            InitializeComponent();

            //Создаем таймеры
            _tAutoShift = new Timer(1000);
            _tBuffer = new Timer(10);
            _tDefectsMap = new Timer(100) { SynchronizingObject = this };
            _tOpcClient = new Timer(1000);
            _tEncoder = new Timer(500);
            _tShowSystemOk = new Timer(1000) { SynchronizingObject = this };
            //_tDeleteOldFiles = new Timer(10000);
            _tDeleteOldFiles = new Timer(1800000);
            _tAutoCreateReport = new Timer(60000);

            BwReInit();

            G.Defects.AddedDefect += AddRowInTable;
            G.Defects.RemovedDefect += RemoveRowFromTable;
        }

        /// <summary>
        /// Инициализация в другом потоке с ожиданием
        /// </summary>
        /// <param name="initSensors"></param>
        public void BwReInit(bool initSensors = true)
        {
            //Начинаем инициализацию системы
            Enabled = false;
            var loadForm = new LoadForm();
            loadForm.Show();

            //Запускаем зависающую инициализацию в другом потоке с ожиданием
            var bwInitialization = new BackgroundWorker();
            bwInitialization.DoWork += bwInitialization_DoWork;
            bwInitialization.RunWorkerCompleted += bwInitialization_RunWorkerCompleted;
            bwInitialization.RunWorkerAsync(initSensors);
            while (bwInitialization.IsBusy) Application.DoEvents();
            bwInitialization.Dispose();

            loadForm.Close();
            Enabled = true;

            G.IsAuto = true;
            var launched = G.Launched;
            bStop.PerformClick();
            if (G.IsOk && launched && G.NeedWorkAfterAutoShift)
                bStart.PerformClick();
            G.IsAuto = false;

            Activate();
        }

        #endregion

        #region Поля

        /// <summary>
        /// Таймер по работе с авто-сменой
        /// </summary>
        private readonly Timer _tAutoShift;

        /// <summary>
        /// Таймер по работе с буфером
        /// </summary>
        private readonly Timer _tBuffer;

        /// <summary>
        ///     Таймер по обновлению карты дефектов
        /// </summary>
        private readonly Timer _tDefectsMap;

        /// <summary>
        ///     Таймер по обновлению состояний с OPC сервера
        /// </summary>
        private readonly Timer _tOpcClient;

        /// <summary>
        ///     Таймер по обновлению состояния энкодера
        /// </summary>
        private readonly Timer _tEncoder;

        /// <summary>
        ///     Таймер по обновлению состояния системы
        /// </summary>
        private readonly Timer _tShowSystemOk;

        /// <summary>
        ///     Таймер по удалению старых файлов
        /// </summary>
        private readonly Timer _tDeleteOldFiles;

        /// <summary>
        /// Таймер по автоматическому созданию отчетов
        /// </summary>
        private readonly Timer _tAutoCreateReport;

        #endregion

        /// <summary>
        /// Переинициализация всей системы
        /// </summary>
        public void ReInit(bool initSensors = true)
        {
            //ПОДГОТОВКА К ИНИЦИАЛИЗАЦИИ
            G.Logger.Debug($"{nameof(MainForm)}: Подготовка к переинициализации системы...");

            //Закрываем динамичные окна
            this.InvokeIfRequired(() =>
            {
                ErrorF?.Close();
                AboutProgramF?.Close();
                ManualModeF?.Close();
                StatisticF?.Close();
                TimeF?.Close();
                ControllerF?.Close();
                ProfileBufferF?.Close();
                CalibrationF?.Close();
                LoadFrameF?.Close();
            });

            G.Logger.Debug($"{nameof(MainForm)}: Второстепенные окна закрыты...");

            //Останавливаем работу входного буфера
            G.NeedWorkInputBuffer = false;

            //Сбрасываем сигнал сброса тревоги
            G.StopAlarm = false;

            //Останавливаем таймеры
            _tAutoShift.Elapsed -= tAutoShift_Tick;
            _tBuffer.Elapsed -= tBuffer_Tick;
            _tDefectsMap.Elapsed -= tDefectsMap_Tick;
            _tOpcClient.Elapsed -= tOpcClient_Tick;
            _tEncoder.Elapsed -= tEncoder_Tick;
            _tShowSystemOk.Elapsed -= tShowSystemOk_Tick;
            _tDeleteOldFiles.Elapsed -= tDeleteOldFiles_Tick;
            _tAutoCreateReport.Elapsed -= tAutoCreateReport_Tick;

            _tAutoShift.Enabled = false;
            _tBuffer.Enabled = false;
            _tDefectsMap.Enabled = false;
            _tOpcClient.Enabled = false;
            _tEncoder.Enabled = false;
            _tShowSystemOk.Enabled = false;
            _tDeleteOldFiles.Enabled = false;
            _tAutoCreateReport.Enabled = false;

            G.Logger.Debug($"{nameof(MainForm)}: Таймеры остановлены...");

            //Отвязываем события
            if (G.ProfileBuffer != null)
                G.ProfileBuffer.AddedProfile -= SwiftAndCheckRealDefects;
            cbId.SelectedIndexChanged -= CbId_SelectedIndexChanged;

            G.Logger.Debug($"{nameof(MainForm)}: События отвязаны...");

            //Очищаем старые объекты
            if (G.OpcClient != null) G.OpcClient.Dispose();
            if (G.SyncController != null) G.SyncController.Dispose();
            if (G.SqlClient != null) G.SqlClient.Dispose();

            G.Logger.Debug($"{nameof(MainForm)}: Объекты удалены...");

            G.Defects.Clear();
            G.PotentialDefects.Clear();

            G.Logger.Debug($"{nameof(MainForm)}: Списки дефектов очищены...");

            Application.DoEvents();

            G.Logger.Debug($"{nameof(MainForm)}: Переинициализация системы...");
            //ИНИЦИАЛИЗАЦИЯ

            //Считываем версию библиотеки SensorE.dll
            VersionDll();

            G.Logger.Debug($"{nameof(MainForm)}: Связь с SensorE.dll установлена...");

            //Загружаем настройки
            UtilsSettings.LoadConfigSettings();
            UtilsSettings.LoadSystemSettings();
            if (initSensors) UtilsSettings.LoadSensorSettings();
            UtilsSettings.LoadExcludedZoneSettings();
            UtilsSettings.LoadTypeDefectSettings();
            UtilsSettings.LoadAutoShiftSettings();

            G.Logger.Debug($"{nameof(MainForm)}: Настройки были считаны из файлов...");

            //Задаем режимы логирования в соответствие с параметрами
            if (!G.Settings.LogTrace) G.Logger.Level ^= LoggingLevel.Trace;
            if (!G.Settings.LogDebug) G.Logger.Level ^= LoggingLevel.Debug;
            if (!G.Settings.LogInfo) G.Logger.Level ^= LoggingLevel.Info;
            if (!G.Settings.LogWarn) G.Logger.Level ^= LoggingLevel.Warn;
            if (!G.Settings.LogError) G.Logger.Level ^= LoggingLevel.Error;
            if (!G.Settings.LogFatal) G.Logger.Level ^= LoggingLevel.Fatal;

            G.Logger.Debug($"{nameof(MainForm)}: Типы логируемых сообщений были установлены...");

            //Отладочный режим для Dll
            SensorE.DebugMode(G.Settings.DebugMode);

            G.Logger.Debug($"{nameof(MainForm)}: Режим Dll был установлен...");

            //Инициализируем входной буфер профилей
            G.InputBuffer = new ReadWriteBuffer<byte[]>(G.Settings.InputBufferSize);

            //Инициализируем буфер профилей
            G.ProfileBuffer = new ProfileBuffer(G.Settings.ProfileBufferSize);

            G.Logger.Debug($"{nameof(MainForm)}: Буферы были инициализированы...");

            //Подгружаем калибровку
            Calibration.LoadCalibration();

            G.Logger.Debug($"{nameof(MainForm)}: Калибровка была загружена...");

            ////Загружаем пробный профиль во входной буфер
            //G.InputBuffer.Write(Calibration.Threshold);

            //Запускаем таймер по очистке старых кадров
            //tDeleteOldFiles.Enabled = true;

            //Очищаем историю
            this.InvokeIfRequired(() => lbLog.Items.Clear());

            //Настраиваем список конфигураций
            this.InvokeIfRequired(() => UtilsD.InitListIdConfig(cbId));
            cbId.SelectedIndexChanged += CbId_SelectedIndexChanged;
            G.NeedIdSettings = G.Settings.Id;

            //Настраиваем таблицу дефектов
            this.InvokeIfRequired(() => UtilsD.InitDefectsTable(dgvDefects));

            //Настраиваем карту дефектов
            if (G.Settings.NMapRows != defectsMap.NRows || G.Settings.NMapColumns != defectsMap.NColumns)
                this.InvokeIfRequired(() => defectsMap.ChangeMapSize(G.Settings.NMapRows, G.Settings.NMapColumns));
            this.InvokeIfRequired(() =>
            {
                defectsMap.SetMapRange(0, 0, G.ProfileSize, G.Settings.MapRange);
            });

            G.Logger.Debug($"{nameof(MainForm)}: Элементы управления главного окна были настроены...");

            //При добавлении строки в буфер, будут автоматически сдвигаться реальные дефекты
            G.ProfileBuffer.AddedProfile += SwiftAndCheckRealDefects;

            G.Logger.Debug($"{nameof(MainForm)}: Привязка к буферу профилей осуществлена...");

            //Инициализируем датчики
            if (initSensors)
            {
                Sensor.InitSensors();

                G.Logger.Debug($"{nameof(MainForm)}: Датчики проинициализированы...");
            }

            //Соединяемся с OPC сервером
            G.OpcClient = new OpcClient();

            G.Logger.Debug($"{nameof(MainForm)}: Клиент OPC-сервера создан...");

            //Устанавливаем соединение с COM-портом
            G.SyncController = new SyncController(G.Settings.ComPortName);

            G.Logger.Debug($"{nameof(MainForm)}: Объект контроллера синхронизации создан...");

            //Устанавливаем соединение с базой данных
            //var mode = G.Settings.WriteInDatabase ? ClientMode.Writing : ClientMode.Reading;
            G.SqlClient = new SqlClient(/*mode*/);
            if (!G.SqlClient.IsOk)
            {
                MessageBox.Show($@"Не удалось установить соединение с базой данных!", @"Ошибка подключения к базе данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            G.Logger.Debug($"{nameof(MainForm)}: Клиент базы данных создан...");

            ////Запускаем работу с входным буфером
            //bwWork.RunWorkerAsync();

            //Запускаем таймеры
            _tBuffer.Elapsed += tBuffer_Tick;
            _tDefectsMap.Elapsed += tDefectsMap_Tick;
            _tOpcClient.Elapsed += tOpcClient_Tick;
            _tEncoder.Elapsed += tEncoder_Tick;
            _tShowSystemOk.Elapsed += tShowSystemOk_Tick;
            _tDeleteOldFiles.Elapsed += tDeleteOldFiles_Tick;

            _tBuffer.Enabled = true;
            _tDefectsMap.Enabled = true;
            _tOpcClient.Enabled = true;
            _tEncoder.Enabled = true;
            _tShowSystemOk.Enabled = true;
            _tDeleteOldFiles.Enabled = true;

            if (G.ConfigSettings.AutoShifts)
            {
                _tAutoShift.Elapsed += tAutoShift_Tick;
                _tAutoShift.Enabled = true;
            }

            if (G.Settings.AutoCreateReport)
            {
                _tAutoCreateReport.Elapsed += tAutoCreateReport_Tick;
                _tAutoCreateReport.Enabled = true;
            }

            G.Logger.Debug($"{nameof(MainForm)}: Таймеры запущены...");

            G.ResetNProfiles();

            //Запускаем работу с буфером
            G.NeedWorkInputBuffer = true;

            G.Logger.Debug($"{nameof(MainForm)}: Глобальные переменные установлены...");
        }



        #region Мои события

        /// <summary>
        /// Сдвигает координаты Y реальных дефектов и проверяет на сигнализацию
        /// </summary>
        private static void SwiftAndCheckRealDefects()
        {
            try
            {
                var markerLinePos = UtilsD.GetGyFromGd(G.Settings.MarkerLinePosition);

                for (var i = G.Defects.Count - 1; i >= 0; i--)
                {
                    var defect = G.Defects[i];
                    defect.SwiftY();

                    //Проверка на сигнализацию с записью в базу данных
                    if (defect.End.Y >= markerLinePos)
                    {
                        if (defect.IsRealDefect && !defect.Alarmed)
                        {
                            if (G.Settings.Alarm) G.AlarmTime.Restart();
                            defect.Alarmed = true;

                            //Записываем информацию о дефекте в базу данных
                            if (G.Settings.WriteInDatabase)
                                G.SqlClient.Write(defect);

                            G.Logger.Debug(defect.ToString());
                        }
                    }
                }
            }
            catch (Exception e) { G.Logger.Trace(e.ToString()); }
        }

        /// <summary>
        ///     Сдвигает координаты Y реальных дефектов и проверяет на сигнализацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="addedProfileEventArgs"></param>
        private static async void SwiftAndCheckRealDefects(
            object sender,
            AddedProfileEventArgs addedProfileEventArgs
        ) =>
            //await Task.Run(SwiftAndCheckRealDefects);
            SwiftAndCheckRealDefects();

        #endregion


        #region SensorE.dll

        /// <summary>
        ///     Считывает и проверяет на пригодность версию dll
        /// </summary>
        private static void VersionDll()
        {
            //Logger.WriteToLog("Вошли в VersionDll");

            var res = SensorE.GetVersionDll(out var version);

            var numberVersion = 0;

            if (version != null)
            {
                G.VersionDll = version.Remove(0, 4); //Отображаем на форме версию без "ver "

                //Получаем численное представление версии без точки
                numberVersion = int.Parse(version.Substring(4, 4).Remove(1, 1));

                //120 - это версия 1.20
                if (numberVersion > 120)
                {
                    G.Logger.Info($"{nameof(MainForm)}: Связь с библиотекой SensorE.dll установлена");
                    return;
                }
            }

            MessageBox.Show(
                @"Связь с библиотекой SensorE.dll (версия больше 1.20) не была установлена! Проверьте наличие библиотеки и перезапустите программу."
            );
            G.Logger.Fatal($"{nameof(MainForm)}: Библиотека SensorE.dll >v1.20 не была найдена! (ver = {numberVersion}) Код ошибки: " + res
            );

            Program.Close();
            //Process.GetCurrentProcess().Kill(); //Убиваем процесс программы
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь закрыл программу");
            e.Cancel = true;
            Program.Close();
        }

        #region BW

        private void bwInitialization_DoWork(object sender, DoWorkEventArgs e) =>
            ReInit((bool)e.Argument);

        private void bwInitialization_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) throw e.Error;
        }

        #endregion

        #region Ссылки на дочерние формы

        ///// <summary>
        ///// Окно загрузки
        ///// </summary>
        //private LoadForm LoadF { get; set; }

        /// <summary>
        ///     Настройки программы
        /// </summary>
        private SystemSettingsForm SystemSettingsF { get; set; }

        /// <summary>
        /// Настройки авто-смен
        /// </summary>
        private AutoShiftSettingsForm AutoShiftSettingsF { get; set; }

        /// <summary>
        ///     Список ошибок системы
        /// </summary>
        private ErrorForm ErrorF { get; set; }

        /// <summary>
        ///     Окно "О программе"
        /// </summary>
        private AboutProgramForm AboutProgramF { get; set; }

        /// <summary>
        ///     Окно ручной работы
        /// </summary>
        private ManualModeForm ManualModeF { get; set; }

        /// <summary>
        ///     Окно статистики
        /// </summary>
        private StatisticForm StatisticF { get; set; }

        /// <summary>
        ///     Окно времен
        /// </summary>
        private TimeForm TimeF { get; set; }

        /// <summary>
        ///     Окно контроллера
        /// </summary>
        private ControllerForm ControllerF { get; set; }

        /// <summary>
        ///     Окно отображения буфера строк
        /// </summary>
        private ProfileBufferForm ProfileBufferF { get; set; }

        /// <summary>
        ///     Окно калибровки
        /// </summary>
        private CalibrationForm CalibrationF { get; set; }

        /// <summary>
        ///     Окно загрузки кадра
        /// </summary>
        private LoadFrameForm LoadFrameF { get; set; }

        /// <summary>
        ///     Окно тестов
        /// </summary>
        private TestForm TestF { get; set; }

        #endregion

        #region Таймеры

        private void tAutoShift_Tick(object sender, ElapsedEventArgs e)
        {
            //_tAutoShift.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tAutoShift.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                if (G.NeedIdSettings != G.Settings.Id)
                {
                    G.IsAuto = true;
                    G.NeedWorkAfterAutoShift = true;
                    this.InvokeIfRequired(() =>
                    {
                        var index = cbId.Items.IndexOf(G.NeedIdSettings);
                        if (index != -1)
                            cbId.SelectedIndex = index;
                    });
                    G.NeedWorkAfterAutoShift = false;
                    G.IsAuto = false;

                    G.Logger.Debug($"{nameof(MainForm)}: Конфигурация была изменена авто-сменой...");
                }
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tAutoShift.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }
        private async void tBuffer_Tick(object sender, EventArgs e)
        {
            //await _tBuffer.DoEventWithTimerPauseAsync(() =>

            //Выключение таймера
            try
            {
                _tBuffer.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                await Task.Run(() =>
                {
                    while (G.NeedWorkInputBuffer)
                    {
                        if (G.InputBuffer.Count > 0)
                        {
                            var swCycle = new Stopwatch();
                            if (Time.IsNeed)
                                swCycle.Start();

                            var profile = G.InputBuffer.Read();

                            if (profile != null)
                            {
                                G.ProfileBuffer.Add(profile);

                                //G.TempCount++;
                            }

                            swCycle.Stop();
                            if (Time.IsNeed)
                                Time.Cycle = swCycle.Elapsed.TotalMilliseconds;
                        }

                        //Производим очистку памяти, пока есть время
                        else
                        {
                            GC.Collect();

                            return;
                        }
                    }
                });
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tBuffer.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private void tShowSystemOk_Tick(object sender, EventArgs e)
        {
            //_tShowSystemOk.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tShowSystemOk.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                if (G.Settings.CheckAutoBorders && G.NProfiles > G.Settings.NCalibrationProfiles)
                {

                    if (!defectsMap.IsValidLeftBorder())
                    {
                        G.ResetNProfiles();
                        G.Logger.Info($"{nameof(MainForm)}: Обнаружено смещение полотна слева, пытаемся сменить тамбур и обнаружить новые границы.");
                    }

                    if (!defectsMap.IsValidRightBorder())
                    {
                        G.ResetNProfiles();
                        G.Logger.Info($"{nameof(MainForm)}: Обнаружено смещение полотна справа, пытаемся сменить тамбур и обнаружить новые границы.");
                    }
                }



                Application.DoEvents();

                bErrorForm.Enabled = !G.IsOk;

                var sensorsIsOk = UtilsD.SensorsIsOk(out var status);
                var uniqueIpIsOk = UtilsSettings.CheckUniqueIpLoadedSensors();
                var calibrationIsOk = Calibration.IsOk();
                var syncIsOk = G.SyncController != null && G.SyncController.IsOk;
                var opcIsOk = G.OpcClient != null && G.OpcClient.IsOk;
                var opcIsGood = G.OpcClient != null && G.OpcClient.GoodData;
                var sqlIsOk = G.SqlClient != null && G.SqlClient.IsOk;
                var meanIsOk = UtilsD.MeanIsOk();

                var isOk = sensorsIsOk && uniqueIpIsOk && calibrationIsOk &&
                           syncIsOk && opcIsOk && opcIsGood && sqlIsOk && meanIsOk;

                if (isOk)
                {
                    lSystemOk.Text = @"Система исправна";
                    lSystemOk.BackColor = Color.Green;
                    G.IsOk = true;
                }
                else
                {
                    lSystemOk.Text = @"Система неисправна";
                    lSystemOk.BackColor = Color.Red;

                    //Если система была в порядке
                    if (G.IsOk)
                    {
                        //Анализ датчиков
                        if (!sensorsIsOk)
                        {
                            var lengthStatus = status.Length;

                            for (var i = 0; i < lengthStatus; i++)
                            {
                                var s = status[i];
                                var sensor = G.Sensors[i];

                                switch (s)
                                {
                                    case SensorStatus.NoInitialization:
                                        G.Logger.Warn(
                                            $"{nameof(MainForm)}: Датчик №{i + 1} с конфигурацией {sensor.Settings.Id} (IP = {sensor.Settings.Ip}) не был инициализирован!");

                                        break;
                                    case SensorStatus.Bad:
                                        G.Logger.Warn(
                                            $"{nameof(MainForm)}: Датчик №{i + 1} с конфигурацией {sensor.Settings.Id} (IP = {sensor.Settings.Ip}) неисправен!");

                                        break;
                                }
                            }
                        }

                        if (!uniqueIpIsOk)
                            G.Logger.Warn($"{nameof(MainForm)}: IP адреса созданных объектов датчиков совпадают!");

                        if (!calibrationIsOk)
                            G.Logger.Warn($"{nameof(MainForm)}: Загруженная калибровка непригодна для текущей конфигурации системы!");

                        if (!syncIsOk)
                            G.Logger.Warn($"{nameof(MainForm)}: Нет связи с контроллером синхронизации!");

                        if (!opcIsOk)
                            G.Logger.Warn($"{nameof(MainForm)}: Нет связи с OPC сервером!");

                        if (!opcIsGood)
                            G.Logger.Warn($"{nameof(MainForm)}: Не удается считать данные с OPC-сервера!");

                        if (!sqlIsOk)
                            G.Logger.Warn($"{nameof(MainForm)}: Нет связи с базой данных!");

                        if (!meanIsOk)
                            G.Logger.Warn($"{nameof(MainForm)}: Программа не видит полотна! (не средний уровень освещенности)");
                    }

                    G.IsOk = false;
                }

                lSystemOk.Update();
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tShowSystemOk.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private async void tEncoder_Tick(object sender, EventArgs e)
        {
            //await _tEncoder.DoEventWithTimerPauseAsync(() =>

            //Выключение таймера
            try
            {
                _tEncoder.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                await Task.Run(() =>
                {
                    if (G.Settings.Alarm && G.AlarmTime.IsRunning)
                    {
                        if (G.AlarmTime.Elapsed.TotalSeconds < G.Settings.AlarmDuration && !G.StopAlarm)
                        {
                            if (!G.SyncController.Alarmed)
                                G.SyncController.Alarm();
                        }
                        else
                        {
                            if (G.SyncController.Alarmed)
                                G.SyncController.Alarm(false);
                            G.AlarmTime.Reset();
                            G.StopAlarm = false;
                        }
                    }
                });
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tEncoder.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private async void tOpcClient_Tick(object sender, EventArgs e)
        {
            //await _tOpcClient.DoEventWithTimerPauseAsync(() =>

            //Выключение таймера
            try
            {
                _tOpcClient.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                await Task.Run(() =>
                {
                    //Если система не запущена, то ничего не делаем
                    if (!G.Launched)
                        return;

                    if (G.OpcClient.DontWork)
                        G.SyncController.StopController();
                    else
                        G.SyncController.StartController();
                });
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tOpcClient.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private void tDefectsMap_Tick(object sender, EventArgs e)
        {
            //_tDefectsMap.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tDefectsMap.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                defectsMap.UpdateAllCells();
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tDefectsMap.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private async void tDeleteOldFiles_Tick(object sender, EventArgs e)
        {
            //await _tDeleteOldFiles.DoEventWithTimerPauseAsync(() =>

            //Выключение таймера
            try
            {
                _tDeleteOldFiles.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                await Task.Run(() =>
                {
                    var delFiles = new List<string>();
                    delFiles.AddRange(KUtils.DeleteOldFiles(G.PathToLogs));
                    delFiles.AddRange(KUtils.DeleteOldFiles(G.PathToPics));

                    if (delFiles.Count > 0)
                    {
                        var message =
                            "При проведении процедуры удаления старых файлов были удалены следующие файлы:";

                        foreach (var delFile in delFiles)
                            message += $"\n{delFile}";

                        G.Logger.Info($"{nameof(MainForm)}: {message}");
                    }

                });
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tDeleteOldFiles.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private async void tAutoCreateReport_Tick(object sender, ElapsedEventArgs e)
        {
            //await _tAutoCreateReport.DoEventWithTimerPauseAsync(() =>

            //Выключение таймера
            try
            {
                _tAutoCreateReport.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                await Task.Run(() =>
                {
                    //G.Shift.UpdateCurrent();
                    if (G.Shift.UpdateCurrent() && G.SqlClient.IsOk)
                    {
                        var begin = G.Shift.PrevEndTime;
                        var end = G.Shift.BeginTime;

                        var fileName = $"Отчет - {DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xlsx";
                        G.SqlClient.CreateReport(begin, end, fileName, false);
                    }
                });
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tAutoCreateReport.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        #endregion

        #region Контролы

        #region Кнопки

        private void bStart_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь начал работу программы от энкодера");

            bStart.Enabled = false;
            G.SyncController.StartController();

            Application.DoEvents();

            G.Launched = true;
            bStop.Enabled = true;

            bStart.BackColor = Color.Green;
            bStart.ForeColor = Color.Black;
            bStop.BackColor = Color.Empty;
            bStop.ForeColor = Color.OrangeRed;
        }

        public void bStop_Click(object sender, EventArgs e)
        {
            if (!G.IsAuto && MessageBox.Show(@"Остановить работу программного обеспечения?",
                    @"Останов работы ПО", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            G.Logger.Info($"{nameof(MainForm)}: Пользователь остановил работу программы от энкодера");

            bStop.Enabled = false;
            G.SyncController.StopController();

            Application.DoEvents();

            G.Launched = false;
            bStart.Enabled = true;

            bStart.BackColor = Color.Empty;
            bStart.ForeColor = Color.Green;
            bStop.BackColor = Color.OrangeRed;
            bStop.ForeColor = Color.Black;
        }

        #endregion

        #region Таблица дефектов

        private void AddRowInTable(Defect defect)
        {
            var row = new object[5];

            var strType = defect.StrType;
            var square = defect.Square.ToString("F2");
            var yCoord = UtilsD.GetYCoordCenter(defect).ToString("F2");
            var xCoord = UtilsD.GetXCoordCenter(defect).ToString("F2");
            var time = defect.DetectionTime?.ToString("HH:mm:ss");

            row[0] = strType;
            row[1] = square;
            row[2] = yCoord;
            row[3] = xCoord;
            row[4] = time;

            this.TryInvokeIfRequired(
                () =>
                {
                    dgvDefects.Rows.Add(row);
                    dgvDefects.Update();

                    lbLog.TopIndex = lbLog.Items.Add($"{time}: {strType} {square} мм2; {yCoord} м; {xCoord} м");
                }
            );
        }

        private void AddRowInTable(object sender, AddedDefectEventArgs e) => AddRowInTable(e.Defect);

        private void RemoveRowFromTable(int index)
        {
            this.TryInvokeIfRequired(
                    () =>
                    {
                        dgvDefects.Rows.RemoveAt(index);
                        dgvDefects.Update();
                    }
                );
        }

        private void RemoveRowFromTable(object sender, RemovedDefectEventArgs e) => RemoveRowFromTable(e.Index);


        //private void dgvDefects_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        //{
        //    if (e.RowIndex >= G.Defects.Count) return;

        //    var defect = G.Defects[e.RowIndex];

        //    switch (e.ColumnIndex)
        //    {
        //        case 0:
        //            e.Value = defect.StrType;
        //            break;

        //        case 1:
        //            e.Value = defect.Square.ToString(CultureInfo.InvariantCulture);
        //            break;

        //        case 2:
        //            e.Value = defect.D.ToString(CultureInfo.InvariantCulture);
        //            break;

        //        case 3:
        //            e.Value = defect.L.ToString(CultureInfo.InvariantCulture);
        //            break;

        //        case 4:
        //            e.Value = defect.DetectionTime?.ToString("HH:mm:ss");
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        private void dgvDefects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь нажал на ячейку таблицы дефектов");

            try
            {
                if (G.Settings.Zoom)
                {
                    if (e.RowIndex > -1)
                        defectZoom.DrawDefect(G.Defects[e.RowIndex]);
                    else
                        defectZoom.Image = null;
                }
            }
            catch (Exception exception) { G.Logger.Trace(exception.ToString()); }
        }


        private void dgvDefects_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (G.Settings.Zoom)
                {
                    if (dgvDefects.CurrentRow != null)
                        defectZoom.DrawDefect(G.Defects[dgvDefects.CurrentRow.Index]);
                    else
                        defectZoom.Image = null;
                }

            }
            catch (Exception exception) { G.Logger.Trace(exception.ToString()); }
        }

        private void dgvDefects_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                var index = (e.RowIndex + 1).ToString();

                var centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                if (sender is DataGridView table)
                {
                    var headerBounds = new Rectangle(
                        e.RowBounds.Left,
                        e.RowBounds.Top,
                        table.RowHeadersWidth,
                        e.RowBounds.Height
                    );
                    e.Graphics.DrawString(index, Font, SystemBrushes.ControlText, headerBounds, centerFormat);
                }
            }
            catch (Exception exception) { G.Logger.Error(exception.ToString()); }
        }

        #endregion

        #endregion


        #region Дочерние формы

        private void tsmiSystemSettings_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно настроек программы");

            if (SystemSettingsF?.IsDisposed != false) SystemSettingsF = new SystemSettingsForm(this);

            SystemSettingsF.ShowDialog();
        }

        private void TsmiAutoShifts_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно настройки авто-смен");

            if (AutoShiftSettingsF?.IsDisposed != false) AutoShiftSettingsF = new AutoShiftSettingsForm();

            AutoShiftSettingsF.ShowDialog();
        }

        private void bErrorForm_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно ошибок");

            if (ErrorF?.IsDisposed != false) ErrorF = new ErrorForm();

            ErrorF.Show();
            ErrorF.Activate();
        }

        private void tsmiAboutProgram_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно \"О программе\"");

            if (AboutProgramF?.IsDisposed != false) AboutProgramF = new AboutProgramForm();

            AboutProgramF.Show();
            AboutProgramF.Activate();
        }

        private void tsmiManualMode_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            //if (!G.IsOk)
            //{
            //    MessageBox.Show(@"Система неисправна! Сперва устраните ошибки!");
            //    return;
            //}

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно ручной работы");

            if (ManualModeF?.IsDisposed != false) ManualModeF = new ManualModeForm();

            ManualModeF.Show();
            ManualModeF.Activate();
        }

        private void tsmiStatistic_Click(object sender, EventArgs e)
        {
            if (G.Sensors.Length == 0)
            {
                MessageBox.Show(@"Нет созданных объектов датчиков! Сперва устраните ошибки!");
                return;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно статистики");

            if (StatisticF?.IsDisposed != false) StatisticF = new StatisticForm();

            StatisticF.Show();
            StatisticF.Activate();
        }

        private void tsmiController_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно контроллера стробов");

            if (ControllerF?.IsDisposed != false) ControllerF = new ControllerForm();

            ControllerF.Show();
            ControllerF.Activate();
        }

        private void tsmiBuffer_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно буфера профилей");

            if (ProfileBufferF?.IsDisposed != false)
            {
                ProfileBufferF = new ProfileBufferForm();

                //ShowFormDelegate d = delegate
                //{
                //    ProfileBufferF.ShowDialog();
                //    ProfileBufferF.Dispose();
                //    ProfileBufferF = null;
                //};
                //d.BeginInvoke(null, null);
                //ProfileBufferF.Show();
            }

            ProfileBufferF.Show();
            ProfileBufferF.Activate();
        }

        private void tsmiCalibration_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно калибровки");

            if (CalibrationF?.IsDisposed != false)
            {
                CalibrationF = new CalibrationForm();

                //ShowFormDelegate d = delegate
                //{
                //    CalibrationF.ShowDialog();
                //    CalibrationF.Dispose();
                //    CalibrationF = null;
                //};
                //d.BeginInvoke(null, null);
                //CalibrationF.Show();
            }
            CalibrationF.Show();
            CalibrationF.Activate();
        }

        private void tsmiTime_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно времени");

            if (TimeF?.IsDisposed != false) TimeF = new TimeForm();

            TimeF.Show();
            TimeF.Activate();
        }


        private void tsmiTest_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Тестовое окно (непонятно как)");

            if (TestF?.IsDisposed != false) TestF = new TestForm();

            TestF.Show();
            TestF.Activate();
        }

        private void bTambourChange_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно смены тамбура");

            var tambourChangeF = new TambourChangeForm();
            if (tambourChangeF.ShowDialog() == DialogResult.OK)
            {
                G.SqlClient.NTambour = tambourChangeF.NTambour;
                G.SqlClient.NShift = tambourChangeF.NShift;
                G.ResetNProfiles();
            }
        }

        #endregion

        #region Смена размера окна

        private void MainForm_ResizeBegin(object sender, EventArgs e) => this.SuspendPainting();

        private void MainForm_ResizeEnd(object sender, EventArgs e) => this.ResumePainting();

        #endregion

        private void bCreateReport_Click(object sender, EventArgs e)
        {
            if (G.Launched)
            {
                MessageBox.Show(@"Система запущена! Сперва остановите работу!");
                return;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно создания отчета");

            var createReportF = new CreateReportForm();
            createReportF.ShowDialog();
        }

        private void tsmiLoadFrame_Click(object sender, EventArgs e)
        {
            if (!G.IsAdministrator)
            {
                var passwordF = new PasswordForm { TopMost = true };
                if (passwordF.ShowDialog() != DialogResult.OK) return;
                G.IsAdministrator = true;
            }

            G.Logger.Info($"{nameof(MainForm)}: Пользователь вызвал Окно загрузки кадров");
            if (LoadFrameF?.IsDisposed != false) LoadFrameF = new LoadFrameForm();

            LoadFrameF.Show();
            LoadFrameF.Activate();
        }

        /// <summary>
        ///     Смена конфигурации была отменена?
        /// </summary>
        private bool _canceled;

        private void CbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если мы тут после отмены или -1, то уходим
            if (_canceled || cbId.SelectedIndex == -1) return;

            if (!G.IsAuto)
            {
                var dialog = MessageBox.Show(
                    @"Вы уверены, что желаете изменить конфигурацию системы? Будет произведена полная переинициализация системы.",
                    @"Смена конфигурации", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (dialog != DialogResult.OK)
                {
                    _canceled = true;
                    var id = G.ConfigSettings.SystemSettings;
                    cbId.SelectedIndex = cbId.Items.IndexOf(id);
                    _canceled = false;
                    return;
                }
            }

            G.ConfigSettings.SystemSettings = cbId.Text;
            G.ConfigSettings.SaveSettingsInFile();

            BwReInit();
        }

        private void BClearLog_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
            G.Logger.Info($"{nameof(MainForm)}: Пользователь нажал на кнопку очистки истории.");
        }

        private void BResetAlarm_Click(object sender, EventArgs e)
        {
            if (G.AlarmTime.IsRunning)
                G.StopAlarm = true;

            G.Logger.Info($"{nameof(MainForm)}: Пользователь нажал на кнопку сброса тревоги.");
        }
    }
}