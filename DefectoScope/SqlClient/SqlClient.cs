using System;

namespace DefectoScope
{
    /// <summary>
    /// SQL клиент
    /// </summary>
    public class SqlClient: IDisposable
    {
        #region Поля

        /// <summary>
        /// Записыватель в базу данных
        /// </summary>
        private SqlWriter _writer;

        /// <summary>
        /// Считыватель базы данных
        /// </summary>
        private SqlReader _reader;

        //private ClientMode _mode;

        #endregion

        #region Свойства

        /// <summary>
        ///     Строка подключения к базе данных
        /// </summary>
        public string SqlConnectionString { get; }

        ///// <summary>
        ///// Режим работы Sql клиента
        ///// </summary>
        //public ClientMode Mode
        //{
        //    get => _mode;
        //    set
        //    {
        //        _mode = value;
        //        switch (_mode)
        //        {
        //            case ClientMode.Writing:
        //                _reader?.Dispose();
        //                if (_writer?.IsDisposed != false) _writer = new SqlWriter(SqlConnectionString);
        //                break;
        //            case ClientMode.Reading:
        //                _writer?.Dispose();
        //                if (_reader?.IsDisposed != false) _reader = new SqlReader(SqlConnectionString);
        //                break;
        //            default: throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null);
        //        }
        //    }
        //}

        /// <summary>
        /// Связь с базой данных в норме?
        /// </summary>
        public bool IsOk
        {
            //get { return Mode == ClientMode.Writing && _writer.IsOk || Mode == ClientMode.Reading && _reader.IsOk; }
            get { return _writer.IsOk && _reader.IsOk; }
        }

        /// <summary>
        /// Номер смены (0 - " ", 1 - "Пусковой")
        /// </summary>
        public short NShift { get; set; } = 0;

        /// <summary>
        /// Номер тамбура
        /// </summary>
        public int NTambour { get; set; } = 0;

        #endregion


        #region Конструкторы / деструкторы

        /// <summary>
        /// Создает SQL клиент с установкой режима
        /// </summary>
        public SqlClient(/*ClientMode mode*/)
        {
            SqlConnectionString = G.Settings.SqlConnectionString;

            //Mode = mode;
            _writer = new SqlWriter(SqlConnectionString);
            _reader = new SqlReader(SqlConnectionString);
        }

        //public SqlClient():this(ClientMode.Writing) { }

        public void Dispose()
        {
            _writer?.Dispose();
        }

        #endregion

        #region Методы

        public void Write(Defect defect)
        {
            //if (Mode != ClientMode.Writing)
            //    throw new Exception("Невозможно начать запись в режиме чтения!");

            //Если в системе что то не так, то ничего мы писать в базу не будем!
            if (!G.IsOk) return;

            var timestamp = DateTime.Now;
            var nShift = NShift;
            var nTambour = NTambour;
            var type = defect.TypeDefect.Level;

            var xCoord = (decimal)defect.GetXCoordCenter(); //м
            var yCoord = (decimal)defect.GetYCoordCenter(); //м

            var s = (decimal)defect.Square;
            var sNom = (decimal)defect.TypeDefect.Square;
            var l = (decimal)defect.D;
            var lNom = (decimal)defect.TypeDefect.Length;
            var w = (decimal)defect.L;
            var wNom = (decimal)defect.TypeDefect.Width;

            var message = new SqlMessage(timestamp, nShift, nTambour, type, xCoord, yCoord, s, sNom, l, lNom, w, wNom);
            _writer.Write(message);
        }

        /// <summary>
        /// Создает отчет в файле Excel по базе данных
        /// </summary>
        public bool CreateReport(DateTime begin, DateTime end, string fileName, bool withTambour, int nTambour = 0)
        {
            try
            {
                //if (Mode != ClientMode.Reading)
                //    throw new Exception("Невозможно начать чтение в режиме записи!");

                _reader.CreateReport(begin, end, fileName, withTambour, nTambour);

                return true;
            }
            catch { return false; }
        }

        #endregion

    }
}