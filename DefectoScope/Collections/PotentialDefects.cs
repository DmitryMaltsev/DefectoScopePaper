namespace DefectoScope
{
    /// <summary>
    ///     Буфер последних потенциальных дефектов
    /// </summary>
    public class PotentialDefects : RecentItemsList<Defect>
    {
        /// <summary>
        ///     Создает буфер последних потенциальных дефетов с заданной максимальной емкостью
        /// </summary>
        /// <param name="maxCapacity"></param>
        public PotentialDefects(int maxCapacity) : base(maxCapacity, d => !d.IsRealDefect)
        {
        }
    }
}