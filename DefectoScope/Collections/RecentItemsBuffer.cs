using System;
using System.Collections.Generic;

namespace DefectoScope
{
    /// <summary>
    /// Буфер последних элементов (циклический)
    /// </summary>
    public class RecentItemsBuffer<T>
    {
        #region Конструкторы

        /// <summary>
        ///     Конструктор буфера
        /// </summary>
        /// <param name="capacity">Емкость буфера</param>
        public RecentItemsBuffer(int capacity)
        {
            Capacity = capacity;
            _buffer = new T[capacity];
        }

        #endregion

        #region Поля

        /// <summary>
        ///     Буфер элементов
        /// </summary>
        private readonly T[] _buffer;

        /// <summary>
        ///     Индекс последнего элемента
        /// </summary>
        private int _index;

        #endregion

        #region Свойства

        /// <summary>
        ///     Емкость буфера
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        ///     Количество элементов в буфере
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Количество элементов в буфере
        /// </summary>
        public int Length => Count;

        #endregion

        /// <summary>
        ///     Добавляет данные в буфер
        /// </summary>
        /// <param name="data">Добавляемые данные</param>
        public void Add(T data)
        {
            if (_index == 0)
                _index = Capacity - 1;
            else
                _index--;

            _buffer[_index] = data;

            if (Count != Capacity) Count++;
        }

        #region Очистка

        /// <summary>
        /// Производит очистку буфера
        /// </summary>
        public void Clear()
        {
            Count = 0;
            _index = 0;
            _buffer.Initialize();
        }

        #endregion

        #region Индексация

        /// <summary>
        ///     Возвращает элемент в буфере
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                //Если индекс больше количества элементов в буфере, то исключение
                if (index >= Count)
                    throw new ArgumentOutOfRangeException();

                return _buffer[(_index + index) % _buffer.Length];
            }
        }

        /// <summary>
        ///     Считывает все элементы буфера по одному (для foreach)
        /// </summary>
        public IEnumerable<T> GetData()
        {
            for (var i = 0; i < Count; i++)
                yield return _buffer[(_index + i) % _buffer.Length];
        }

        /// <summary>
        ///     Считывает заданное число элементов буфера по одному (для foreach)
        /// </summary>
        /// <param name="count">Количество считываемых элементов</param>
        public IEnumerable<T> GetData(int count)
        {
            //Если количество считываемых элементов больше количества элементов в буфере
            if (count > Count)
                throw new Exception("Недостаточно данных в буфере");

            for (var i = 0; i < count; i++)
                yield return _buffer[(_index + i) % _buffer.Length];
        }

        #endregion
    }
}