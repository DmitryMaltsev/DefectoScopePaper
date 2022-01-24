#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace DefectoScope
{
    public class SafeEnumerable<T> : IEnumerable<T>
    {
        /// <summary>
        ///     Базовый перечислитель коллекции (не потокобезопасный)
        /// </summary>
        private readonly IEnumerable<T> _inner;

        /// <summary>
        ///     Блокировщик
        /// </summary>
        private readonly object _lock;

        /// <summary>
        ///     Конструктор перечислителя
        /// </summary>
        /// <param name="inner">Базовый перечислитель</param>
        /// <param name="lock">Блокировщик</param>
        public SafeEnumerable(IEnumerable<T> inner, object @lock)
        {
            _lock = @lock;
            _inner = inner;
        }

        #region Implementation of IEnumerable

        /// <inheritdoc />
        /// <summary>
        ///     Возвращает перечислитель, выполняющий перебор элементов в коллекции.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new SafeEnumerator<T>(_inner.GetEnumerator(), _lock);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Возвращает перечислитель, который осуществляет итерацию по коллекции.
        /// </summary>
        /// <returns>
        ///     Объект <see cref="T:System.Collections.IEnumerator" />, который используется для прохода по коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}