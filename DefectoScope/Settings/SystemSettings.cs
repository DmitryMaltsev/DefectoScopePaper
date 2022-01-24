using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Reflection;
using Kogerent;

namespace DefectoScope
{
    /// <summary>
    /// Настройки программы
    /// </summary>
    public class SystemSettings : ISettings
    {
        #region Поля
        private string[] _sensorSettings = new string[0];
        private byte _nSensors;
        private string[] _excludedZones = new string[0];
        private byte _nExcludedZones;
        private string[] _typeDefects = new string[0];
        private byte _nTypeDefects;

        #endregion

        #region Свойства

        /// <summary>
        ///     Конфигурации датчиков
        /// </summary>
        public string[] SensorSettings
        {
            get => _sensorSettings;
            set => _sensorSettings = value;
        }

        /// <summary>
        ///     Количество датчиков
        /// </summary>
        public byte NSensors
        {
            get => _nSensors;
            set
            {
                _nSensors = value;
                Array.Resize(ref _sensorSettings, _nSensors);
            }
        }

        /// <summary>
        /// Конфигурации исключаемых зон
        /// </summary>
        public string[] ExcludedZones
        {
            get => _excludedZones;
            set => _excludedZones = value;
        }

        /// <summary>
        /// Количество исключаемых зон
        /// </summary>
        public byte NExcludedZones
        {
            get => _nExcludedZones;
            set
            {
                _nExcludedZones = value;
                Array.Resize(ref _excludedZones, _nExcludedZones);
            }
        }

        /// <summary>
        /// Конфигурации типов дефектов
        /// </summary>
        public string[] TypeDefects
        {
            get => _typeDefects;
            set => _typeDefects = value;
        }

        /// <summary>
        /// Количество типов дефектов
        /// </summary>
        public byte NTypeDefects
        {
            get => _nTypeDefects;
            set
            {
                _nTypeDefects = value;
                Array.Resize(ref _typeDefects, _nTypeDefects);
            }
        }

        /// <summary>
        /// Имя COM-порта
        /// </summary>
        public string ComPortName { get; set; }

        /// <summary>
        /// Строб каждые N миллиметров
        /// </summary>
        public float StrobeEveryNMm { get; set; }

        /// <summary>
        /// Делитель энкодера
        /// </summary>
        public ushort EncoderDivider { get; set; }

        /// <summary>
        /// Возвращает количество миллиметров между стробами с учетом энкодера
        /// </summary>
        public float MmInStep => StrobeEveryNMm * EncoderDivider;

        ///// <summary>
        ///// Период опроса датчиков в режиме программного запуска в миллисекундах.
        ///// Сначала 0 и нечетные, затем через указанный период четные, и так далее
        ///// (т.е. опрос всех датчиков через удвоенный период)
        ///// </summary>
        //public ushort PeriodStartProgram { get; set; }

        /// <summary>
        /// Световая и звуковая сигнализация?
        /// </summary>
        public bool Alarm { get; set; }

        /// <summary>
        /// Длительность сигнализации в секундах
        /// </summary>
        public byte AlarmDuration { get; set; }

        /// <summary>
        /// Файл калибровки
        /// </summary>
        public string CalibrationFileName { get; set; }

        /// <summary>
        /// Цвет линии опорного массива (словом)
        /// </summary>
        public KnownColor CalibrationDataKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет линии опорного массива
        /// </summary>
        public Color CalibrationDataColor => Color.FromKnownColor(CalibrationDataKnownColor);

        /// <summary>
        /// Цвет шумовых областей (словом)
        /// </summary>
        public KnownColor NoiseKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет шумовых областей
        /// </summary>
        public Color NoiseColor => Color.FromKnownColor(NoiseKnownColor);

        /// <summary>
        /// Допуск значений для темного (%)
        /// </summary>
        public byte ToleranceDark { get; set; }

        /// <summary>
        /// Цвет линии порогового массива для темного (словом)
        /// </summary>
        public KnownColor ToleranceDarkKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет линии порогового массива для темного
        /// </summary>
        public Color ToleranceDarkColor => Color.FromKnownColor(ToleranceDarkKnownColor);

        /// <summary>
        /// Цвет темных областей (словом)
        /// </summary>
        public KnownColor DarkAreaKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет темных областей
        /// </summary>
        public Color DarkAreaColor => Color.FromKnownColor(DarkAreaKnownColor);

        /// <summary>
        /// Допуск значений для светлого (%)
        /// </summary>
        public byte ToleranceLight { get; set; }

        /// <summary>
        /// Цвет линии порогового массива для светлого (словом)
        /// </summary>
        public KnownColor ToleranceLightKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет линии порогового массива для светлого
        /// </summary>
        public Color ToleranceLightColor => Color.FromKnownColor(ToleranceLightKnownColor);

        /// <summary>
        /// Цвет светлых областей (словом)
        /// </summary>
        public KnownColor LightAreaKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет светлых областей
        /// </summary>
        public Color LightAreaColor => Color.FromKnownColor(LightAreaKnownColor);

        /// <summary>
        /// Количество калибровочных профилей
        /// </summary>
        public ushort NCalibrationProfiles { get; set; }

        /// <summary>
        /// Цвет профиля (словом)
        /// </summary>
        public KnownColor ProfileKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет профиля
        /// </summary>
        public Color ProfileColor => Color.FromKnownColor(ProfileKnownColor);

        /// <summary>
        /// Дальность карты дефектов (мм)
        /// </summary>
        public float MapRange { get; set; }

        /// <summary>
        /// Отображать маркерную линию на экране?
        /// </summary>
        public bool MarkerLine { get; set; }

        /// <summary>
        /// Позиция маркерной линии
        /// </summary>
        public float MarkerLinePosition { get; set; }

        /// <summary>
        /// Цвет маркерной линии (словом)
        /// </summary>
        public KnownColor MarkerLineKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет маркерной линии
        /// </summary>
        public Color MarkerLineColor => Color.FromKnownColor(MarkerLineKnownColor);

        /// <summary>
        /// Ширина маркерной линии
        /// </summary>
        public byte MarkerLineWidth { get; set; }

        /// <summary>
        /// Количество строк на карте дефектов
        /// </summary>
        public byte NMapRows { get; set; }

        /// <summary>
        /// Количество столбцов на карте дефектов
        /// </summary>
        public byte NMapColumns { get; set; }

        /// <summary>
        /// Размер маркеров
        /// </summary>
        public byte MarkerSize { get; set; }

        /// <summary>
        /// Размер входного буфера профилей
        /// </summary>
        public int InputBufferSize { get; set; }

        /// <summary>
        /// Размер буфера профилей
        /// </summary>
        public ushort ProfileBufferSize { get; set; }

        /// <summary>
        /// Отображать шкалу на экране?
        /// </summary>
        public bool ScaleLine { get; set; }

        ///// <summary>
        ///// Позиция шкалы
        ///// </summary>
        //public ushort ScaleLinePosition { get; set; }

        /// <summary>
        /// Количество делений на шкале
        /// </summary>
        public byte NScaleLineDivisions { get; set; }

        /// <summary>
        /// Цвет шкалы (словом)
        /// </summary>
        public KnownColor ScaleLineKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет шкалы
        /// </summary>
        public Color ScaleLineColor => Color.FromKnownColor(ScaleLineKnownColor);

        /// <summary>
        /// Ширина шкалы
        /// </summary>
        public byte ScaleLineWidth { get; set; }

        /// <summary>
        /// Записывать информацию для слежения за участками кода в лог?
        /// </summary>
        public bool LogTrace { get; set; }

        /// <summary>
        /// Записывать отладочную информацию в лог?
        /// </summary>
        public bool LogDebug { get; set; }

        /// <summary>
        /// Записывать обычную информацию в лог?
        /// </summary>
        public bool LogInfo { get; set; }

        /// <summary>
        /// Записывать предупреждения и странное поведение в лог?
        /// </summary>
        public bool LogWarn { get; set; }

        /// <summary>
        /// Записывать ошибки операций в лог?
        /// </summary>
        public bool LogError { get; set; }

        /// <summary>
        /// Записывать фатальные ошибки программы в лог?
        /// </summary>
        public bool LogFatal { get; set; }

        ///// <summary>
        ///// Необходимы рабочие датчики?
        ///// </summary>
        //public bool WorkingSensors { get; set; }

        /// <summary>
        ///     Отладочный режим (в dll и не только)
        /// </summary>
        public bool DebugMode { get; set; }

        /// <summary>
        /// Окно зума включено?
        /// </summary>
        public bool Zoom { get; set; }

        /// <summary>
        /// Отметки в миллиметрах?
        /// </summary>
        public bool LabelsInMm { get; set; }

        /// <summary>
        /// Строка подключения к базе данных MS SQL
        /// </summary>
        public string SqlConnectionString { get; set; }

        /// <summary>
        /// Записывать данные в базу данных?
        /// </summary>
        public bool WriteInDatabase { get; set; }

        /// <summary>
        /// Проверять связь с базой данных при инициализации?
        /// </summary>
        public bool InitTestOpenDB { get; set; }

        /// <summary>
        /// Использовать авто-нахождение границ?
        /// </summary>
        public bool AutoBorders { get; set; }

        /// <summary>
        /// Проверять состояние граничных дефектов в режиме авто-границ?
        /// </summary>
        public bool CheckAutoBorderDefects { get; set; }

        /// <summary>
        /// Проверять состояние границ?
        /// </summary>
        public bool CheckAutoBorders => AutoBorders && CheckAutoBorderDefects;

        /// <summary>
        /// Калибровка без дефектов? (не обнаруживать дефекты, пока открыто окно калибровки)
        /// </summary>
        public bool CalibrationWithoutDefects { get; set; }

        /// <summary>
        /// Использовать автосоздание отчета в конце смены?
        /// </summary>
        public bool AutoCreateReport { get; set; }

        #endregion

        #region Implementation of ISettings

        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства.
        /// </summary>
        public ConcurrentDictionary<string, object> Default { get; } = new ConcurrentDictionary<string, object>
        {
            [nameof(NSensors)] = 1,
            [nameof(NExcludedZones)] = 2,
            [nameof(NTypeDefects)] = 2,

            [nameof(ComPortName)] = "COM1",
            [nameof(StrobeEveryNMm)] = 2.48185825f,
            [nameof(EncoderDivider)] = 3,
            //[nameof(PeriodStartProgram)] = 15,
            [nameof(Alarm)] = true,
            [nameof(AlarmDuration)] = 5,

            [nameof(CalibrationFileName)] = Calibration.DefCalibrationFileName,
            [nameof(CalibrationDataKnownColor)] = KnownColor.Black,
            [nameof(NoiseKnownColor)] = KnownColor.Black,
            [nameof(ToleranceDark)] = 20,
            [nameof(ToleranceDarkKnownColor)] = KnownColor.Blue,
            [nameof(DarkAreaKnownColor)] = KnownColor.Black,
            [nameof(ToleranceLight)] = 20,
            [nameof(ToleranceLightKnownColor)] = KnownColor.Blue,
            [nameof(LightAreaKnownColor)] = KnownColor.White,
            [nameof(NCalibrationProfiles)] = 10,
            [nameof(ProfileKnownColor)] = KnownColor.Red,

            [nameof(MapRange)] = 60000,
            [nameof(MarkerLine)] = true,
            [nameof(MarkerLinePosition)] = 30000,
            [nameof(MarkerLineKnownColor)] = KnownColor.Orange,
            [nameof(MarkerLineWidth)] = 2,
            [nameof(NMapRows)] = 10,
            [nameof(NMapColumns)] = 10,
            [nameof(MarkerSize)] = 4,

            [nameof(InputBufferSize)] = 1000,
            [nameof(ProfileBufferSize)] = 300,

            [nameof(ScaleLine)] = true,
            //[nameof(ScaleLinePosition)] = 280 ,
            [nameof(NScaleLineDivisions)] = 4,
            [nameof(ScaleLineKnownColor)] = KnownColor.Gold,
            [nameof(ScaleLineWidth)] = 2,

            [nameof(LogTrace)] = true,
            [nameof(LogDebug)] = true,
            [nameof(LogInfo)] = true,
            [nameof(LogWarn)] = true,
            [nameof(LogError)] = true,
            [nameof(LogFatal)] = true,

            //[nameof(WorkingSensors)] = false,
            [nameof(DebugMode)] = false,
            [nameof(Zoom)] = true ,
            [nameof(LabelsInMm)] = true,

            [nameof(SqlConnectionString)] = "Data Source = DEFECTOSCOP\\DEFECTOSCOP;Initial Catalog = _tbf;User ID = sa;Password = Z6GEN",
            [nameof(WriteInDatabase)] = true,
            [nameof(InitTestOpenDB)] = true,

            [nameof(AutoBorders)] = true,
            [nameof(CheckAutoBorderDefects)] = true,
            [nameof(CalibrationWithoutDefects)] = true,
            [nameof(AutoCreateReport)] = true,
        };

        /// <summary>
        /// Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        public ConcurrentDictionary<string, object[]> Limit { get; } = new ConcurrentDictionary<string, object[]>
        {
            [nameof(NSensors)] = new object[] { 1, 9 },
            [nameof(NExcludedZones)] = new object[] { 0, 10 },
            [nameof(NTypeDefects)] = new object[] { 1, 10 },

            [nameof(StrobeEveryNMm)] = new object[] { 0.01f, 10 },
            [nameof(EncoderDivider)] = new object[] {1, 10000},
            //[nameof(PeriodStartProgram)] = new object[] { 10, 10000 },
            [nameof(AlarmDuration)] = new object[] { 1, 60 },

            [nameof(ToleranceDark)] = new object[] { 0, 100 },
            [nameof(ToleranceLight)] = new object[] { 0, 100 },
            [nameof(NCalibrationProfiles)] = new object[] { 10, 1000 },

            [nameof(ProfileBufferSize)] = new object[] { 100, 1000 },
            [nameof(InputBufferSize)] = new object[] { 100, 100000 },

            [nameof(MapRange)] = new object[] { 10000, 1000000 },
            [nameof(MarkerLinePosition)] = new object[] { 5000, 1000000 },
            [nameof(MarkerLineWidth)] = new object[] { 1, 10 },
            [nameof(NMapRows)] = new object[] { 1, 20 },
            [nameof(NMapColumns)] = new object[] { 1, 20 },
            [nameof(MarkerSize)] = new object[] { 1, 10 },
            
            //[nameof(ScaleLinePosition)] = new object[] { 0, 1000 },
            [nameof(NScaleLineDivisions)] = new object[] { 0, 20 },
            [nameof(ScaleLineWidth)] = new object[] { 1, 10 },
        };

        /// <summary>
        /// Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns></returns>
        public bool AllIsOk()
        {
            var success = UtilsD.IsValidComPortName(ComPortName, out var newComPortName);
            if (success)
                ComPortName = newComPortName;
            else
                G.Logger.Fatal($"{nameof(SystemSettings)}: В файле настроек {TypeSettingsMessage} параметр {nameof(ComPortName)} ({Id}) имеет недопустимое по логике значение.");

            success &= KUtils.IsValidFileName(CalibrationFileName, out var newCalibrationFileName);
            if (success)
                CalibrationFileName = newCalibrationFileName;
            else
                G.Logger.Fatal($"{nameof(SystemSettings)}: В файле настроек {TypeSettingsMessage} параметр {nameof(CalibrationFileName)} ({Id}) имеет недопустимое по логике значение.");

            success = success && this.NumbersIsOk() && this.KnownColorsIsOk();
            return success;
        }

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        public PropertyInfo[] PropertiesInfo { get; } = UtilsSettings.GetProperties(typeof(SystemSettings));

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        public string TypeSettingsMessage { get; } = "системы";

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        public string RootName { get; } = "System";

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        public string MainElementName { get; } = "Program";

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        public string AttributeName { get; } = "name";

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        public string PathToDoc { get; } = UtilsSettings.GetPathToSettings(nameof(SystemSettings));

        #endregion
    }
}