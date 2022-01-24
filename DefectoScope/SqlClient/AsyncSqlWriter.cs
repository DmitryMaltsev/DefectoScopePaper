using System.Collections.Concurrent;

namespace DefectoScope
{
    /// <summary>
    /// Асинхронный записыватель данных в базу данных
    /// </summary>
    public class AsyncSqlWriter
    {
        #region Конструкторы / Деструкторы

        /// <summary>
        ///     Создает асинхронный записыватель
        /// </summary>
        /// <param name="pendingMessages">Буфер сообщений</param>
        public AsyncSqlWriter(BlockingCollection<SqlMessage> pendingMessages) => _pendingMessages = pendingMessages;

        #endregion

        #region Поля

        /// <summary>
        ///     Буфер сообщений
        /// </summary>
        private readonly BlockingCollection<SqlMessage> _pendingMessages;

        #endregion

        #region Методы

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Push(SqlMessage message)
        {
            if (!_pendingMessages.IsAddingCompleted)
                _pendingMessages.TryAdd(message);
        }

        #endregion
    }
}