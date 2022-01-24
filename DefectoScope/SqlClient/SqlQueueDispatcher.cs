using System;
using System.Collections.Concurrent;
using System.Threading;

namespace DefectoScope
{
    /// <summary>
    /// Диспетчер очереди записи в базу данных
    /// </summary>
    public class SqlQueueDispatcher
    {
        #region Конструкторы / Деструкторы

        /// <summary>
        ///     Создает диспетчер очереди записи в базу данных
        /// </summary>
        /// <param name="pendingMessages">Буфер сообщений</param>
        /// <param name="listener">Обработчик записи</param>
        public SqlQueueDispatcher(
            BlockingCollection<SqlMessage> pendingMessages,
            SqlListener listener
        )
        {
            _pendingMessages = pendingMessages;
            _listener = listener;
        }

        #endregion

        #region Поля

        /// <summary>
        ///     Обработчик записи
        /// </summary>
        private readonly SqlListener _listener;

        /// <summary>
        ///     Буфер сообщений
        /// </summary>
        private readonly BlockingCollection<SqlMessage> _pendingMessages;

        /// <summary>
        ///     Поток диспетчера
        /// </summary>
        private Thread _dispatcherThread;

        #endregion

        #region Методы

        #region Приватные

        /// <summary>
        ///     Запускает цикл диспетчера сообщений
        /// </summary>
        private void MessageLoop()
        {
            var cancellationToken = new CancellationTokenSource();

            while (_pendingMessages.TryTake(out var message, Timeout.Infinite, cancellationToken.Token)) _listener.Write(message);
        }

        #endregion

        #region Публичные

        /// <summary>
        ///     Запускает диспетчер очереди логирования
        /// </summary>
        public void Start()
        {
            _dispatcherThread = new Thread(MessageLoop) { Name = "SqlQueueDispatcher Thread", IsBackground = true };
            _dispatcherThread.Start();
        }

        /// <summary>
        ///     Ожидает окончания потока диспетчера
        /// </summary>
        /// <param name="timeout">Таймаут</param>
        /// <returns>Поток завершился?</returns>
        public bool WaitForCompletion(TimeSpan timeout) => _dispatcherThread.Join(timeout);

        /// <summary>
        ///     Ожидает окончания потока диспетчера
        /// </summary>
        /// <returns>Поток завершился?</returns>
        public void WaitForCompletion() => _dispatcherThread.Join();

        #endregion

        #endregion
    }
}