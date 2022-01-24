using System.Collections.Generic;

namespace DefectoScope
{
    /// <summary>
    /// Расширение перечислителя коллекции
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Перебирать элементы коллекции как закрытые
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="ie">Базовый перечислитель</param>
        /// <param name="lock">Блокировщик</param>
        /// <returns></returns>
        public static IEnumerable<T> AsLocked<T>(this IEnumerable<T> ie, object @lock)
        {
            return new SafeEnumerable<T>(ie, @lock);
        }
    }
}