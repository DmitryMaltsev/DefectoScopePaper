#region

using System;
using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно ошибок
    /// </summary>
    public partial class ErrorForm : Form
    {
        /// <summary>
        /// Создает окно ошибок
        /// </summary>
        public ErrorForm() => InitializeComponent();

        #region Логирование

        /// <summary>
        ///     Логирует необходимый текст в listbox на главной форме.
        /// </summary>
        /// <param name="text">Логируемый текст</param>
        public void Log(string text)
        {
            //G.Logger.WriteToLog("Вошли в Log");

            var now = DateTime.Now; //Запоминаем текущую дату и время

            lock (lbLog)
            //Записываем текущее время и затем необходимый текст
                lbLog.TopIndex = lbLog.Items.Add(now.ToString("HH:mm:ss:fff: ") + text);

            //G.Logger.Info(text);
        }

        #endregion

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            var sensorsIsOk = UtilsD.SensorsIsOk(out var status);
            var uniqueIpIsOk = UtilsSettings.CheckUniqueIpLoadedSensors();
            var calibrationIsOk = Calibration.IsOk();
            var syncIsOk = G.SyncController != null && G.SyncController.IsOk;
            var opcIsOk = G.OpcClient != null && G.OpcClient.IsOk;
            var opcIsGood = G.OpcClient != null && G.OpcClient.GoodData;
            var sqlIsOk = G.SqlClient != null && G.SqlClient.IsOk;
            var meanIsOk = UtilsD.MeanIsOk();

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
                            Log($"Датчик №{i + 1} с конфигурацией {sensor.Settings.Id} (IP = {sensor.Settings.Ip}) не был инициализирован!");

                            break;
                        case SensorStatus.Bad:
                            Log($"Датчик №{i + 1} с конфигурацией {sensor.Settings.Id} (IP = {sensor.Settings.Ip}) неисправен!");

                            break;
                    }
                }
            }

            if (!uniqueIpIsOk)
                Log("IP адреса созданных объектов датчиков совпадают! Измените их в настройках!");

            if (!calibrationIsOk)
                Log("Загруженная калибровка непригодна для текущей конфигурации системы!");

            if (!syncIsOk)
                Log("Нет связи с контроллером синхронизации!");

            if (!opcIsOk)
                Log("Нет связи с OPC сервером!");

            if (!opcIsGood)
                Log("Не удается считать данные с OPC-сервера!");

            if (!sqlIsOk)
                Log("Нет связи с базой данных!");

            if (!meanIsOk)
                Log("Программа не видит полотна! (не средний уровень освещенности)");
        }

        private void ErrorForm_FormClosing(object sender, FormClosingEventArgs e) => 
            G.Logger.Info($"{nameof(ErrorForm)}: Пользователь закрыл окно");
    }
}