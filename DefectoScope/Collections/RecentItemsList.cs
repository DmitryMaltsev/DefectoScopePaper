using System;
using System.Collections.Generic;

namespace DefectoScope
{
    /// <inheritdoc />
    /// <summary>
    /// Буфер последних элементов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecentItemsList<T> : List<T>
    {
        public event RemovedDefectEventHandler RemovedItem;

        /// <summary>
        /// Максимальная емкость
        /// </summary>
        public int MaxCapacity { get; }

        /// <summary>
        /// Критерий удаления ненужного старого элемента (если null, то первый элемент будет удален)
        /// </summary>
        public Predicate<T> IsUseless { get; }

        /// <inheritdoc />
        /// <summary>
        /// Создает буфер последних элементов с заданной максимальной емкостью
        /// </summary>
        /// <param name="maxCapacity"></param>
        /// <param name="isUseless"></param>
        public RecentItemsList(int maxCapacity, Predicate<T> isUseless = null)
        {
            MaxCapacity = maxCapacity;
            Capacity = maxCapacity;
            IsUseless = isUseless;
        }

        /// <summary>
        /// Удаляет ненужные элементы из списка, если он полон
        /// </summary>
        public int RemoveUselessIfFill()
        {
            var count = Count - MaxCapacity;

            var index = -1;

            while (count > 0)
            {
                index = IsUseless != null ? FindIndex(IsUseless) : 0;
                RemoveAt(index);
                RemovedItem?.Invoke(this, new RemovedDefectEventArgs(index));
                count--;
            }

            return index;
        }

        /// <summary>
        ///     Добавляет элемент в буфер.
        /// Если емкость превышается, то удаляется старый ненужный элемент.
        /// </summary>
        /// <param name="item"></param>
        public new void Add(T item)
        {
            base.Add(item);
            RemoveUselessIfFill();
        }

        /// <summary>
        ///     Вставляет элемент в буфер.
        /// Если емкость превышается, то удаляется первый элемент.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            RemoveUselessIfFill();
        }

        /// <summary>
        ///     Добавляет элементы указанной коллекции в буфер. Если емкость превышается, то удаляются первые элементы.
        /// </summary>
        /// <param name="collection"></param>
        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            RemoveUselessIfFill();
        }

    }

}