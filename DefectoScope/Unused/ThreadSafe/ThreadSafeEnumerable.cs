using System.Collections.Generic;

namespace DefectoScope
{
    public class ThreadSafeEnumerable<T>
    {
        /// <summary>
        /// Базовая коллекция
        /// </summary>
        private readonly IEnumerable<T> _items;

        /// <summary>
        /// Блокировщик
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Конструктор коллекции
        /// </summary>
        /// <param name="items"></param>
        public ThreadSafeEnumerable(IEnumerable<T> items)
        {
            _items = items;
        }

        /// <summary>
        /// Потокобезопасно выдает элементы коллекции
        /// </summary>
        public IEnumerable<T> Items => _items.AsLocked(_lock);
    }
    

}