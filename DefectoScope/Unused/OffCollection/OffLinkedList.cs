#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#endregion

namespace DefectoScope.OffCollection
{
    /// <summary>
    /// Двусвязный циклический список
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class OffLinkedList<T> : ICollection<T>, ICollection, IReadOnlyCollection<T>
    {
        #region Поля

        /// <summary>
        /// Синхронизатор
        /// </summary>
        private object _syncRoot;

        #endregion

        #region Свойства

        /// <summary>
        /// Возвращает первый элемент
        /// </summary>
        public OffLinkedListNode First { get; private set; }

        /// <summary>
        /// Возвращает последний элемент
        /// </summary>
        public OffLinkedListNode Last => First?.Previous;

        /// <summary>
        /// Версия списка
        /// </summary>
        public int Version { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Ничего не делаем
        /// </summary>
        public OffLinkedList() { }

        /// <summary>
        /// Создает список и добавляет элементы коллекции в него
        /// </summary>
        /// <param name="collection"></param>
        public OffLinkedList(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            foreach (var item in collection) AddLast(item);
        }

        #endregion



        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null) Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Rank != 1) throw new ArgumentException();
            if (array.GetLowerBound(0) != 0) throw new ArgumentException();
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < Count) throw new ArgumentException();

            if (array is T[] tArray)
                CopyTo(tArray, index);
            else
            {
                //
                // Catch the obvious case assignment will fail.
                // We can found all possible problems by doing the check though.
                // For example, if the element type of the Array is derived from T,
                // we can't figure out if we can successfully copy the element beforehand.
                //
                var targetType = array.GetType().GetElementType();
                var sourceType = typeof(T);
                if (targetType != null &&
                    !(targetType.IsAssignableFrom(sourceType) || sourceType.IsAssignableFrom(targetType)))
                    throw new ArgumentException();

                if (!(array is object[] objects)) throw new ArgumentException();
                var node = First;

                try
                {
                    if (node != null)
                    {
                        do
                        {
                            objects[index++] = node.Value;
                            node = node.Next;
                        } while (node != First);
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException();
                }
            }
        }

        public int Count { get; private set; }

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T value) => AddLast(value);

        public void Clear()
        {
            var current = First;

            while (current != null)
            {
                var temp = current;
                current = current.Next; // use Next the instead of "next", otherwise it will loop forever
                temp.Invalidate();
            }

            First = null;
            Count = 0;
            Version++;
        }

        public bool Contains(T value) => Find(value) != null;

        public void CopyTo(T[] array, int index)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (index < 0 || index > array.Length) throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < Count) throw new ArgumentException();

            var node = First;

            if (node != null)
            {
                do
                {
                    array[index++] = node.Value;
                    node = node.Next;
                } while (node != First);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        public bool Remove(T value)
        {
            var node = Find(value);

            if (node != null)
            {
                InternalRemoveNode(node);
                return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public OffLinkedListNode AddAfter(OffLinkedListNode node, T value)
        {
            ValidateNode(node);
            var result = new OffLinkedListNode(node.List, value);
            InternalInsertNodeBefore(node.Next, result);
            return result;
        }

        public void AddAfter(OffLinkedListNode node, OffLinkedListNode newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            InternalInsertNodeBefore(node.Next, newNode);
            newNode.List = this;
        }

        public OffLinkedListNode AddBefore(OffLinkedListNode node, T value)
        {
            ValidateNode(node);
            var result = new OffLinkedListNode(node.List, value);
            InternalInsertNodeBefore(node, result);
            if (node == First) First = result;
            return result;
        }

        public void AddBefore(OffLinkedListNode node, OffLinkedListNode newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);
            InternalInsertNodeBefore(node, newNode);
            newNode.List = this;
            if (node == First) First = newNode;
        }

        public OffLinkedListNode AddFirst(T value)
        {
            var result = new OffLinkedListNode(this, value);

            if (First == null)
                InternalInsertNodeToEmptyList(result);
            else
            {
                InternalInsertNodeBefore(First, result);
                First = result;
            }

            return result;
        }

        public void AddFirst(OffLinkedListNode node)
        {
            ValidateNode(node);

            if (First == null)
                InternalInsertNodeToEmptyList(node);
            else
            {
                InternalInsertNodeBefore(First, node);
                First = node;
            }

            node.List = this;
        }

        public OffLinkedListNode AddLast(T value)
        {
            var result = new OffLinkedListNode(this, value);
            if (First == null)
                InternalInsertNodeToEmptyList(result);
            else
                InternalInsertNodeBefore(First, result);
            return result;
        }

        public void AddLast(OffLinkedListNode node)
        {
            ValidateNode(node);

            if (First == null)
                InternalInsertNodeToEmptyList(node);
            else
                InternalInsertNodeBefore(First, node);
            node.List = this;
        }

        public OffLinkedListNode Find(T value)
        {
            var node = First;
            var c = EqualityComparer<T>.Default;

            if (node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(node.Value, value)) return node;
                        node = node.Next;
                    } while (node != First);
                }
                else
                {
                    do
                    {
                        if (node.Value == null) return node;
                        node = node.Next;
                    } while (node != First);
                }
            }

            return null;
        }

        public OffLinkedListNode FindLast(T value)
        {
            if (First == null) return null;

            var last = First.Previous;
            var node = last;
            var c = EqualityComparer<T>.Default;

            if (node != null)
            {
                if (value != null)
                {
                    do
                    {
                        if (c.Equals(node.Value, value)) return node;

                        node = node.Previous;
                    } while (node != last);
                }
                else
                {
                    do
                    {
                        if (node.Value == null) return node;
                        node = node.Previous;
                    } while (node != last);
                }
            }

            return null;
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Удаляет указанный узел из списка
        /// </summary>
        /// <param name="node">Удаляемый узел</param>
        public void Remove(OffLinkedListNode node)
        {
            ValidateNode(node);
            InternalRemoveNode(node);
        }

        /// <summary>
        /// Удаляет первый элемент списка
        /// </summary>
        public void RemoveFirst()
        {
            if (First == null) throw new InvalidOperationException();
            InternalRemoveNode(First);
        }

        /// <summary>
        /// Удаляет последний элемент списка
        /// </summary>
        public void RemoveLast()
        {
            if (First == null) throw new InvalidOperationException();
            InternalRemoveNode(First.Previous);
        }

        /// <summary>
        /// Вставляет новый узел До указанного
        /// </summary>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        private void InternalInsertNodeBefore(OffLinkedListNode node, OffLinkedListNode newNode)
        {
            newNode.Next = node;
            newNode.Previous = node.Previous;
            node.Previous.Next = newNode;
            node.Previous = newNode;
            Version++;
            Count++;
        }

        /// <summary>
        /// Вставляет узел в пустой список
        /// </summary>
        /// <param name="newNode"></param>
        private void InternalInsertNodeToEmptyList(OffLinkedListNode newNode)
        {
            newNode.Next = newNode;
            newNode.Previous = newNode;
            First = newNode;
            Version++;
            Count++;
        }

        /// <summary>
        /// Удаляет указанный узел из списка
        /// </summary>
        /// <param name="node"></param>
        private void InternalRemoveNode(OffLinkedListNode node)
        {
            if (node.Next == node)
                First = null;
            else
            {
                node.Next.Previous = node.Previous;
                node.Previous.Next = node.Next;
                if (First == node) First = node.Next;
            }

            node.Invalidate();
            Count--;
            Version++;
        }

        /// <summary>
        /// Валидация узла
        /// </summary>
        /// <param name="node"></param>
        public void ValidateNode(OffLinkedListNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException();
        }

        /// <summary>
        /// Перечислитель элементов списка
        /// </summary>
        public class Enumerator : IEnumerator<T>
        {
            #region Поля

            /// <summary>
            /// Список
            /// </summary>
            private readonly OffLinkedList<T> _list;

            /// <summary>
            /// Узел
            /// </summary>
            private OffLinkedListNode _node;

            /// <summary>
            /// Версия
            /// </summary>
            private readonly int _version;

            /// <summary>
            /// Индекс
            /// </summary>
            private int _index;

            #endregion

            #region Свойства

            /// <summary>
            /// Текущее значение
            /// </summary>
            public T Current { get; private set; }

            #endregion

            #region Конструкторы / Деструкторы

            /// <summary>
            /// Создает перечислитель для списка
            /// </summary>
            /// <param name="list">Список</param>
            public Enumerator(OffLinkedList<T> list)
            {
                _list = list;
                _version = list.Version;
                _node = list.First;
                _index = 0;
                Current = default;
            }

            /// <summary>
            /// Ничего не делаем))
            /// </summary>
            public void Dispose() { }

            #endregion

            #region IEnumerator

            /// <summary>
            /// Возвращает текущее значение
            /// </summary>
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

            /// <summary>
            /// Сбрасывает перечислитель
            /// </summary>
            void IEnumerator.Reset()
            {
                if (_version != _list.Version) throw new InvalidOperationException();

                Current = default;
                _node = _list.First;
                _index = 0;
            }

            #endregion

            #region Методы

            /// <summary>
            /// Переходит на следующий элемент
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                if (_version != _list.Version) throw new InvalidOperationException();

                if (_node == null)
                {
                    _index = _list.Count + 1;
                    return false;
                }

                ++_index;
                Current = _node.Value;
                _node = _node.Next;

                if (_node == _list.First) _node = null;
                return true;
            }

            #endregion
        }

        /// <summary>
        /// Узел двухсвязного списка
        /// </summary>
        public class OffLinkedListNode
        {
            #region Поля

            /// <summary>
            /// Узел За этим
            /// </summary>
            private OffLinkedListNode _nextNode;

            /// <summary>
            /// Узел До этого
            /// </summary>
            private OffLinkedListNode _prevNode;

            #endregion

            #region Свойства

            /// <summary>
            /// Cписок, в котором находится узел
            /// </summary>
            public OffLinkedList<T> List { get; set; }

            /// <summary>
            /// Возвращает узел За этим
            /// </summary>
            public OffLinkedListNode Next
            {
                get => _nextNode == null || _nextNode == List.First ? null : _nextNode;
                set => _nextNode = value;
            }

            /// <summary>
            /// Возвращает узел До этого
            /// </summary>
            public OffLinkedListNode Previous
            {
                get => _prevNode == null || this == List.First ? null : _prevNode;
                set => _prevNode = value;
            }

            /// <summary>
            /// Значение узла
            /// </summary>
            public T Value { get; set; }

            #endregion

            #region Конструкторы

            /// <summary>
            /// Создает узел и заполняет его значение
            /// </summary>
            /// <param name="value">Значение</param>
            public OffLinkedListNode(T value) => Value = value;

            /// <summary>
            /// Создает узел, привязывает его к списку и заполняет его значение
            /// </summary>
            /// <param name="listNode">Список узлов</param>
            /// <param name="value">Значение</param>
            public OffLinkedListNode(OffLinkedList<T> listNode, T value)
            {
                List = listNode;
                Value = value;
            }

            #endregion

            #region Методы

            /// <summary>
            /// Делает узел недействительным
            /// </summary>
            public void Invalidate()
            {
                List = null;
                _nextNode = null;
                _prevNode = null;
            }

            #endregion
        }
    }


}