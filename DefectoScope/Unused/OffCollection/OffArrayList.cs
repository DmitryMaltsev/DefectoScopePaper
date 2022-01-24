#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

#endregion

namespace DefectoScope.OffCollection
{
    /// <inheritdoc cref="List{T}" />
    /// <summary>
    ///     Копия <see cref="T:System.Collections.Generic.List`1" />, убраны ненужные проверки и мусор.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class OffArrayList<T> : IList<T>, IList, IReadOnlyList<T>
    {
        #region Операторы

        /// <summary>
        ///     Устанавливает или получает элемент по указанному индексу.
        /// </summary>
        /// <param name="index"></param>
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

        #region Константы

        /// <summary>
        ///     максимальный размер массива
        /// </summary>
        private const int MaxArrayLength = 0X7FEFFFFF;

        /// <summary>
        ///     Максимальный размер массива байт
        /// </summary>
        private const int MaxByteArrayLength = 0x7FFFFFC7;

        /// <summary>
        ///     Базовая емкость списка
        /// </summary>
        private const int DefaultCapacity = 4;

        #endregion

        #region Поля

        /// <summary>
        ///     Пустой массив
        /// </summary>
        private static readonly T[] EmptyArray = new T[0];

        /// <summary>
        ///     Данные списка
        /// </summary>
        private readonly T[] _items;

        /// <summary>
        ///     Корень синхронизации
        /// </summary>
        [NonSerialized] private object _syncRoot;

        /// <summary>
        ///     Версия списка (количество изменений, но это не точно)
        /// </summary>
        private int _version;

        #endregion

        #region Конструкторы

        public OffArrayList() : this(0) { }

        public OffArrayList(int capacity)
        {
            if (capacity < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Capacity,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            _items = capacity == 0 ? EmptyArray : new T[capacity];
            Capacity = capacity;
        }

        public OffArrayList(IEnumerable<T> collection)
        {
            if (collection == null)
                OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Collection);

            if (collection is ICollection<T> c)
            {
                var count = c.Count;

                if (count == 0)
                    _items = EmptyArray;
                else
                {
                    _items = new T[count];
                    c.CopyTo(_items, 0);
                    Capacity = count;
                    Count = count;
                }
            }
            else
            {
                Count = 0;
                _items = EmptyArray;

                if (collection != null)
                {
                    using (var en = collection.GetEnumerator())
                    {
                        while (en.MoveNext())
                            Add(en.Current);
                    }
                }
            }
        }

        #endregion

        #region Свойства

        /// <summary>
        ///     Получает емкость этого списка. Емкость - это размер внутреннего массива,
        ///     используемого для хранения элементов.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        ///     Возвращает количество элементов в списке
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region IList

        /// <summary>
        ///     Фиксированного размера?
        /// </summary>
        bool IList.IsFixedSize => true;

        /// <summary>
        ///     Только для чтения?
        /// </summary>
        bool IList.IsReadOnly => false;

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

        bool IList.Contains(object item) => IsCompatibleObject(item) && Contains((T)item);

        int IList.IndexOf(object item)
        {
            if (IsCompatibleObject(item)) return IndexOf((T)item);
            return -1;
        }

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

        void IList.Remove(object item)
        {
            if (IsCompatibleObject(item)) Remove((T)item);
        }

        #endregion

        #region ICollection

        /// <inheritdoc />
        /// <summary>
        ///     Является ли этот список синхронизированным (поточно-ориентированным)?
        /// </summary>
        bool ICollection.IsSynchronized => false;

        /// <summary>
        ///     Корень синхронизации для этого объекта
        /// </summary>
        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null) Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Копирует этот список в массив, который должен иметь совместимый тип массива.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            if (array.Rank != 1)
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgRankMultiDimNotSupported);

            try
            {
                //Array.Copy проверит NULL
                Array.Copy(_items, 0, array, arrayIndex, Count);
            }
            catch (ArrayTypeMismatchException)
            {
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidArrayType);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Этот список только для чтения?
        /// </summary>
        bool ICollection<T>.IsReadOnly => false;

        #endregion

        #region IEnumerator

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        #endregion

        #region Методы

        /// <summary>
        ///     Добавляет данный объект в конец этого списка.
        ///     Размер списка увеличивается на единицу.
        ///     При необходимости емкость списка удваивается перед добавлением нового элемента.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            //OFF_CAPACITY_CHECK

            _items[Count++] = item;
            _version++;
        }


        /// <summary>
        ///     Очищает содержимое списка
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
        ///     Возвращает true, если указанный элемент находится в списке.
        ///     Это делает линейный O(n) поиск. Равенство определяется вызовом item.Equals().
        /// </summary>
        /// <param name="item"></param>
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

        public void CopyTo(T[] array, int arrayIndex)
        {
            //Делегируем оставшуюся часть проверки ошибок в Array.Copy.
            Array.Copy(_items, 0, array, arrayIndex, Count);
        }

        /// <summary>
        ///     Возвращает индекс первого появления данного значения в диапазоне этого списка.
        ///     Список ищется вперед от начала до конца.
        ///     Элементы списка сравниваются с заданным значением с помощью метода Object.Equals.
        ///     Этот метод использует метод Array.IndexOf для выполнения поиска.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item) => Array.IndexOf(_items, item, 0, Count);

        /// <summary>
        ///     Вставляет элемент в этот список по указанному индексу.
        ///     Размер списка увеличивается на единицу.
        ///     При необходимости емкость списка удваивается перед вставкой нового элемента.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            //Обратите внимание, что вставки в конце являются законными.
            if ((uint)index > (uint)Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeListInsert);
            }

            //OFF_CAPACITY_CHECK

            if (index < Count) Array.Copy(_items, index, _items, index + 1, Count - index);
            _items[index] = item;
            Count++;
            _version++;
        }


        /// <summary>
        ///     Удаляет указанный элемент из списка. Размер списка уменьшается на единицу.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }


        /// <summary>
        ///     Удаляет элемент по указанному индексу. Размер списка уменьшается на единицу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)Count) OffThrowHelper.ThrowArgumentOutOfRangeException();
            Count--;
            if (index < Count) Array.Copy(_items, index + 1, _items, index, Count - index);
            _items[Count] = default;
            _version++;
        }

        private static bool IsCompatibleObject(object value) => value is T || value == null && default(T) == null;


        /// <summary>
        ///     Добавляет элементы данной коллекции в конец этого списка.
        ///     При необходимости емкость списка увеличивается в два раза по сравнению с предыдущей емкостью
        ///     или новым размером, в зависимости от того, что больше.
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection) => InsertRange(Count, collection);

        public ReadOnlyCollection<T> AsReadOnly() => new ReadOnlyCollection<T>(this);

        /// <summary>
        ///     Выполняет поиск в разделе списка для данного элемента, используя алгоритм двоичного поиска.
        ///     Элементы списка сравниваются со значением поиска, используя заданный интерфейс IComparer.
        ///     Если Comparer равно NULL, элементы списка сравниваются со значением поиска с использованием интерфейса IComparable,
        ///     который в этом случае должен быть реализован всеми элементами списка и заданным значением поиска.
        ///     Этот метод предполагает, что данный раздел списка уже отсортирован; если это не так, результат будет неверным.
        ///     Метод возвращает индекс указанного значения в списке. Если список не содержит заданного значения,
        ///     метод возвращает отрицательное целое число. Оператор побитового дополнения (~) может быть применен
        ///     к отрицательному результату для получения индекса первого элемента (если есть), который больше заданного значения
        ///     поиска.
        ///     Это также индекс, по которому значение поиска должно быть вставлено в список, чтобы список оставался
        ///     отсортированным.
        ///     Метод использует метод Array.BinarySearch для выполнения поиска.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            if (index < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (count < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count - index < count)
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidOffLen);

            return Array.BinarySearch(_items, index, count, item, comparer);
        }

        public int BinarySearch(T item) => BinarySearch(0, Count, item, null);

        public int BinarySearch(T item, IComparer<T> comparer) => BinarySearch(0, Count, item, comparer);

        public OffArrayList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            if (converter == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Converter);
            else
            {
                var list = new OffArrayList<TOutput>(Count);
                for (var i = 0; i < Count; i++) list._items[i] = converter(_items[i]);
                list.Count = Count;
                return list;
            }

            return new OffArrayList<TOutput>();
        }


        /// <summary>
        ///     Копирует этот список в массив, который должен иметь совместимый тип массива.
        /// </summary>
        /// <param name="array"></param>
        public void CopyTo(T[] array) => CopyTo(array, 0);

        /// <summary>
        ///     Копирует раздел этого списка в указанный массив по указанному индексу. Метод использует метод Array.Copy для
        ///     копирования элементов.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <param name="count"></param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (Count - index < count) OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidOffLen);

            Array.Copy(_items, index, array, arrayIndex, count);
        }

        public bool Exists(Predicate<T> match) => FindIndex(match) != -1;

        public T Find(Predicate<T> match)
        {
            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                for (var i = 0; i < Count; i++)
                {
                    if (match(_items[i]))
                        return _items[i];
                }
            }

            return default;
        }

        public OffArrayList<T> FindAll(Predicate<T> match)
        {
            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                var list = new OffArrayList<T>();
                for (var i = 0; i < Count; i++)
                {
                    if (match(_items[i]))
                        list.Add(_items[i]);
                }

                return list;
            }

            return new OffArrayList<T>();
        }

        public int FindIndex(Predicate<T> match) => FindIndex(0, Count, match);

        public int FindIndex(int startIndex, Predicate<T> match) => FindIndex(startIndex, Count - startIndex, match);

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            if ((uint)startIndex > (uint)Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.StartIndex,
                    ExceptionResource.ArgumentOutOfRangeIndex);
            }

            if (count < 0 || startIndex > Count - count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeCount);
            }

            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                var endIndex = startIndex + count;
                for (var i = startIndex; i < endIndex; i++)
                {
                    if (match(_items[i]))
                        return i;
                }
            }

            return -1;
        }

        public T FindLast(Predicate<T> match)
        {
            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                for (var i = Count - 1; i >= 0; i--)
                {
                    if (match(_items[i]))
                        return _items[i];
                }
            }

            return default;
        }

        public int FindLastIndex(Predicate<T> match) => FindLastIndex(Count - 1, Count, match);

        public int FindLastIndex(int startIndex, Predicate<T> match) =>
            FindLastIndex(startIndex, startIndex + 1, match);

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);

            if (Count == 0)
            {
                // Особый случай для списка 0 длины
                if (startIndex != -1)
                {
                    OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.StartIndex,
                        ExceptionResource.ArgumentOutOfRangeIndex);
                }
            }
            else
            {
                // Убедимся, что мы не вне диапазона      
                if ((uint)startIndex >= (uint)Count)
                {
                    OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.StartIndex,
                        ExceptionResource.ArgumentOutOfRangeIndex);
                }
            }

            // Это также ловит, когда startIndex == MAXINT, поэтому MAXINT - 0 + 1 == -1, что составляет <0.
            if (count < 0 || startIndex - count + 1 < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeCount);
            }
            else if (match != null)
            {
                var endIndex = startIndex - count;
                for (var i = startIndex; i > endIndex; i--)
                {
                    if (match(_items[i]))
                        return i;
                }
            }

            return -1;
        }

        public void ForEach(Action<T> action)
        {
            if (action == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                var version = _version;

                for (var i = 0; i < Count; i++)
                {
                    if (version != _version) break;
                    action(_items[i]);
                }

                if (version != _version)
                    OffThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperationEnumFailedVersion);
            }
        }

        /// <summary>
        ///     Возвращает перечислитель для этого списка с указанным разрешением на удаление элементов.
        ///     Если изменения, внесенные в список во время выполнения перечисления,
        ///     методы перечислителя MoveNext и GetObject вызовут исключение.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator() => new Enumerator(this);

        public OffArrayList<T> GetRange(int index, int count)
        {
            if (index < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (count < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count - index < count) OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidOffLen);

            var list = new OffArrayList<T>(count);
            Array.Copy(_items, index, list._items, 0, count);
            list.Count = count;
            return list;
        }

        /// <summary>
        ///     Возвращает индекс первого появления данного значения в диапазоне этого списка.
        ///     Список ищется вперед, начиная с индекса и заканчивая количеством элементов.
        ///     Элементы списка сравниваются с заданным значением с помощью метода Object.Equals.
        ///     Этот метод использует метод Array.IndexOf для выполнения поиска.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index)
        {
            if (index > Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeIndex);
            }

            return Array.IndexOf(_items, item, index, Count - index);
        }

        /// <summary>
        ///     Возвращает индекс первого появления данного значения в диапазоне этого списка.
        ///     Список ищется вперед, начиная с индекса и вплоть до количества элементов.
        ///     Элементы списка сравниваются с заданным значением с помощью метода Object.Equals.
        ///     Этот метод использует метод Array.IndexOf для выполнения поиска.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index, int count)
        {
            if (index > Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeIndex);
            }

            if (count < 0 || index > Count - count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeCount);
            }

            return Array.IndexOf(_items, item, index, count);
        }

        /// <summary>
        ///     Вставляет элементы данной коллекции по заданному индексу.
        ///     При необходимости емкость списка увеличивается в два раза по сравнению с предыдущей емкостью
        ///     или новым размером, в зависимости от того, что больше.
        ///     Диапазоны могут быть добавлены в конец списка, установив индекс в размер списка.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (collection == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Collection);
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

        /// <summary>
        ///     Возвращает индекс последнего появления данного значения в диапазоне этого списка.
        ///     Список ищется в обратном направлении, начиная с конца и заканчивая первым элементом в списке.
        ///     Элементы списка сравниваются с заданным значением с помощью метода Object.Equals.
        ///     Этот метод использует метод Array.LastIndexOf для выполнения поиска.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LastIndexOf(T item)
        {
            if (Count == 0)
                return -1;
            return LastIndexOf(item, Count - 1, Count);
        }

        /// <summary>
        ///     Возвращает индекс последнего появления данного значения в диапазоне этого списка.
        ///     Список ищется в обратном направлении, начиная с индекса и заканчивая первым элементом в списке.
        ///     Элементы списка сравниваются с заданным значением с помощью метода Object.Equals.
        ///     Этот метод использует метод Array.LastIndexOf для выполнения поиска.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int LastIndexOf(T item, int index)
        {
            if (index >= Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeIndex);
            }

            return LastIndexOf(item, index, index + 1);
        }

        /// <summary>
        ///     Возвращает индекс последнего появления данного значения в диапазоне этого списка.
        ///     Список ищется в обратном направлении, начиная с индекса индекса и вплоть до количества элементов.
        ///     Элементы списка сравниваются с заданным значением с помощью метода Object.Equals.
        ///     Этот метод использует метод Array.LastIndexOf для выполнения поиска.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int LastIndexOf(T item, int index, int count)
        {
            if (Count != 0 && index < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count != 0 && count < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count == 0) return -1;

            if (index >= Count)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeBiggerThanCollection);
            }

            if (count > index + 1)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeBiggerThanCollection);
            }

            return Array.LastIndexOf(_items, item, index, count);
        }

        /// <summary>
        ///     Этот метод удаляет все элементы, которые соответствуют предикату. Сложность O(n).
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int RemoveAll(Predicate<T> match)
        {
            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                //Первый свободный слот в массиве предметов
                var freeIndex = 0;

                //Ищем первый предмет, который нужно удалить.
                while (freeIndex < Count && !match(_items[freeIndex])) freeIndex++;
                if (freeIndex >= Count) return 0;

                var current = freeIndex + 1;

                while (current < Count)
                {
                    //Ищем первый предмет, который нужно сохранить.
                    while (current < Count && match(_items[current])) current++;

                    if (current < Count) _items[freeIndex++] = _items[current++];
                }

                Array.Clear(_items, freeIndex, Count - freeIndex);
                var result = Count - freeIndex;
                Count = freeIndex;
                _version++;
                return result;
            }

            return -1;
        }

        /// <summary>
        ///     Удаляет диапазон элементов из этого списка.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (count < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count - index < count)
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidOffLen);

            if (count > 0)
            {
                Count -= count;
                if (index < Count) Array.Copy(_items, index + count, _items, index, Count - index);
                Array.Clear(_items, Count, count);
                _version++;
            }
        }

        /// <summary>
        ///     Переворачивает элементы в этом списке.
        /// </summary>
        public void Reverse() => Reverse(0, Count);

        /// <summary>
        ///     Инвертирует элементы в диапазоне этого списка. После вызова этого метода элемент в диапазоне,
        ///     заданном index и count, который ранее находился в index i, теперь будет расположен
        ///     в index + (index + count - i - 1). Этот метод использует метод Array.Reverse для реверсирования элементов.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void Reverse(int index, int count)
        {
            if (index < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (count < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count - index < count)
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidOffLen);
            Array.Reverse(_items, index, count);
            _version++;
        }

        /// <summary>
        ///     Сортирует элементы в этом списке. Использует компаратор по умолчанию и Array.Sort.
        /// </summary>
        public void Sort() => Sort(0, Count, null);

        /// <summary>
        ///     Сортирует элементы в этом списке. Использует Array.Sort с предоставленным компаратором.
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<T> comparer) => Sort(0, Count, comparer);

        /// <summary>
        ///     Сортирует элементы в разделе этого списка. Сортировка сравнивает элементы друг с другом,
        ///     используя заданный интерфейс IComparer. Если Comparer равно NULL,
        ///     элементы сравниваются друг с другом, используя интерфейс IComparable,
        ///     который в этом случае должен быть реализован всеми элементами списка.
        ///     Этот метод использует метод Array.Sort для сортировки элементов.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            if (index < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Index,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (count < 0)
            {
                OffThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.Count,
                    ExceptionResource.ArgumentOutOfRangeNeedNonNegNum);
            }

            if (Count - index < count)
                OffThrowHelper.ThrowArgumentException(ExceptionResource.ArgumentInvalidOffLen);

            Array.Sort(_items, index, count, comparer);
            _version++;
        }

        public void Sort(Comparison<T> comparison)
        {
            if (comparison == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);

            if (Count > 0)
            {
                IComparer<T> comparer = new FunctorComparer<T>(comparison);
                Array.Sort(_items, 0, Count, comparer);
            }
        }

        /// <summary>
        ///     Возвращает новый массив объектов, содержащий содержимое списка.
        ///     Это требует копирования списка, что является операцией O(n).
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            var array = new T[Count];
            Array.Copy(_items, 0, array, 0, Count);
            return array;
        }

        public bool TrueForAll(Predicate<T> match)
        {
            if (match == null) OffThrowHelper.ThrowArgumentNullException(ExceptionArgument.Match);
            else
            {
                for (var i = 0; i < Count; i++)
                {
                    if (!match(_items[i]))
                        return false;
                }
            }

            return true;
        }

        internal static IList<T> Synchronized(OffArrayList<T> list) => new SynchronizedList(list);

        #endregion

        #region Internal

        [Serializable]
        internal class SynchronizedList : IList<T>
        {
            private readonly OffArrayList<T> _list;
            private readonly object _root;

            internal SynchronizedList(OffArrayList<T> list)
            {
                _list = list;
                _root = ((ICollection)list).SyncRoot;
            }

            public int Count
            {
                get
                {
                    lock (_root) return _list.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    lock (_root) return ((ICollection<T>)_list).IsReadOnly;
                }
            }

            public void Add(T item)
            {
                lock (_root) _list.Add(item);
            }

            public void Clear()
            {
                lock (_root) _list.Clear();
            }

            public bool Contains(T item)
            {
                lock (_root) return _list.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                lock (_root) _list.CopyTo(array, arrayIndex);
            }

            public bool Remove(T item)
            {
                lock (_root) return _list.Remove(item);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                lock (_root) return _list.GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                lock (_root) return ((IEnumerable<T>)_list).GetEnumerator();
            }

            public T this[int index]
            {
                get
                {
                    lock (_root) return _list[index];
                }
                set
                {
                    lock (_root) _list[index] = value;
                }
            }

            public int IndexOf(T item)
            {
                lock (_root) return _list.IndexOf(item);
            }

            public void Insert(int index, T item)
            {
                lock (_root) _list.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                lock (_root) _list.RemoveAt(index);
            }
        }

        [Serializable]
        public struct Enumerator : IEnumerator<T>
        {
            private readonly OffArrayList<T> _list;
            private int _index;
            private readonly int _version;

            internal Enumerator(OffArrayList<T> list)
            {
                _list = list;
                _index = 0;
                _version = list._version;
                Current = default;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                var localList = _list;

                if (_version == localList._version && (uint)_index < (uint)localList.Count)
                {
                    Current = localList._items[_index];
                    _index++;
                    return true;
                }

                return MoveNextRare();
            }

            private bool MoveNextRare()
            {
                if (_version != _list._version)
                    OffThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperationEnumFailedVersion);

                _index = _list.Count + 1;
                Current = default;
                return false;
            }

            public T Current { get; private set; }

            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || _index == _list.Count + 1)
                    {
                        OffThrowHelper.ThrowInvalidOperationException(
                            ExceptionResource.InvalidOperationEnumOpCantHappen);
                    }

                    return Current;
                }
            }

            void IEnumerator.Reset()
            {
                if (_version != _list._version)
                    OffThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperationEnumFailedVersion);

                _index = 0;
                Current = default;
            }
        }

        internal sealed class FunctorComparer<TF> : IComparer<TF>
        {
            private readonly Comparison<TF> _comparison;

            public FunctorComparer(Comparison<TF> comparison) => _comparison = comparison;

            public int Compare(TF x, TF y) => _comparison(x, y);
        }

        #endregion
    }
}