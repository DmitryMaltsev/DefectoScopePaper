using System.Drawing;

namespace DefectoScope
{
    /// <summary>
    /// Деление шкалы с меткой
    /// </summary>
    public struct ScalePoint
    {
        /// <summary>
        /// Расположение деления шкалы
        /// </summary>
        public Point P { get; }

        /// <summary>
        /// Метка точки шкалы
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Расположение метки
        /// </summary>
        public Point LabelP { get; }

        /// <summary>
        /// Равнение метки относительно точки расположения
        /// </summary>
        public StringFormat LabelFormat { get; }

        /// <summary>
        /// Создает точку шкалы с меткой
        /// </summary>
        /// <param name="p">Расположение деления шкалы</param>
        /// <param name="label">Метка точки шкалы</param>
        /// <param name="labelP">Расположение метки</param>
        /// <param name="labelFormat">Равнение метки относительно точки расположения</param>
        public ScalePoint(Point p, string label, Point labelP, StringFormat labelFormat)
        {
            P = p;
            Label = label;
            LabelP = labelP;
            LabelFormat = labelFormat;
        }
    }
}