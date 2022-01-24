namespace DefectoScope
{
    /// <summary>
    /// Режимы работы датчиков
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Режим профилей
        /// </summary>
        Profile,
        /// <summary>
        /// Режим видео
        /// </summary>
        Video,
        /// <summary>
        /// Режим видео через ОЗУ
        /// </summary>
        VideoRam
    }

    /// <summary>
    /// Режимы синхронизации датчиков
    /// </summary>
    public enum SyncMode
    {
        /// <summary>
        /// Режим внешней синхронизации
        /// </summary>
        SyncExt,
        /// <summary>
        /// Режим синхронизации по команде
        /// </summary>
        SyncCmd,
        /// <summary>
        /// Режим непрерывной работы с заданной частотой
        /// </summary>
        SyncNone
    }

    /// <summary>
    /// Режимы ручной подачи сигналов
    /// </summary>
    public enum ManualMode
    {
        /// <summary>
        /// Прерывисто (с заданным количеством повторений)
        /// </summary>
        Repeatedly,
        /// <summary>
        /// Непрерывно
        /// </summary>
        Constantly
    }

    /// <summary>
    /// Графические маркеры дефектов
    /// </summary>
    public enum Marker { Square, Triangle, Circle, Rhombus}

    /// <summary>
    /// Режим работы Sql клиента
    /// </summary>
    public enum ClientMode
    {
        /// <summary>
        /// Режим записи
        /// </summary>
        Writing,

        /// <summary>
        /// Режим чтения
        /// </summary>
        Reading
    }

    /// <summary>
    /// Состояние датчика
    /// </summary>
    public enum SensorStatus
    {
        /// <summary>
        /// Не инициализирован
        /// </summary>
        NoInitialization = -1,

        /// <summary>
        /// Исправен
        /// </summary>
        Good,

        /// <summary>
        /// Неисправен
        /// </summary>
        Bad
    }
}