namespace DefectoScope
{
    /// <summary>
    /// Подсчитанные времена
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Необходим счет времени?
        /// </summary>
        public static bool IsNeed = false;

        /// <summary>
        /// Время получения строк из буферов
        /// </summary>
        public static double GetRows;

        /// <summary>
        /// Время объединения строк в одну
        /// </summary>
        public static double ConcatAllRows;

        /// <summary>
        /// Время добавления строки во входной буфер
        /// </summary>
        public static double AddInputBuffer;

        ///// <summary>
        ///// Время добавления строки в буфер
        ///// </summary>
        //public static double AddRowsBuffer;

        /// <summary>
        /// Время отрисовки буфера
        /// </summary>
        public static double AddRowsBufferForm;

        /// <summary>
        /// Время цикла
        /// </summary>
        public static double Cycle;
    }
}