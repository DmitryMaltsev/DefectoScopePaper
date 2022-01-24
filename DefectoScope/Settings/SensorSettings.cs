#region

using System.Collections.Concurrent;
using System.Reflection;


#endregion

namespace DefectoScope
{
    /// <summary>
    ///     Настройки датчика
    /// </summary>
    public class SensorSettings : ISettings
    {
        #region Свойства

        /// <summary>
        ///     Ip-адрес датчика
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        ///     Левая граница окна
        /// </summary>
        public ushort LeftWindow { get; set; }

        /// <summary>
        ///     Верхняя граница окна
        /// </summary>
        public ushort TopWindow { get; set; }

        /// <summary>
        ///     Ширина окна
        /// </summary>
        public ushort WidthWindow { get; set; }

        /// <summary>
        ///     Высота окна
        /// </summary>
        public ushort HeightWindow { get; set; }

        /// <summary>
        ///     Экспозиция
        /// </summary>
        public ushort Exposition { get; set; }

        /// <summary>
        ///     Цифровое усиление
        /// </summary>
        public byte AdcGain { get; set; }

        /// <summary>
        ///     Аналоговое усиление
        /// </summary>
        public byte PgaGain { get; set; }

        /// <summary>
        ///     Коэффициент отношения пикселей к миллиметру. Сколько пикселей в 1 мм?
        /// </summary>
        public float KPixelsToMm { get; set; }

        /// <summary>
        ///     Возвращает ширину окна в миллиметрах
        /// </summary>
        public double LWindow => WidthWindow / KPixelsToMm;

        #endregion

        #region Implementation of ISettings

        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства.
        /// </summary>
        public ConcurrentDictionary<string, object> Default { get; } = new ConcurrentDictionary<string, object>
        {
            [nameof(Ip)] = "192.168.113.1",
            [nameof(LeftWindow)] = 0,
            [nameof(TopWindow)] = 0,
            [nameof(WidthWindow)] = Sensor.SizeX,
            [nameof(HeightWindow)] = 2,
            [nameof(Exposition)] = 100,
            [nameof(AdcGain)] = 40,
            [nameof(PgaGain)] = 0,
            [nameof(KPixelsToMm)] = 1f
        };

        /// <summary>
        /// Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        public ConcurrentDictionary<string, object[]> Limit { get; } = new ConcurrentDictionary<string, object[]>
        {
            [nameof(LeftWindow)] = new object[] { 0, Sensor.SizeX },
            [nameof(TopWindow)] = new object[] { 0, Sensor.SizeY },
            [nameof(WidthWindow)] = new object[] { 1, Sensor.SizeX },
            [nameof(HeightWindow)] = new object[] { 2, Sensor.SizeY },
            [nameof(Exposition)] = new object[] { 0, 10000 },
            [nameof(AdcGain)] = new object[] { 0, 255 },
            [nameof(PgaGain)] = new object[] { 0, 3 },
            [nameof(KPixelsToMm)] = new object[] { 0.01f, 5 },
        };

        /// <summary>
        /// Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns>Конфигурация без ошибок?</returns>
        public bool AllIsOk()
        {
            var success = UtilsD.IsValidIp(Ip, out var newIp);
            if (success)
                Ip = newIp;
            else
                G.Logger.Fatal($"{nameof(SensorSettings)}: В файле настроек {TypeSettingsMessage} параметр {nameof(Ip)} ({Id}) имеет недопустимое по логике значение.");

            success = success && this.NumbersIsOk();

            return success;
        }

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        public PropertyInfo[] PropertiesInfo { get; } = UtilsSettings.GetProperties(typeof(SensorSettings));

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        public string TypeSettingsMessage { get; } = "датчика";

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        public string RootName { get; } = "Devices";

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        public string MainElementName { get; } = "Sensor";

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        public string AttributeName { get; } = "name";

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        public string PathToDoc { get; set; } = UtilsSettings.GetPathToSettings(nameof(SensorSettings));

        #endregion
    }
}