#region

using System;
using System.Drawing;
using Kogerent;

#endregion

namespace DefectoScope
{
    /// <summary>
    ///     Дефект
    /// </summary>
    public class Defect
    {
        /// <summary>
        ///     Координата начала дефекта (верхний левый угол).
        /// </summary>
        public Point Begin = Point.Empty;

        /// <summary>
        ///     Координата конца дефекта (нижний правый угол).
        /// </summary>
        public Point End = Point.Empty;

        /// <summary>
        ///     Это осветление? (цвет дефекта светлее фона)
        /// </summary>
        public bool IsLight { get; set; }

        /// <summary>
        ///     Тип дефекта словом (потемнение или осветление)
        /// </summary>
        public string StrType => IsLight ? "Осветление" : "Потемнение";

        /// <summary>
        ///     Центральная точка дефекта
        /// </summary>
        public Point Center => new Point((End.X + Begin.X) / 2, (End.Y + Begin.Y) / 2);

        /// <summary>
        ///     Определяет ширину дефекта.
        /// </summary>
        public ushort Width => (ushort) (End.X - Begin.X);

        /// <summary>
        ///     Определяет ширину дефекта в мм
        /// </summary>
        public float L => UtilsD.GetGlFromGx(End.X) - UtilsD.GetGlFromGx(Begin.X);

        /// <summary>
        ///     Определяет длину дефекта.
        /// </summary>
        public ushort Length => (ushort) (End.Y - Begin.Y);

        /// <summary>
        ///     Определяет длину дефекта в мм.
        /// </summary>
        public float D
        {
            get
            {
                //Сраный шаман! Будьте прокляты...
                if (Length == 1) return Length * 2;
                //if (Length == 2) return Length * 3;
                //if (Length == 3) return Length * 4;

                return Length * G.Settings.MmInStep;
            }
        }

        /// <summary>
        ///     Границы дефекта в пикселях
        /// </summary>
        public Rectangle Rectangle => new Rectangle(Begin.X, Begin.Y, Width, Length);

        /// <summary>
        ///     Габаритная площадь (мм2)
        /// </summary>
        public float Square => L * D;

        /// <summary>
        ///     Сигнализировал о себе?
        /// </summary>
        public bool Alarmed { get; set; } = false;

        /// <summary>
        ///     Тип дефекта
        /// </summary>
        public TypeDefectSettings TypeDefect { get; private set; }

        /// <summary>
        ///     Это реальный дефект?
        /// </summary>
        public bool IsRealDefect => TypeDefect != null;

        /// <summary>
        /// Время обнаружения дефекта?
        /// </summary>
        public DateTime? DetectionTime { get; private set; }

        /// <summary>
        ///     Кадр дефекта
        /// </summary>
        public byte[,] Frame { get; private set; } = new byte[0, 0];

        /// <summary>
        ///     Определяет тип данного дефекта
        /// </summary>
        /// <returns>Тип определен?</returns>
        public bool IdentifyType()
        {
            foreach (var typeDefect in G.TypeDefects)
            {
                var typeFound = IsLight == typeDefect.Level && L >= typeDefect.Width && D >= typeDefect.Length &&
                                Square >= typeDefect.Square;

                if (typeFound)
                {
                    TypeDefect = typeDefect;
                    DetectionTime = DateTime.Now;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Смещает координату Y для дефекта
        /// </summary>
        /// <param name="swift">Смещение</param>
        public void SwiftY(int swift = 1)
        {
            Begin.Y += swift;
            End.Y += swift;
        }

        /// <summary>
        ///     Обновляет кадр дефекта
        /// </summary>
        public void UpdateFrame()
        {
            var nProfiles = G.ProfileBuffer.Count;
            const int indent = 50; //Отступ во все направления по пикселям

            var xBegin = Begin.X - indent;
            if (xBegin < 0)
                xBegin = 0;

            var lastIndex = G.ProfileSize - 1;

            var xEnd = End.X + indent;
            if (xEnd > lastIndex)
                xEnd = lastIndex;

            var yBegin = nProfiles - End.Y - indent;
            if (yBegin < 0)
                yBegin = 0;

            lastIndex = G.ProfileBuffer.Count - 1;

            var yEnd = nProfiles - Begin.Y + indent;
            if (yEnd > lastIndex)
                yEnd = lastIndex;

            Frame = UtilsD.ExtractionMatrix(G.ProfileBuffer, xBegin, xEnd, yBegin, yEnd);

            var time = DetectionTime ?? DateTime.Now;
            var file = KUtils.GetFileInfo(G.PathToPics, $"{time:yyyy-MM-dd_HH-mm-ss.fffffff}.bmp");

            Frame.ConvertByteMatrixToBitmap(Color.Black, Color.White).Save(file.FullName);
        }

        /// <summary>
        ///     Дефекты пересекаются?
        /// </summary>
        /// <param name="d1">Первый дефект</param>
        /// <param name="d2">Второй дефект</param>
        /// <returns></returns>
        public static bool Intersect(Defect d1, Defect d2) =>
            d1.Begin.X <= d2.End.X && d1.Begin.Y <= d2.End.Y &&
            d2.Begin.X <= d1.End.X && d2.Begin.Y <= d1.End.Y;

        /// <summary>
        ///     Дефекты пересекаются?
        /// </summary>
        /// <param name="defect">Дефект</param>
        /// <returns></returns>
        public bool IntersectWith(Defect defect) => Intersect(this, defect);

        public new string ToString()
        {
            var level = TypeDefect.Level;

            var xCoord = this.GetXCoordCenter(); //м
            var yCoord = this.GetYCoordCenter(); //м

            var s = Square;
            var sNom = TypeDefect.Square;
            var l = D;
            var lNom = TypeDefect.Length;
            var w = L;
            var wNom = TypeDefect.Width;


            //Записываем тип брака
            string type;
            //Если площадь больше 2кв.мм, то это крупный брак
            if (Square > 2)
                type = level ? "Дырка" : "Лепешка";
            else
                type = level ? "Отверстие" : "Включение";
            //Хрен знает зачем, но так было..
            if (xCoord < 0.02) type = "Трещина";

            var message = $"Дефект: {type} с центром в X = {xCoord:F2}м и Y = {yCoord:F2}м.\n" +
                          $"Параметры: Площадь = {s:F2} мм2 ({sNom:F2} мм2), Длина = {l:F2} мм ({lNom:F2} мм), Ширина = {w:F2} мм ({wNom:F2} мм).";

            return message;
        }
    }
}