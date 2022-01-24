#region

using System;
using System.Diagnostics;
using Kogerent;

#endregion

namespace DefectoScope
{
    public static class G
    {
        //public static int TempCount = 0;



        /// <summary>
        /// Период обновления графики в миллисекундах.
        /// 30 кадров в секунду - период 33.3 мс.
        /// 60 кадров в секунду - 16.7 мс.
        /// 20 мс - 50 кадров в секунду.
        /// 25 мс - 40 кадров в секунду.
        /// 30 мс - 33 кадра в секунду.
        /// </summary>
        public const int UpdatePeriodMs = 25;

        /// <summary>
        ///     Версия SensorE.dll
        /// </summary>
        public static string VersionDll;

        /// <summary>
        /// Папка логов
        /// </summary>
        public const string LogsFolder = "Logs";

        /// <summary>
        /// Путь к логам
        /// </summary>
        public static string PathToLogs { get; } = KUtils.GetStartUpFolder(LogsFolder);

        /// <summary>
        /// Папка настроек
        /// </summary>
        public const string SettingsFolder = "Settings";

        /// <summary>
        /// Путь к настройкам
        /// </summary>
        public static string PathToSettings { get; } = KUtils.GetStartUpFolder(SettingsFolder);


        /// <summary>
        /// Настройки конфигураций
        /// </summary>
        public static ConfigSettings ConfigSettings = new ConfigSettings();

        /// <summary>
        /// Папка отчетов
        /// </summary>
        public const string ReportsFolder = "Reports";

        /// <summary>
        /// Путь к отчетам
        /// </summary>
        public static string PathToReports { get; } = KUtils.GetStartUpFolder(ReportsFolder);

        /// <summary>
        /// Папка изображений дефектов
        /// </summary>
        public const string PicsFolder = "Pics";

        /// <summary>
        /// Путь к отчетам
        /// </summary>
        public static string PathToPics { get; } = KUtils.GetStartUpFolder(PicsFolder);

        /// <summary>
        ///     Список датчиков
        /// </summary>
        public static Sensor[] Sensors = new Sensor[0];

        /// <summary>
        ///     Входной буфер объединенных callback строк
        /// </summary>
        public static ReadWriteBuffer<byte[]> InputBuffer;

        /// <summary>
        /// Необходима ли работа входного буфера? (ключ выхода)
        /// </summary>
        public static bool NeedWorkInputBuffer;

        /// <summary>
        ///     Буфер строк
        /// </summary>
        public static ProfileBuffer ProfileBuffer;

        /// <summary>
        ///     Список всех найденных потенциальных дефектов внутри буфера
        /// </summary>
        public static PotentialDefects PotentialDefects = new PotentialDefects(100);

        /// <summary>
        /// Количество прошедших профилей в тамбуре
        /// </summary>
        public static int NProfiles;

        /// <summary>
        /// Левая граница полотна
        /// </summary>
        public static int Left;

        /// <summary>
        /// Правая граница полотна
        /// </summary>
        public static int Right;

        /// <summary>
        /// Средний уровень профиля
        /// </summary>
        public static byte Mean;

        /// <summary>
        /// Сбрасывает количество профилей в тамбуре (и другие переменные)
        /// </summary>
        public static void ResetNProfiles()
        {
            NProfiles = 0;
            Left = 0;
            Right = ProfileSize - 1;
            Mean = 128;
        }

        /// <summary>
        ///     Список всех реальных дефектов
        /// </summary>
        public static Defects Defects = new Defects(100);

        /// <summary>
        ///     COM-порт для работы с энкодером
        /// </summary>
        public static SyncController SyncController;

        /// <summary>
        /// Клиент для связи с сервером OPC
        /// </summary>
        public static OpcClient OpcClient;

        /// <summary>
        /// Клиент для связи с базой данных
        /// </summary>
        public static SqlClient SqlClient;

        /// <summary>
        ///     Логгер
        /// </summary>
        public static OFFLogger Logger;

        /// <summary>
        ///     Настройки системы
        /// </summary>
        public static SystemSettings Settings = new SystemSettings();

        /// <summary>
        ///     Список исключаемых зон
        /// </summary>
        public static ExcludedZoneSettings[] ExcludedZones = new ExcludedZoneSettings[0];

        /// <summary>
        ///     Список типов дефектов
        /// </summary>
        public static TypeDefectSettings[] TypeDefects = new TypeDefectSettings[0];

        /// <summary>
        ///     Список авто-смен
        /// </summary>
        public static AutoShiftSettings[] AutoShifts = new AutoShiftSettings[0];

        /// <summary>
        ///     Система исправна? (по умолчанию да)
        /// </summary>
        public static bool IsOk = true;

        /// <summary>
        ///     Размер профиля
        /// </summary>
        public static int ProfileSize;

        /// <summary>
        ///     Время сигнализации
        /// </summary>
        public static Stopwatch AlarmTime = new Stopwatch();

        /// <summary>
        ///     Работа от энкодера была запущена?
        /// </summary>
        public static bool Launched;

        /// <summary>
        /// Режим администратора? (изначально нет)
        /// </summary>
        public static bool IsAdministrator = false;

        /// <summary>
        /// События вызываются автоматически? (без пользователя)
        /// </summary>
        public static bool IsAuto = false;

        /// <summary>
        /// Сейчас в процессе калибровки?
        /// </summary>
        public static bool IsCalibration = false;

        /// <summary>
        /// Необходима работа после авто-смены?
        /// </summary>
        public static bool NeedWorkAfterAutoShift = false;

        /// <summary>
        /// Необходимый ID настроек системы
        /// </summary>
        public static string NeedIdSettings = SettingsUtils.DefIdSettings;

        /// <summary>
        /// Сигнал прерывания сигнализации
        /// </summary>
        public static bool StopAlarm = false;

        /// <summary>
        /// Текущая смена
        /// </summary>
        public static Shift Shift = new Shift();

        ///// <summary>
        ///// Номер тамбура
        ///// </summary>
        //public static int NTambour;
    }
}