namespace DefectoScope
{
    public class AddedDefectEventArgs
    {
        /// <summary>
        /// Добавленный дефект
        /// </summary>
        public Defect Defect { get; }

        /// <summary>
        /// Индекс добавленного элемента в списке
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defect">Добавленный дефект</param>
        /// <param name="index">Индекс добавленного элемента в списке</param>
        internal AddedDefectEventArgs(Defect defect, int index)
        {
            Defect = defect;
            Index = index;
        }
    }
}