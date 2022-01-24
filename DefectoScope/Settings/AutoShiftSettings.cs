using System.Collections.Concurrent;
using System.Reflection;

namespace DefectoScope
{
    /// <summary>
    /// Настройки авто-смены конфигурации
    /// </summary>
    public class AutoShiftSettings : ISettings
    {
        #region Свойства
        
        /// <summary>
        ///     Левая сторона (начало), г/м2
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        ///     Правая сторона (конец), г/м2
        /// </summary>
        public float Right { get; set; }

        /// <summary>
        ///     Идентификатор конфигурации системы на который происходит смена
        /// </summary>
        public string SystemSettingId { get; set; }

        #endregion

        #region Implementation of ISettings

        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства.
        /// </summary>
        public ConcurrentDictionary<string, object> Default { get; } = new ConcurrentDictionary<string, object>
        {
            [nameof(Left)] = 0,
            [nameof(Right)] = 0,
            [nameof(SystemSettingId)] = Constants.DefIdSettings
        };

        /// <summary>
        ///     Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        public ConcurrentDictionary<string, object[]> Limit { get; } = new ConcurrentDictionary<string, object[]>
        {
            [nameof(Left)] = new object[] { 0, 100000 },
            [nameof(Right)] = new object[] { 0, 100000 }
        };

        /// <summary>
        ///     Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns></returns>
        public bool AllIsOk() => this.NumbersIsOk() && this.KnownColorsIsOk();

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        public PropertyInfo[] PropertiesInfo { get; } = UtilsSettings.GetProperties(typeof(AutoShiftSettings));

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        public string TypeSettingsMessage { get; } = "авто-смены";

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        public string RootName { get; } = "AutoShifts";

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        public string MainElementName { get; } = "AutoShift";

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        public string AttributeName { get; } = "name";

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        public string PathToDoc { get; } = UtilsSettings.GetPathToSettings(nameof(AutoShiftSettings));

        #endregion
    }
}