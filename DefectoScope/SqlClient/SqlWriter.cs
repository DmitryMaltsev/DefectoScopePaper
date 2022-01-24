using System;
using System.Collections.Concurrent;

namespace DefectoScope
{
    /// <summary>
    /// Однокнопочный асинхронный записыватель в базу данных
    /// </summary>
    public class SqlWriter: IDisposable
    {
        #region Свойства

        /// <summary>
        ///     Записыватель работает?
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     Записыватель остановлен?
        /// </summary>
        public bool IsStopped { get; private set; }

        /// <summary>
        ///     Связь с базой данных в норме?
        /// </summary>
        public bool IsOk => _listener.IsOk;

        /// <summary>
        /// Записыватель уничтожен?
        /// </summary>
        public bool IsDisposed { get; private set; }

        #endregion

        #region Поля

        /// <summary>
        ///     Асинхронный записыватель
        /// </summary>
        private readonly AsyncSqlWriter _writer;

        /// <summary>
        /// Обработчик записи
        /// </summary>
        private readonly SqlListener _listener;

        /// <summary>
        ///     Обработчик очереди записи
        /// </summary>
        private readonly SqlQueueDispatcher _dispatcher;

        /// <summary>
        ///     Очередь записи
        /// </summary>
        private readonly BlockingCollection<SqlMessage> _queue;

        #endregion

        #region Методы

        #region Публичные

        /// <summary>
        /// Записывает sql сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Write(SqlMessage message) => _writer.Push(message);

        /// <summary>
        ///     Запускает процесс записи, если он еще не запущен
        /// </summary>
        /// <returns>Запуск был?</returns>
        public bool Start()
        {
            if (IsRunning) return false;

            _dispatcher.Start();
            IsRunning = true;
            return true;
        }

        /// <summary>
        ///     Останавливает процесс записи, если он еще не остановлен
        /// </summary>
        /// <returns>Стоп был?</returns>
        public bool Stop()
        {
            if (IsStopped) return false;

            _queue.CompleteAdding();
            _dispatcher.WaitForCompletion();
            IsRunning = false;
            IsStopped = true;
            return true;
        }

        #endregion

        #endregion

        #region Конструкторы / Деструкторы

        /// <summary>
        ///     Создает асинхронный записыватель
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        /// <param name="autoStart">Автостарт записывателя. По умолчанию включено.</param>
        public SqlWriter(string connectionString, bool autoStart = true)
        {
            //Инициализируем очередь записи
            _queue = new BlockingCollection<SqlMessage>();

            //Инициализируем асинхронный записыватель
            _writer = new AsyncSqlWriter(_queue);

            //Инициализируем листенер записи
            _listener = new SqlListener(connectionString);

            //Инициализируем обработчик очереди сообщений
            _dispatcher = new SqlQueueDispatcher(_queue, _listener);

            //Стартуем если автостарт включен
            if (autoStart) Start();
        }

        /// <summary>
        ///     При удалении объекта останавливается работа логгера, чтобы сохранить данные.
        ///     При закрытии программы может не справляться, поэтому необходимо прописывать самому
        /// </summary>
        public void Dispose()
        {
            Stop();

            _queue?.Dispose();

            IsDisposed = true;
        }

        #endregion
    }
}