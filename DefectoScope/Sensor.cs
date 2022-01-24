#region

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Kogerent;

#endregion

namespace DefectoScope
{
    public class Sensor
    {
        #region Поля

        /// <summary>
        ///     Режим работы датчиков
        /// </summary>
        public const Mode SensorMode = Mode.Video;

        /// <summary>
        ///     Размер кадра по ширине
        /// </summary>
        public const ushort SizeX = 2048;

        /// <summary>
        ///     Размер кадра по высоте
        /// </summary>
        public const ushort SizeY = 1088;

        /// <summary>
        ///     Идентификатор объекта типа указатель на 32-битное целочисленное значение
        /// </summary>
        public IntPtr Handle;

        /// <summary>
        ///     Датчик включен?
        /// </summary>
        public bool IsEnabled;

        /// <summary>
        ///     Датчик нужен?
        /// </summary>
        public bool IsNeed = false;

        /// <summary>
        ///     Датчик проинициализирован?
        /// </summary>
        public bool Initialized;

        /// <summary>
        ///     Температура профилометра
        /// </summary>
        public float Temper;

        /// <summary>
        ///     Частота кадров в режиме непрерывной работы
        /// </summary>
        public int FrameRate = 1;

        /// <summary>
        ///     Номер датчика
        /// </summary>
        public byte NDevice;

        /// <summary>
        ///     Режим работы датчика
        /// </summary>
        public Mode Mode { get; }

        /// <summary>
        ///     Режим синхронизации
        /// </summary>
        public SyncMode SyncMode { get; private set; }

        /// <summary>
        ///     Callback обрабатывается?
        /// </summary>
        public bool ProcessingCallback;

        /// <summary>
        ///     Данные получены?
        /// </summary>
        public bool GotData;

        /// <summary>
        /// Реальная частота профилей (Гц)
        /// </summary>
        public float RealFrequencyProfiles = 1;

        ///// <summary>
        /////     Количество последних частот
        ///// </summary>
        //private const byte FrequenciesCapacity = 100;

        /// <summary>
        /// Количество стробов в пачке (для измерения средней частоты)
        /// </summary>
        private ushort _countStrobes;

        /// <summary>
        /// Время затраченное на пачку (для измерения средней частоты)
        /// </summary>
        private double _totalSeconds;

        /// <summary>
        /// Число стробов в пачке (для измерения средней частоты)
        /// </summary>
        private ushort _nStrobes = 1000;

        ///// <summary>
        /////     Буфер последних частот профилей (Гц)
        ///// </summary>
        //private readonly RecentItemsBuffer<float> _realFrequenciesProfiles =
        //    new RecentItemsBuffer<float>(FrequenciesCapacity);

        ///// <summary>
        /////     Количество пропущенных программой профилей (из-за обработки)
        ///// </summary>
        //public int NProgramSkippedProfiles;

        ///// <summary>
        /////     Количество фактов пропущенных профилей датчиком
        ///// </summary>
        //public int NSensorSkippedProfiles;

        /// <summary>
        ///    Номер кадра/профиля по dll
        /// </summary>
        public int NFrame;

        /// <summary>
        /// Состояние дискретных входов (от 0 до 0х0F) по dll (только в режиме профилей)
        /// </summary>
        public int StatusDiscreteInputs;

        /// <summary>
        /// Количество точек, полученных профилометром, по dll (только в режиме профилей)
        /// </summary>
        public int NPoints;

        /// <summary>
        /// Номер строба
        /// </summary>
        public int NStrobe;

        /// <summary>
        /// Количество фактов переполнения входного буфера в dll
        /// </summary>
        public int NOverflowsBuffer;

        /// <summary>
        /// Количество фактов неполучения кадров по таймауту в dll
        /// </summary>
        public int NTimeouts;

        /// <summary>
        /// Количество фактов неполных кадров в dll
        /// </summary>
        public int NBreakFrames;

        /// <summary>
        /// Количество фактов пропущенных стробов в dll
        /// </summary>
        public int NSkipStrobes;

        /// <summary>
        /// Общее количество кадров
        /// </summary>
        public int NAllFrames;

        /// <summary>
        /// Общее количество неполных кадров
        /// </summary>
        public int NBadFrames;

        /// <summary>
        ///     Буфер кадра (под окно)
        /// </summary>
        public byte[] Buffer = new byte[0];

        ///// <summary>
        /////     Строка облака точек
        ///// </summary>
        //public byte[] Row = new byte[0];

        /// <summary>
        ///     Настройки датчика
        /// </summary>
        public SensorSettings Settings;

        /// <summary>
        ///     Делегат для получения видео
        /// </summary>
        private SensorE.CallbackEventVideoDelegate _callbackEventVideo = CallbackVideo;

        /// <summary>
        ///     Блокировщик части кода callback
        /// </summary>
        private static readonly object LockerCallback = new object();

        /// <summary>
        ///     Делегат для получения профилей
        /// </summary>
        private readonly SensorE.CallbackEventProfileDelegate _callbackEventProfile = null;

        /// <summary>
        ///     Делегат для авто-экспозиции
        /// </summary>
        private readonly SensorE.CallbackEventAutoExpDelegate _callbackEventAutoExp = null;

        /// <summary>
        ///     Размер заголовка данных callback
        /// </summary>
        private const byte CallbackDataHeaderSize = 16;

        /// <summary>
        ///     Количество байт в данных заголовка данных callback
        /// </summary>
        private const byte NBytesInCallbackDataHeaderType = 4;

        /// <summary>
        ///     Размер заголовка данных callback в файтах
        /// </summary>
        private const byte CallbackDataHeaderBytes = 64;

        /// <summary>
        ///     Время цикла
        /// </summary>
        private readonly Stopwatch _cycleTime = new Stopwatch();

        ///// <summary>
        ///// Объект для блокировки кода в callback'ах
        ///// </summary>
        //private static readonly object LockerInCallback = new object();

        ///// <summary>
        ///// Делегат при получении всех данных
        ///// </summary>
        //public static CallbackGotAllData CallbackGotAllData = null;

        /// <summary>
        ///     Профиль (объединение строк всех датчиков)
        /// </summary>
        public static byte[] Profile = new byte[0];

        #endregion

        #region Свойства

        /// <summary>
        ///     Данные всех датчиков получены?
        /// </summary>
        public static bool GotAllData
        {
            get
            {
                var result = true;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var sensor in G.Sensors) result &= sensor.GotData;
                return result;
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        ///     Создает объект датчика
        ///     <param name="nDevice">Номер датчика</param>
        /// <param name="mode">Режим работы датчика</param>
        /// <param name="settings">Настройки датчика, если null, то по умолчанию</param>
        /// </summary>
        public Sensor(byte nDevice, Mode mode, SensorSettings settings = null)
        {
            NDevice = nDevice;
            Mode = mode;
            Settings = settings ?? new SensorSettings();
        }

#endregion

        #region Функции из SensorE

        public int DisposeSensorE() => SensorE.DisposeSensor(Handle);

        public int CreateSensorE() => SensorE.CreateSensorV2(Settings.Ip, out Handle);

        //public int CreateSensorE() => SensorE.CreateSensor(Settings.Ip, 10, 11, out Handle, 10);
        public int StopE() => SensorE.Stop(Handle);

        //public int LoadCalibrationE() => SensorE.LoadCalibration(Handle,
        //    Application.StartupPath + G.PathToCalibration + Settings.FileCalibration,
        //    1); //это номер калибровки в файле BIN, который всегда равен 1

        //public int CalibrationModeE() => SensorE.CalibrationModeV2(Handle, Settings.InMm);

        //public int SetParametersE() => SensorE.SetParameters(Handle, Settings.MaxSpotsCount, Settings.Level,
        //    Settings.MinWidth, Settings.MaxWidth);

        //public int SetRotateParamE() =>
        //    SensorE.SetRotateParam(Handle, Math.Abs(Settings.Angle) > 0 ? 1 : 0, Settings.Angle);

        public int SpotsNoAckColumnE() => SensorE.SpotsNoAckColumn(Handle);
        public int VideoE() => SensorE.Video(Handle);
        public int VideoToRamE() => SensorE.VideoToRam(Handle);

        public int StartListenerE() =>
            SensorE.StartListener(Handle, _callbackEventVideo, _callbackEventProfile, _callbackEventAutoExp);

        public int SetWindowE() => SensorE.SetWindow(
            Handle,
            Settings.LeftWindow,
            Settings.TopWindow,
            Settings.WidthWindow,
            Settings.HeightWindow
        );

        public int SetInvLaserE() => SensorE.SetInvLaser(Handle);
        public int SetExpositionE() => SensorE.SetExposition(Handle, Settings.Exposition);
        public int SetPgaGainE() => SensorE.SetPgaGain(Handle, Settings.PgaGain);
        public int SetAdcGainE() => SensorE.SetAdcGain(Handle, Settings.AdcGain);
        public int SyncExtE() => SensorE.SyncExt(Handle);
        public int SyncCmdE() => SensorE.SyncCmd(Handle);
        public int SendSyncE() => SensorE.SendSync(Handle);
        public int SyncNoneE() => SensorE.SyncNone(Handle, FrameRate);
        public int GetTemperE() => SensorE.GetTemper(Handle, out Temper);

        #endregion

        /// <summary>
        ///     Это успех? (успешное выполнение операции из SensorE.dll)
        /// </summary>
        /// <param name="errorCode">Код ошибки</param>
        /// <returns></returns>
        public bool IsSuccess(int errorCode) => errorCode == SensorE.Success;

        /// <summary>
        ///     Вызывает функцию из Sensor.E по стандартному "протоколу"
        /// </summary>
        /// <param name="func">Функции датчика заканчиваются на "E"</param>
        /// <returns>Успех?</returns>
        public bool Call(Func<int> func)
        {
            try
            {
                var errorCode = func.Invoke();
                if (!IsSuccess(errorCode))
                {
                    G.Logger.Error($"{nameof(Sensor)}: Датчик №{NDevice}: {func.Method.Name} = {errorCode}");

                    //Для функции DisposeSensor не делаем (цикличность)
                    if (func.Method.Name != nameof(DisposeSensorE))
                    {
                        try
                        {
                        
                            DisposeSensor();
                        }
                        catch { // ignored
                        }
                    }

                    return false;
                }

                return true;
            }
            catch { throw new Exception($"{nameof(Call)}({func.Method.Name})"); }
        }

        /// <summary>
        ///     Уничтожает объект датчика, если он он был создан
        /// </summary>
        /// <returns>Успех</returns>
        public bool DisposeSensor()
        {
            //Если объект датчика не существует, ничего не делаем
            if (Handle == (IntPtr) null) return true;

            var result = Call(DisposeSensorE);

            //В любом случае очищаем привязку на объект
            Handle = (IntPtr) null;

            return result;
        }

        /// <summary>
        ///     Создает объект датчика, если его не существует
        /// </summary>
        /// <returns>Успех?</returns>
        public bool CreateSensor() => Handle != (IntPtr) null || Call(CreateSensorE);

        /// <summary>
        ///     Останавливает работу датчика и передачу данных в Dll
        /// </summary>
        /// <returns>Успех?</returns>
        public bool Stop() => Call(StopE);

        ///// <summary>
        /////     Загружает калибровочные данные в датчик
        ///// </summary>
        ///// <returns>Успех?</returns>
        //public bool LoadCalibration() => Call(LoadCalibrationE);

        ///// <summary>
        /////     Устанавливает калибровочный режим
        ///// </summary>
        ///// <returns>Успех?</returns>
        //public bool CalibrationMode() => Call(CalibrationModeE);

        ///// <summary>
        /////     Устанавливает параметры обработки пятен на кадрах, получаемых с матрицы
        ///// </summary>
        ///// <returns>Успех?</returns>
        //public bool SetParameters() => Call(SetParametersE);

        ///// <summary>
        /////     Устанавливает параметры вращения профиля
        ///// </summary>
        ///// <returns>Успех?</returns>
        //public bool SetRotateParam() => Call(SetRotateParamE);

        /// <summary>
        ///     Устанавливает параметры вращения профиля
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SpotsNoAckColumn() => Call(SpotsNoAckColumnE);

        /// <summary>
        ///     Устанавливает режим передачи видео
        /// </summary>
        /// <returns>Успех?</returns>
        public bool Video() => Call(VideoE);

        /// <summary>
        ///     Устанавливает режим передачи видео через ОЗУ
        /// </summary>
        /// <returns>Успех?</returns>
        public bool VideoToRam() => Call(VideoToRamE);

        /// <summary>
        ///     Запускает поток приема данных
        /// </summary>
        /// <returns>Успех?</returns>
        public bool StartListener() => Call(StartListenerE);

        /// <summary>
        ///     Устанавливает размеры окна
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SetWindow() => Call(SetWindowE);

        /// <summary>
        ///     Устанавливает инверсию включения лазера
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SetInvLaser() => Call(SetInvLaserE);

        /// <summary>
        ///     Устанавливает экспозицию датчика
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SetExposition() => Call(SetExpositionE);

        /// <summary>
        ///     Устанавливает аналоговое усиление
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SetPgaGain() => Call(SetPgaGainE);

        /// <summary>
        ///     Устанавливает цифровое усиление
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SetAdcGain() => Call(SetAdcGainE);

        /// <summary>
        ///     Устанавливает режим внешней синхронизации
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SyncExt()
        {
            SyncMode = SyncMode.SyncExt;
            return Call(SyncExtE);
        }

        /// <summary>
        ///     Устанавливает режим синхронизации по команде от сервера
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SyncCmd()
        {
            SyncMode = SyncMode.SyncCmd;
            return Call(SyncCmdE);
        }

        /// <summary>
        ///     Отправляет команду профилометру на выдачу одного профиля/кадра, работает только в режиме синхронизации по команде
        ///     от сервера SyncCmd
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SendSync() => Call(SendSyncE);

        /// <summary>
        ///     Устанавливает режим непрерывной работы с частотой <see cref="FrameRate" />
        /// </summary>
        /// <returns>Успех?</returns>
        public bool SyncNone()
        {
            SyncMode = SyncMode.SyncNone;
            return Call(SyncNoneE);
        }


        /// <summary>
        ///     Получает температуру датчика
        /// </summary>
        /// <returns>Успех?</returns>
        public bool GetTemper() => Call(GetTemperE);


        /// <summary>
        ///     Инициализирует датчик
        /// </summary>
        /// <returns>Успех?</returns>
        public bool InitSensor()
        {
            var success = CreateSensor() && StartListener() && Stop();

            success = success && SetWindow();

            switch (Mode)
            {
                //case Mode.Profile:

                //    success = success &&
                //              LoadCalibration() &&
                //              CalibrationMode() &&
                //              SetParameters() &&
                //              SetRotateParam() &&
                //              SpotsNoAckColumn();

                //    break;

                //Действия для инициализации видео-режима
                case Mode.Video:

                    success = success && Video();
                    break;

                //Действия для инициализации режима video-to-RAM
                case Mode.VideoRam:

                    success = success && VideoToRam();
                    break;
            }

            //success = success && SetWindow();

            success = success &&
                      SetExposition() &&
                      SetPgaGain() &&
                      SetAdcGain() &&
                      GetTemper();

            ////Если необходима инверсия включения лазера
            //if (Settings.InvLaser)
            //    success = success &&
            //              SetInvLaser();

            success = success && SyncExt();

            //Устанавливаем флаги
            Initialized = true;
            IsEnabled = success;

            if (!success) return false;

            G.Logger.Info($"{ nameof(Sensor)}: Датчик №{ NDevice}: инициализация завершена успешно");
            return true;
        }

        /// <summary>
        ///     Callback видео. Выкачиваем в буфер из dll.
        /// </summary>
        /// <param name="sender">Объект датчика</param>
        /// <param name="mode">Режим (не используется)</param>
        /// <param name="bytes">Указатель на буфер кадра</param>
        /// <param name="nBytes">Длина данных (вместе с заголовком)</param>
        private static void CallbackVideo(IntPtr sender, int mode, IntPtr bytes, int nBytes)
        {
            //G.Logger.WriteToLog("Вход в CallbackVideo");

            var s = G.Sensors.FirstOrDefault(sensor => sensor.Handle == sender);

            //Пришли данные от неизвестного датчика
            if (s == null)
            {
                G.Logger.Warn($"{nameof(Sensor)}: Пришли данные от неизвестного программе датчика.");
                return;
            }

            //Вычисление частоты профилей (даже пропущенные)
            if (s._cycleTime.IsRunning)
            {
                if (s._countStrobes < s._nStrobes)
                {
                    s._cycleTime.Stop();
                    s._totalSeconds += s._cycleTime.Elapsed.TotalSeconds;
                    s._countStrobes++;
                }
                else
                {
                    s.RealFrequencyProfiles = (float) (s._countStrobes / s._totalSeconds);
                    s._nStrobes = (ushort) KUtils.GetCeilingValue(s.RealFrequencyProfiles / 2);
                    s._totalSeconds = 0;
                    s._countStrobes = 0;
                }
                
                s._cycleTime.Restart();
            }
            else
                s._cycleTime.Start();

            //Обрабатываем данные для датчика, у которого их нет
            if (!s.GotData)
            {
                //Считываем заголовок
                unsafe
                {
                    var intArray = (uint*)bytes;
                    if (intArray != null)
                    {
                        s.NFrame = (int)intArray[0];

                        var flags = intArray[4];

                        var overflowBuffer = (flags & 1 << 0) != 0;
                        if (overflowBuffer) s.NOverflowsBuffer++;

                        var timeout = (flags & 1 << 1) != 0;
                        if (timeout) s.NTimeouts++;

                        var breakFrame = (flags & 1 << 2) != 0;
                        if (breakFrame) s.NBreakFrames++;

                        var skipStrobe = (flags & 1 << 3) != 0;
                        if (skipStrobe) s.NSkipStrobes++;
                    }
                }

                //Убираем заголовок из пакета данных
                nBytes -= CallbackDataHeaderBytes;

                //Если кроме заголовка больше ничего не осталось, то выходим, такой callback не нужен
                if (nBytes <= 0) return;

                //Заполняем буфер
                s.Buffer = new byte[nBytes];

                //G.Logger.Trace("Записали в локальный");

                Marshal.Copy(bytes + CallbackDataHeaderBytes, s.Buffer, 0, nBytes);

                s.GotData = true;

                if (!GotAllData) return;
            }

            //Блокируем код, так как другие потоки callback могут зайти
            lock (LockerCallback)
            {
                //Для первого потока это условие выполнится, сюда зашедшего, для остальных нет
                if (!GotAllData) return;

                //Если все данные пришли, или пришли новые данные для датчика их имеющего
                var sw = new Stopwatch();
                if (Time.IsNeed) sw.Start();

                //Собираем профиль
                Profile = UtilsD.GetProfile();
                //G.Logger.Trace("Создали профиль");

                if (Time.IsNeed)
                {
                    Time.ConcatAllRows = sw.Elapsed.TotalMilliseconds;
                    sw.Restart();
                }

                G.InputBuffer.Write(Profile);

                sw.Stop();
                if (Time.IsNeed) Time.AddInputBuffer = sw.Elapsed.TotalMilliseconds;

                //Сбрасываем ключ для получения новых данных
                foreach (var sensor in G.Sensors) sensor.GotData = false;
            }
        }

        /// <summary>
        ///     Привязывает обработку при получении видео
        /// </summary>
        public void BindCallbackEventVideo()
        {
            if (_callbackEventVideo == null) _callbackEventVideo = CallbackVideo;
        }

        #region Методы

        /// <summary>
        ///     Инициализирует датчики
        /// </summary>
        public static void InitSensors()
        {
            //Инициализируем первый датчик
            var success = G.Sensors[0].InitSensor();
            if (!success)
            {
                MessageBox.Show(
                    $@"Инициализация датчика (IP = {G.Sensors[0].Settings.Ip}) прошла с ошибкой!", @"Ошибка инициализации датчика", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
               

                //if (G.Settings.WorkingSensors)
                //{
                //    G.Logger.Fatal($@"Инициализация датчика (IP = {G.Sensors[0].Settings.Ip}) прошла с ошибкой!");
                //    Program.Close();
                //}
                //else
                G.Logger.Error($@"{nameof(Sensor)}: Инициализация датчика (IP = {G.Sensors[0].Settings.Ip}) прошла с ошибкой!");
                //Process.GetCurrentProcess().Kill();
            }

            //Также проверяем уникальность IP
            var isUnique = UtilsSettings.CheckUniqueIpLoadedSensors();

            //Если не уникальные адреса, то дальше не инициализируем
            if (!isUnique) return;

            //Для каждого датчика проводим инициализацию
            for (byte i = 1; i < G.Sensors.Length; i++)
            {
                success = /*success && */G.Sensors[i].InitSensor();
                if (!success)
                {
                    MessageBox.Show(
                        $@"Инициализация датчика (IP = {G.Sensors[i].Settings.Ip}) прошла с ошибкой!", @"Ошибка инициализации датчика", MessageBoxButtons.OK, MessageBoxIcon.Error
                    );

                    //if (G.Settings.WorkingSensors)
                    //{
                    //    G.Logger.Fatal($@"Инициализация датчика (IP = {G.Sensors[i].Settings.Ip}) прошла с ошибкой!");
                    //    Program.Close();
                    //}
                    //else
                        G.Logger.Error($@"{nameof(Sensor)}: Инициализация датчика (IP = {G.Sensors[i].Settings.Ip}) прошла с ошибкой!");
                    //Process.GetCurrentProcess().Kill();
                }
            }

            ////Привязываемся к callback
            //if (success)
            //    foreach (var sensor in G.Sensors) sensor.BindCallbackEventVideo();
        }

        /// <summary>
        ///     Останавливает работу всех датчиков
        /// </summary>
        public static void StopSensors()
        {
            foreach (var sensor in G.Sensors)
            {
                if (sensor.Handle == IntPtr.Zero) continue;

                sensor.SyncCmd();
                sensor.Stop();
            }
        }

        #endregion

        #region Единицы измерения

        #endregion

        #region Пиксели

        /// <summary>
        ///     Возвращает глобальную координату X из локальной.
        /// </summary>
        /// <param name="x">Локальная координата X</param>
        /// <returns></returns>
        public int GetGxFromX(int x)
        {
            if (x > Settings.WidthWindow)
                throw new Exception($"Локальная координата X = {x} не принадлежит диапазону датчика!");

            var tX = 0;
            foreach (var sensor in G.Sensors)
            {
                if (sensor.NDevice != NDevice)
                    tX += sensor.Settings.WidthWindow;
                else
                    break;
            }

            return x + tX;
        }

        /// <summary>
        ///     Ищет глобальную координату X из локальной.
        ///     <para>True - если глобальная координата X нашлась.</para>
        /// </summary>
        /// <param name="x">Локальная координата X</param>
        /// <param name="gX">Глобальная координата X</param>
        /// <returns>Локальная координата X нашлась?</returns>
        public bool FindGxFromX(int x, out int gX)
        {
            try
            {
                gX = GetGxFromX(x);
                return true;
            }
            catch
            {
                gX = default;
                return false;
            }
        }

        #endregion

        #region Миллиметры

        /// <summary>
        ///     Возвращает глобальную координату L из локальной.
        /// </summary>
        /// <param name="l">Локальная координата L</param>
        /// <returns></returns>
        public float GetGlFromL(float l)
        {
            if (l > Settings.LWindow)
                throw new Exception($"Локальная координата L = {l} не принадлежит диапазону датчика!");

            double tL = 0;
            foreach (var sensor in G.Sensors)
            {
                if (sensor.NDevice != NDevice)
                    tL += sensor.Settings.LWindow;
                else
                    break;
            }

            return l + (float) tL;
        }

        /// <summary>
        ///     Ищет глобальную координату L из локальной.
        ///     <para>True - если глобальная координата L нашлась.</para>
        /// </summary>
        /// <param name="l">Локальная координата L</param>
        /// <param name="gL">Глобальная координата L</param>
        /// <returns>Локальная координата X нашлась?</returns>
        public bool FindGlFromL(float l, out float gL)
        {
            try
            {
                gL = GetGlFromL(l);
                return true;
            }
            catch
            {
                gL = default;
                return false;
            }
        }

        #endregion

        #region Пиксели ↔ Миллиметры

        /// <summary>
        ///     Возвращает локальную координату L из локальной координаты X.
        /// </summary>
        /// <param name="x">Локальная координата x</param>
        /// <returns></returns>
        public float GetLFromX(int x)
        {
            if (x > Settings.WidthWindow)
                throw new Exception($"Локальная координата X = {x} не принадлежит диапазону датчика!");

            return x / Settings.KPixelsToMm;
        }

        /// <summary>
        ///     Ищет локальную координату L из локальной координаты X.
        ///     <para>True - если локальная координата L нашлась.</para>
        /// </summary>
        /// <param name="x">Локальная координата X</param>
        /// <param name="l">Локальная координата L</param>
        /// <returns>Локальная координата L нашлась?</returns>
        public bool FindLFromX(int x, out float l)
        {
            try
            {
                l = GetLFromX(x);
                return true;
            }
            catch
            {
                l = default;
                return false;
            }
        }

        /// <summary>
        ///     Возвращает локальную координату X из локальной координаты L.
        /// </summary>
        /// <param name="l">Локальная координата L</param>
        /// <returns></returns>
        public int GetXFromL(float l)
        {
            if (l > Settings.LWindow)
                throw new Exception($"Локальная координата L = {l} не принадлежит диапазону датчика!");

            return (int) Math.Round(l * Settings.KPixelsToMm, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        ///     Ищет локальную координату X из локальной координаты L.
        ///     <para>True - если локальная координата X нашлась.</para>
        /// </summary>
        /// <param name="l">Локальная координата L</param>
        /// <param name="x">Локальная координата X</param>
        /// <returns>Локальная координата X нашлась?</returns>
        public bool FindXFromL(float l, out int x)
        {
            try
            {
                x = GetXFromL(l);
                return true;
            }
            catch
            {
                x = default;
                return false;
            }
        }

        #endregion
    }
}