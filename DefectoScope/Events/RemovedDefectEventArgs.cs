namespace DefectoScope
{
    public class RemovedDefectEventArgs
    {
        /// <summary>
        /// Индекс удаленного элемента из списка
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Индекс удаленного элемента из списка</param>
        internal RemovedDefectEventArgs(int index) => Index = index;
    }
}