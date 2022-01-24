#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace DefectoScope
{
    /// <inheritdoc />
    /// <summary>
    ///     Потокобезопасный список
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class SafeList<T> : IList<T>
    {
        /// <summary>
        ///     Базовая коллекция (не потокобезопасная)
        /// </summary>
        private readonly List<T> _inner;

        /// <summary>
        ///     Блокировщик
        /// </summary>
        private readonly object _lock = new object();

        #region Конструкторы

        /// <summary>
        /// Инициализирует новый список, который является пустым и имеет начальную емокость по умолчанию
        /// </summary>
        public SafeList() => _inner = new List<T>();

        /// <summary>
        /// Инициализирует новый пустой список с указанной начальной емкостью
        /// </summary>
        /// <param name="capacity">Число элементов, которые может изначально вместить новый список</param>
        public SafeList(int capacity) => _inner = new List<T>(capacity);

        #endregion


        /// <inheritdoc />
        /// <summary>
        ///     Возвращает перечислитель, осуществляющий перебор элементов списка
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            //Вместо того, чтобы возвращать не потоконебезопасный перечислитель,
            //мы помещаем его в наш потокобезопасный класс
            // ReSharper disable once InconsistentlySynchronizedField
            return new SafeEnumerator<T>(_inner.GetEnumerator(), _lock);
        }


        #region Implementation of IEnumerable

        /// <inheritdoc />
        /// <summary>
        ///     Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        ///     Объект <see cref="T:System.Collections.IEnumerator" />, который используется для прохода по коллекции.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            lock (_lock) return _inner.GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        /// <inheritdoc />
        /// <summary>
        ///     Добавляет элемент в коллекцию <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">
        ///     Объект, добавляемый в коллекцию <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     Объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения.
        /// </exception>
        public void Add(T item)
        {
            lock (_lock) _inner.Add(item);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Удаляет все элементы из коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     Объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения.
        /// </exception>
        public void Clear()
        {
            lock (_lock) _inner.Clear();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Определяет, содержит ли коллекция <see cref="T:System.Collections.Generic.ICollection`1" /> указанное значение.
        /// </summary>
        /// <param name="item">
        ///     Объект для поиска в <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        /// <returns>
        ///     Значение <see langword="true" />, если параметр <paramref name="item" /> найден в коллекции
        ///     <see cref="T:System.Collections.Generic.ICollection`1" />; в противном случае — значение <see langword="false" />.
        /// </returns>
        public bool Contains(T item)
        {
            lock (_lock) return _inner.Contains(item);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Копирует элементы коллекции <see cref="T:System.Collections.Generic.ICollection`1" /> в массив
        ///     <see cref="T:System.Array" />, начиная с указанного индекса массива <see cref="T:System.Array" />.
        /// </summary>
        /// <param name="array">
        ///     Одномерный массив <see cref="T:System.Array" />, в который копируются элементы из интерфейса
        ///     <see cref="T:System.Collections.Generic.ICollection`1" />.
        ///     Массив <see cref="T:System.Array" /> должен иметь индексацию, начинающуюся с нуля.
        /// </param>
        /// <param name="arrayIndex">
        ///     Отсчитываемый от нуля индекс в массиве <paramref name="array" />, указывающий начало копирования.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     Свойство <paramref name="array" /> имеет значение <see langword="null" />.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     Значение параметра <paramref name="arrayIndex" /> меньше 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     Количество элементов в исходной коллекции <see cref="T:System.Collections.Generic.ICollection`1" /> больше, чем
        ///     свободное пространство от <paramref name="arrayIndex" /> до конца массива назначения <paramref name="array" />.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lock) _inner.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Удаляет первое вхождение указанного объекта из коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">
        ///     Объект, который необходимо удалить из коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        /// <returns>
        ///     Значение <see langword="true" />, если объект <paramref name="item" /> успешно удален из
        ///     <see cref="T:System.Collections.Generic.ICollection`1" />; в противном случае — значение <see langword="false" />.
        ///     Этот метод также возвращает значение <see langword="false" />, если значение <paramref name="item" /> не найдено в
        ///     исходной коллекции <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        ///     Объект <see cref="T:System.Collections.Generic.ICollection`1" /> доступен только для чтения.
        /// </exception>
        public bool Remove(T item)
        {
            lock (_lock) return _inner.Remove(item);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///     Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public int Count
        {
            get
            {
                lock (_lock) return _inner.Count;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Получает значение, указывающее, является ли объект <see cref="T:System.Collections.Generic.ICollection`1" />
        ///     доступным только для чтения.
        /// </summary>
        /// <returns>
        ///     Значение <see langword="true" />, если интерфейс <see cref="T:System.Collections.Generic.ICollection`1" /> доступен
        ///     только для чтения; в противном случае — значение <see langword="false" />.
        /// </returns>
        public bool IsReadOnly
        {
            get
            {
                lock (_lock) return false;
            }
        }

        #endregion

        #region Implementation of IList<T>

        /// <inheritdoc />
        /// <summary>
        ///     Определяет индекс заданного элемента в списке <see cref="T:System.Collections.Generic.IList`1" />.
        /// </summary>
        /// <param name="item">
        ///     Объект для поиска в <see cref="T:System.Collections.Generic.IList`1" />.
        /// </param>
        /// <returns>
        ///     Индекс <paramref name="item" />, если он найден в списке; в противном случае — значение -1.
        /// </returns>
        public int IndexOf(T item)
        {
            lock (_lock) return _inner.IndexOf(item);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Вставляет элемент в список <see cref="T:System.Collections.Generic.IList`1" /> по указанному индексу.
        /// </summary>
        /// <param name="index">
        ///     Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item" />.
        /// </param>
        /// <param name="item">
        ///     Объект, вставляемый в коллекцию <see cref="T:System.Collections.Generic.IList`1" />.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     Объект <see cref="T:System.Collections.Generic.IList`1" /> доступен только для чтения.
        /// </exception>
        public void Insert(int index, T item)
        {
            lock (_lock) _inner.Insert(index, item);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Удаляет элемент <see cref="T:System.Collections.Generic.IList`1" />, расположенный по указанному индексу.
        /// </summary>
        /// <param name="index">
        ///     Отсчитываемый от нуля индекс удаляемого элемента.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     Объект <see cref="T:System.Collections.Generic.IList`1" /> доступен только для чтения.
        /// </exception>
        public void RemoveAt(int index)
        {
            lock (_lock) _inner.RemoveAt(index);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Возвращает или задает элемент по указанному индексу.
        /// </summary>
        /// <param name="index">
        ///     Отсчитываемый от нуля индекс элемента, который требуется возвратить или задать.
        /// </param>
        /// <returns>Элемент, расположенный по указанному индексу.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> не является допустимым индексом в <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     Свойство задано, и список <see cref="T:System.Collections.Generic.IList`1" /> доступен только для чтения.
        /// </exception>
        public T this[int index]
        {
            get
            {
                lock (_lock) return _inner[index];
            }
            set
            {
                lock (_lock) _inner[index] = value;
            }
        }

        #endregion
    }
}