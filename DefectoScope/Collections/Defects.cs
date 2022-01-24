namespace DefectoScope
{
    public class Defects : RecentItemsList<Defect>
    {
        public event AddedDefectEventHandler AddedDefect;
        public event RemovedDefectEventHandler RemovedDefect;

        /// <inheritdoc />
        /// <summary>
        /// Создает буфер последних элементов с заданной максимальной емкостью
        /// </summary>
        /// <param name="maxCapacity"></param>
        public Defects(int maxCapacity) : base(maxCapacity) => RemovedItem += OnRemovedItem;

        private void OnRemovedItem(object sender, RemovedDefectEventArgs e) => RemovedDefect?.Invoke(this, e);

        public new void Add(Defect defect)
        {
            base.Add(defect);
            AddedDefect?.Invoke(this, new AddedDefectEventArgs(defect, Count - 1));
        }

        /// <summary>
        /// Добавляет дефект с возрастанием площади.
        /// Чтобы потом удалить первый элемент
        /// </summary>
        /// <param name="defect"></param>
        public void AddDefect(Defect defect)
        {
            var index = 0;
            var found = false;
            for (var i = Count - 1; i >= 0; i--)
            {
                if (DefectSquareComparison(this[i], defect) <= 0)
                {
                    Insert(i, defect);
                    index = i;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                base.Add(defect);
                index = Count - 1;
            }

            AddedDefect?.Invoke(this, new AddedDefectEventArgs(defect, index));
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            RemovedDefect?.Invoke(this, new RemovedDefectEventArgs(index));
        }

        /// <summary>
        /// Сравнивает два дефекта по площади (по возрастанию)
        /// </summary>
        /// <param name="d1">Дефект</param>
        /// <param name="d2">Дефект</param>
        /// <returns></returns>
        private static int DefectSquareComparison(Defect d1, Defect d2)
        {
            if (d1 == null)
                return d2 == null ? 0 : -1;

            return d2?.Square.CompareTo(d1.Square) ?? 1;
        }

        /// <summary>
        /// Сортирует элементы в списке по убыванию площадей
        /// </summary>
        public new void Sort() => base.Sort(DefectSquareComparison);
    }





}