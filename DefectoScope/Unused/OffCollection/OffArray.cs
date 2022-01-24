using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DefectoScope.OffCollection
{
    [Serializable]
    public class OffArray<T> : IList<T>, IList, IReadOnlyList<T>
    {
        #region Константы

        /// <summary>
        ///     Максимальный размер массива
        /// </summary>
        private const int MaxArrayLength = 0X7FEFFFFF;

        /// <summary>
        ///     Максимальный размер массива байт
        /// </summary>
        private const int MaxByteArrayLength = 0x7FFFFFC7;

        #endregion

        #region Поля
        /// <summary>
        ///     Массив данных
        /// </summary>
        private readonly T[] _items;

        /// <summary>
        ///     Пустой массив
        /// </summary>
        private static readonly T[] EmptyArray = new T[0];

        /// <summary>
        ///     Синхронизатор
        /// </summary>
        [NonSerialized] private object _syncRoot;

        /// <summary>
        ///     Версия массива (количество изменений)
        /// </summary>
        private int _version;

        #endregion

        #region Свойства

        /// <summary>
        ///     Получает емкость этого списка. Емкость - это размер внутреннего массива,
        ///     используемого для хранения элементов.
        /// </summary>
        public int Capacity => _items.Length;

        /// <summary>
        ///     Возвращает количество элементов в списке
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Возвращает синхронизатор
        /// </summary>
        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null) Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        /// <summary>
        /// Синхронизирован?
        /// </summary>
        public bool IsSynchronized => _syncRoot != null;

        #endregion


        #region Конструкторы

        /// <summary>
        /// Создает пустой массив (непонятные причины, но да ладно)
        /// </summary>
        public OffArray() : this(0) { }

        /// <summary>
        /// Создает массив с заданной емкостью
        /// </summary>
        /// <param name="capacity">Емкость массива</param>
        public OffArray(int capacity)
        {
            if (capacity < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Capacity,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            //Проверка на адекватность - ограничиваем верхнюю планку
            if ((uint)capacity > MaxArrayLength) capacity = MaxArrayLength;

            _items = capacity == 0 ? EmptyArray : new T[capacity];
        }

        #endregion

        #region Операторы

        /// <summary>
        ///     Возвращает или задает элемент по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count) OffThrowHelper.ThrowArgumentOutOfRangeException();
                return _items[index];
            }
            set
            {
                if ((uint)index >= (uint)Count) OffThrowHelper.ThrowArgumentOutOfRangeException();
                _items[index] = value;
                _version++;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        ///     Добавляет указанный элемент в массив
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if ((uint)Count >= (uint)Capacity)
                OffThrowHelper.ThrowArgumentOutOfRangeException();

            _items[Count++] = item;
            _version++;
        }

        /// <summary>
        /// Приводим ли объект к типу хранимых в массиве данных?
        /// </summary>
        /// <param name="value">Приводимый объект</param>
        /// <returns></returns>
        private static bool IsCompatibleObject(object value) => value is T || value == null && default(T) == null;

        /// <summary>
        ///     Находится ли указанный элемент в массиве?
        /// <!-- Это делается линейныйным O(n) поиском, равенство определяется вызовом item.Equals()-->
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            if (item == null)
            {
                for (var i = 0; i < Count; i++)
                {
                    if (_items[i] == null)
                        return true;
                }

                return false;
            }

            var c = EqualityComparer<T>.Default;
            for (var i = 0; i < Count; i++)
            {
                if (c.Equals(_items[i], item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Выполняет поиск указанного элемента в массиве от начала до конца и возвращает индекс первого найденного совпадения.
        /// </summary>
        /// <param name="item">Указанный элемент</param>
        /// <returns></returns>
        public int IndexOf(T item) => Array.IndexOf(_items, item, 0, Count);

        /// <summary>
        /// Вставляет указанный элемент в массив по указанному индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="item">Вставляемый элемент</param>
        public void Insert(int index, T item)
        {
            //Вставлять в конец возможно
            if ((uint)index > (uint)Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeListInsert);
            }

            if ((uint)Count >= (uint)Capacity)
                OffThrowHelper.ThrowArgumentOutOfRangeException();

            //Сдвигаем часть массива от индекса от конца
            if (index < Count) Array.Copy(_items, index, _items, index + 1, Count - index);
            _items[index] = item;   //Вставляем элемент
            Count++;
            _version++;
        }

        /// <summary>
        ///     Удаляет элемент из массива по указанному индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns></returns>
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)Count) OffThrowHelper.ThrowArgumentOutOfRangeException();
            Count--;

            if (index < Count) Array.Copy(_items, index + 1, _items, index, Count - index);
            _items[Count] = default;
            _version++;
        }

        /// <summary>
        ///     Удаляет указанный элемент из массива
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index >= 0)
            {
                RemoveAt(index);
                _version++;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Очищает содержимое массива
        /// </summary>
        public void Clear()
        {
            if (Count > 0)
            {
                Array.Clear(_items, 0, Count);
                Count = 0;
            }

            _version++;
        }

        /// <summary>
        /// Копирует массив в указанный массив с указанной позиции
        /// </summary>
        /// <param name="array">Указанный массив</param>
        /// <param name="arrayIndex">Индекс</param>
        public void CopyTo(T[] array, int arrayIndex) => Array.Copy(_items, 0, array, arrayIndex, Count);

        /// <summary>
        /// Вставляет элементы указанной коллекции по указанному индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="collection">Указанная коллекция</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection == null)
                OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Collection);
            else if ((uint)index > (uint)Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeIndex);
            }
            else
            {
                if (collection is ICollection<T> c)
                {
                    var count = c.Count;

                    if (count > 0)
                    {
                        //OFF_CAPACITY_CHECK

                        if (index < Count) Array.Copy(_items, index, _items, index + count, Count - index);

                        // Если мы вставляем список в себя же, то
                        // ReSharper disable once PossibleUnintendedReferenceComparison
                        if (this == c)
                        {
                            // Копируем первую часть _items, чтобы вставить местоположение
                            Array.Copy(_items, 0, _items, index, index);
                            // Копируем последнюю часть _items обратно во вставленное место
                            Array.Copy(_items, index + count, _items, index * 2, Count - index);
                        }
                        else
                        {
                            var itemsToInsert = new T[count];
                            c.CopyTo(itemsToInsert, 0);
                            itemsToInsert.CopyTo(_items, index);
                        }

                        Count += count;
                    }
                }
                else
                {
                    using (var en = collection.GetEnumerator())
                    {
                        while (en.MoveNext())
                            Insert(index++, en.Current);
                    }
                }

                _version++;
            }
        }

        #endregion

        #region IList

        #region Свойства


        /// <summary>
        /// Фиксированного размера? Данное свойство для всех массивов всегда имеет значение true.
        /// </summary>
        bool IList.IsFixedSize => true;

        /// <summary>
        /// Только для чтения? Данное свойство для всех массивов всегда имеет значение false.
        /// </summary>
        bool IList.IsReadOnly => false;

        #endregion

        #region Операторы

        /// <summary>
        /// Возвращает или задает элемент по указанному индексу
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns></returns>
        object IList.this[int index]
        {
            get => this[index];
            set
            {
                OffThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.Value);

                try
                {
                    this[index] = (T)value;
                }
                catch (InvalidCastException)
                {
                    OffThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
                }
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет указанный объект в массив и возвращает индекс объекта в массиве
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Номер элемента</returns>
        int IList.Add(object item)
        {
            OffThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.Item);

            try
            {
                Add((T)item);
            }
            catch (InvalidCastException)
            {
                OffThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
            }

            return Count - 1;
        }

        /// <summary>
        /// Находится ли указанный объект в массиве?
        /// </summary>
        /// <param name="item">Искомый объект</param>
        /// <returns></returns>
        bool IList.Contains(object item) => IsCompatibleObject(item) && Contains((T)item);

        /// <summary>
        /// Выполняет поиск указанного объекта в массиве от начала до конца и возвращает индекс первого найденного совпадения.
        /// Если объект привести не удалось, то возвращает -1.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int IList.IndexOf(object item)
        {
            if (IsCompatibleObject(item)) return IndexOf((T)item);
            return -1;
        }

        /// <summary>
        /// Вставляет указанный объект в массив по указанному индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="item">Вставляемый объект</param>
        void IList.Insert(int index, object item)
        {
            OffThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.Item);

            try
            {
                Insert(index, (T)item);
            }
            catch (InvalidCastException)
            {
                OffThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
            }
        }

        /// <summary>
        /// Удаляет указанный объект из массива
        /// </summary>
        /// <param name="item">Удаляемый массив</param>
        void IList.Remove(object item)
        {
            if (IsCompatibleObject(item)) Remove((T)item);
        }

        #endregion


        #endregion

        #region ICollection

        #region Свойства

        /// <summary>
        /// Только для чтения? Данное свойство для всех массивов всегда имеет значение false.
        /// </summary>
        bool ICollection<T>.IsReadOnly => false;

        #endregion

        #region Методы

        /// <summary>
        ///     Копирует массив в указанный массив с указанной позиции
        /// </summary>
        /// <param name="array">Указанный массив</param>
        /// <param name="arrayIndex">Индекс</param>
        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array.Rank != 1)
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgRankMultiDimNotSupported);

            try
            {
                Array.Copy(_items, 0, array, arrayIndex, Count);
            }
            catch (ArrayTypeMismatchException)
            {
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidArrayType);
            }
        }

        #endregion

        #endregion

        #region Internal classes

        /// <summary>
        /// Переборщик элементов массива
        /// </summary>
        [Serializable]
        public struct Enumerator : IEnumerator<T>
        {
            #region Поля

            /// <summary>
            /// Массив данных
            /// </summary>
            private readonly OffArray<T> _items;

            /// <summary>
            /// Текущий индекс
            /// </summary>
            private int _index;

            /// <summary>
            /// Версия массива (количество изменений)
            /// </summary>
            private readonly int _version;

            #endregion

            #region Свойства

            /// <summary>
            /// Текущий элемент массива
            /// </summary>
            public T Current { get; private set; }

            #endregion

            #region Конструкторы / Деструкторы

            /// <summary>
            /// Конструктор переборщика элементов массива
            /// </summary>
            /// <param name="items">Массив</param>
            internal Enumerator(OffArray<T> items)
            {
                _items = items;
                _index = 0;
                _version = items._version;
                Current = default;
            }

            /// <summary>
            /// Ничего не делает))
            /// </summary>
            public void Dispose()
            {
            }

            #endregion

            #region Методы

            /// <summary>
            /// Переходит на следующий элемент массива
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                var local = _items;

                if (_version == local._version && (uint)_index < (uint)local.Count)
                {
                    Current = local._items[_index];
                    _index++;
                    return true;
                }

                return MoveNextRare();
            }

            /// <summary>
            /// Переходит за пределы массива
            /// </summary>
            /// <returns></returns>
            private bool MoveNextRare()
            {
                if (_version != _items._version)
                    OffThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperationEnumFailedVersion);

                _index = _items.Count + 1;
                Current = default;
                return false;
            }

            #endregion

            #region IEnumerator

            /// <summary>
            /// Возвращает текущий элемент массива
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || _index == _items.Count + 1)
                    {
                        OffThrowHelper.ThrowInvalidOperationException(
                            ExceptionResource.InvalidOperationEnumOpCantHappen);
                    }

                    return Current;
                }
            }

            /// <summary>
            /// Переход на начало массива (сброс индекса)
            /// </summary>
            void IEnumerator.Reset()
            {
                if (_version != _items._version)
                    OffThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperationEnumFailedVersion);

                _index = 0;
                Current = default;
            }

            #endregion
        }

        #endregion

        #region IEnumerator

        /// <summary>
        /// Возвращает переборщик элементов массива
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Возвращает переборщик элементов массива
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        #endregion

    }
}