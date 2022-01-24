#region

using System;
using System.Collections.Concurrent;
using System.Reflection;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Настройки конфигураций
    /// </summary>
    public class ConfigSettings : ISettings
    {
        #region Поля

        private string[] _autoShiftSettings = new string[0];
        private byte _nAutoShifts;

        #endregion


        #region Свойства

        /// <summary>
        ///     Конфигурация системы
        /// </summary>
        public string SystemSettings { get; set; }

        /// <summary>
        /// Режим авто-смены конфигураций включен?
        /// </summary>
        public bool AutoShifts { get; set; }

        /// <summary>
        ///     Конфигурации авто-смен
        /// </summary>
        public string[] AutoShiftSettings
        {
            get => _autoShiftSettings;
            set => _autoShiftSettings = value;
        }

        /// <summary>
        ///     Количество авто-смен
        /// </summary>
        public byte NAutoShifts
        {
            get => _nAutoShifts;
            set
            {
                _nAutoShifts = value;
                Array.Resize(ref _autoShiftSettings, _nAutoShifts);
            }
        }

        #endregion

        #region Implementation of ISettings

        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства.
        /// </summary>
        public ConcurrentDictionary<string, object> Default { get; } = new ConcurrentDictionary<string, object>
        {
            [nameof(SystemSettings)] = Constants.DefIdSettings,
            [nameof(AutoShifts)] = false,
            [nameof(NAutoShifts)] = 1
        };

        /// <summary>
        /// Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        public ConcurrentDictionary<string, object[]> Limit { get; } = new ConcurrentDictionary<string, object[]>
        {
            [nameof(NAutoShifts)] = new object[] {0, 1000},
        };

        /// <summary>
        /// Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns></returns>
        public bool AllIsOk() => true;

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        public PropertyInfo[] PropertiesInfo { get; } = UtilsSettings.GetProperties(typeof(ConfigSettings));

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        public string TypeSettingsMessage { get; } = "конфигураций";

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        public string RootName { get; } = "Configurations";

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        public string MainElementName { get; } = "Configuration";

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        public string AttributeName { get; } = "name";

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        public string PathToDoc { get; } = UtilsSettings.GetPathToSettings(nameof(ConfigSettings));

        #endregion
    }
}