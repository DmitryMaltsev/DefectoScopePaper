using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DefectoScope
{
    /// <inheritdoc />
    /// <summary>
    /// Потокобезопасный перечислитель коллекции
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// Базовый перечислитель коллекции (не потокобезопасный)
        /// </summary>
        private readonly IEnumerator<T> _inner;

        /// <summary>
        /// Блокировщик
        /// </summary>
        private readonly object _lock;

        /// <summary>
        /// Конструктор перечислителя 
        /// </summary>
        /// <param name="inner">Базовый перечислитель</param>
        /// <param name="lock">Блокировщик</param>
        public SafeEnumerator(IEnumerator<T> inner, object @lock)
        {
            _inner = inner;
            _lock = @lock;
            
            //Начинаем блокировку
            Monitor.Enter(_lock);
        }

        /// <inheritdoc />
        /// <summary>
        /// Снимает блокировку
        /// </summary>
        public void Dispose()
        {
            //Снимаем блокировку (будет когда foreach завершится)
            Monitor.Exit(_lock);
        }

        #region Наследуем реализацию базового перечислителя

        /// <inheritdoc />
        /// <summary>
        /// Перемещает перечислитель к следующему элементу коллекции.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext() => _inner.MoveNext();

        /// <inheritdoc />
        /// <summary>
        /// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом коллекции.
        /// </summary>
        public void Reset() => _inner.Reset();

        /// <inheritdoc />
        /// <summary>
        /// Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
        /// </summary>
        public T Current => _inner.Current;

        /// <inheritdoc />
        /// <summary>
        /// Возвращает элемент коллекции, соответствующий текущей позиции перечислителя
        /// </summary>
        object IEnumerator.Current => Current;

        #endregion
    }
}