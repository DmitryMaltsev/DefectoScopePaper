#region

using System.Collections.Concurrent;
using System.Drawing;
using System.Reflection;

#endregion

namespace DefectoScope
{
    /// <summary>
    ///     Настройки исключаемой зоны
    /// </summary>
    public class ExcludedZoneSettings : ISettings
    {
        #region Поля

        private float _left;
        private float _right;

        #endregion

        #region Свойства

        /// <summary>
        ///     Левая сторона зоны (начало), пиксели
        /// </summary>
        public int LeftX { get; private set; }

        /// <summary>
        ///     Левая сторона зоны (начало), мм
        /// </summary>
        public float Left
        {
            get => _left;
            set
            {
                _left = value;

                //Округляем правую границу до предельно возможного, если это необходимо
                var success = UtilsD.FindGxFromGl(value, out var gX);
                if (!success) gX = G.ProfileSize - 1;
                LeftX = gX;
            }
        }

        /// <summary>
        ///     Правая сторона зоны (конец), пиксели
        /// </summary>
        public int RightX { get; private set; }

        /// <summary>
        ///     Правая сторона зоны (конец), мм
        /// </summary>
        public float Right
        {
            get => _right;
            set
            {
                _right = value;

                //Округляем правую границу до предельно возможного, если это необходимо
                var success = UtilsD.FindGxFromGl(value, out var gX);
                if (!success) gX = G.ProfileSize - 1;
                RightX = gX;
            }
        }

        /// <summary>
        ///     Цвет графического представления (словом)
        /// </summary>
        public KnownColor FillKnownColor { get; set; }

        /// <summary>
        ///     Цвет графического представления
        /// </summary>
        public Color FillColor => Color.FromKnownColor(FillKnownColor);

        #endregion

        #region Implementation of ISettings

        /// <summary>
        ///     Свойства по умолчанию. Ключ - имя свойства, значение - значение по умолчанию для данного свойства.
        /// </summary>
        public ConcurrentDictionary<string, object> Default { get; } = new ConcurrentDictionary<string, object>
        {
            [nameof(Left)] = 0,
            [nameof(Right)] = 0,
            [nameof(FillKnownColor)] = KnownColor.Orange
        };

        /// <summary>
        ///     Допустимые границы числовых свойств. Ключ - имя свойства, значение - массив из двух чисел (минимума и максимума)
        /// </summary>
        public ConcurrentDictionary<string, object[]> Limit { get; } = new ConcurrentDictionary<string, object[]>
        {
            [nameof(Left)] = new object[] {0, 100000},
            [nameof(Right)] = new object[] {0, 100000}
        };

        /// <summary>
        ///     Все параметры в порядке? (логическая проверка)
        /// </summary>
        /// <returns></returns>
        public bool AllIsOk() => this.NumbersIsOk() && this.KnownColorsIsOk();

        /// <summary>
        ///     Список публичных нестатических свойств (параметров настроек)
        /// </summary>
        public PropertyInfo[] PropertiesInfo { get; } = UtilsSettings.GetProperties(typeof(ExcludedZoneSettings));

        /// <summary>
        ///     Идентификатор конфигурации настроек
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Ответ на вопрос "Настройки чего?" (в родительном падеже)
        /// </summary>
        public string TypeSettingsMessage { get; } = "исключаемой зоны";

        /// <summary>
        ///     Имя корневого элемента в документе настроек
        /// </summary>
        public string RootName { get; } = "ExcludedZones";

        /// <summary>
        ///     Имя главного элемента конфигурации настроек
        /// </summary>
        public string MainElementName { get; } = "ExcludedZone";

        /// <summary>
        ///     Имя атрибута конфигурации настроек
        /// </summary>
        public string AttributeName { get; } = "name";

        /// <summary>
        ///     Путь до документа настроек
        /// </summary>
        public string PathToDoc { get; } = UtilsSettings.GetPathToSettings(nameof(ExcludedZoneSettings));

        #endregion
    }
}