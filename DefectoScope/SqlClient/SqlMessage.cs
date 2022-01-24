using System;

namespace DefectoScope
{
    public class SqlMessage
    {
        #region Конструкторы / Деструкторы

        /// <summary>
        /// Создает SQL-сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="nShift">Номер смены (0 - " ", 1 - "Пусковой")</param>
        /// <param name="nTambour">Номер тамбура</param>
        /// <param name="type">Тип дефекта (False - лепешка/включение, True - дырка/отверстие)</param>
        /// <param name="xCoord">Центр дефекта по X в мм</param>
        /// <param name="yCoord">Центр дефекта по Y в мм</param>
        /// <param name="s">Площадь дефекта, мм2</param>
        /// <param name="sNom">Минимальная площадь дефекта, мм2</param>
        /// <param name="l">Длина дефекта, мм</param>
        /// <param name="lNom">Минимальная длина дефекта, мм</param>
        /// <param name="w">Ширина дефекта, мм</param>
        /// <param name="wNom">Минимальная ширина дефекта, мм</param>
        public SqlMessage(DateTime timestamp, short nShift, int nTambour, bool type, decimal xCoord, decimal yCoord, decimal s, decimal sNom, decimal l, decimal lNom, decimal w, decimal wNom)
        {
            Timestamp = timestamp;
            NShift = nShift;
            NTambour = nTambour;
            Type = type;
            XCoord = xCoord;
            YCoord = yCoord;
            S = s;
            SNom = sNom;
            L = l;
            LNom = lNom;
            W = w;
            WNom = wNom;
        }

        #endregion

        #region Свойства

        /// <summary>
        ///     Отметка времени
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Номер смены (0 - " ", 1 - "Пусковой")
        /// </summary>
        public short NShift { get; }

        /// <summary>
        /// Номер тамбура
        /// </summary>
        public int NTambour { get; }

        /// <summary>
        /// Тип дефекта (False - лепешка/включение, True - дырка/отверстие)
        /// </summary>
        public bool Type { get; }

        /// <summary>
        /// Центр дефекта по X в м
        /// </summary>
        public decimal XCoord { get; }

        //float wp = ADOstGetTamburData->FieldByName("_x_nom")->Value;
        // if(xx<0.02 || xx> wp-0.02)
        // tmp = "Трещина";
        public decimal XNom { get; }

        /// <summary>
        /// Центр дефекта по Y в м
        /// </summary>
        public decimal YCoord { get; }

        /// <summary>
        /// Площадь дефекта, мм2
        /// </summary>
        public decimal S { get; }

        /// <summary>
        /// Минимальная площадь дефекта, мм2
        /// </summary>
        public decimal SNom { get; }

        /// <summary>
        /// Длина дефекта, мм
        /// </summary>
        public decimal L { get; }

        /// <summary>
        /// Минимальная длина дефекта, мм
        /// </summary>
        public decimal LNom { get; }

        /// <summary>
        /// Ширина дефекта, мм
        /// </summary>
        public decimal W { get; }

        /// <summary>
        /// Минимальная ширина дефекта, мм
        /// </summary>
        public decimal WNom { get; }

        #endregion
    }
}