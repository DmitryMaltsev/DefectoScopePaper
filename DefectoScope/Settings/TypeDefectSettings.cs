using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Reflection;

namespace DefectoScope
{
    /// <summary>
    /// Настройки типа дефекта
    /// </summary>
    public class TypeDefectSettings : ISettings
    {
        #region Свойства

        /// <summary>
        /// Уровень светлый?
        /// </summary>
        public bool Level { get; set; }

        /// <summary>
        /// Площадь дефекта (мм2)
        /// </summary>
        public float Square { get; set; }

        /// <summary>
        /// Длина дефекта (мм)
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Ширина дефекта (мм)
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Графический маркер дефекта
        /// </summary>
        public Marker Marker { get; set; }

        /// <summary>
        /// Цвет графического маркера (словом)
        /// </summary>
        public KnownColor MarkerKnownColor { get; set; }

        /// <summary>
        /// Возвращает цвет графического маркера
        /// </summary>
        public Color MarkerColor => Color.FromKnownColor(MarkerKnownColor);

        /// <summary>
        /// Показывать границу дефекта?
        /// </summary>
        public bool ShowRectangle { get; set; }

        /// <summary>
        /// Ширина границы
        /// </summary>
        public float WidthRectangle { get; set; }

        #endregion

       #region Implementation of ISettings

        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства.
        /// </summary>
        public ConcurrentDictionary<string, object> Default { get; } = new ConcurrentDictionary<string, object>
        {
            [nameof(Level)] = false,
            [nameof(Square)] = 0,
            [nameof(Length)] = 0,
            [nameof(Width)] = 0,
            [nameof(Marker)] = Marker.Square,
            [nameof(MarkerKnownColor)] = KnownColor.Red
        };

        /// <summary>
        /// Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        public ConcurrentDictionary<string, object[]> Limit { get; } = new ConcurrentDictionary<string, object[]>
        {
            [nameof(Square)] = new object[] { 0, 1000000 },
            [nameof(Length)] = new object[] { 0, 1000000 },
            [nameof(Width)] = new object[] { 0, 1000000 },
        };

        /// <summary>
        /// Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns></returns>
        public bool AllIsOk()
        {
            var success = Enum.IsDefined(typeof(Marker), Marker);
            if (!success)
                G.Logger.Fatal($"{nameof(TypeDefectSettings)}: В файле настроек {TypeSettingsMessage} параметр {nameof(Marker)} ({Id}) имеет недопустимое по логике значение.");

            success = success && this.NumbersIsOk() && this.KnownColorsIsOk();
            return success;
        }

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        public PropertyInfo[] PropertiesInfo { get; } = UtilsSettings.GetProperties(typeof(TypeDefectSettings));

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        public string TypeSettingsMessage { get; } = "типа дефекта";

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        public string RootName { get; } = "TypeDefects";

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        public string MainElementName { get; } = "TypeDefect";

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        public string AttributeName { get; } = "name";

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        public string PathToDoc { get; } = UtilsSettings.GetPathToSettings(nameof(TypeDefectSettings));

        #endregion
    }
}