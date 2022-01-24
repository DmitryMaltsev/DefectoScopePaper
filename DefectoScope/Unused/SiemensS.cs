//using System;
//using System.Diagnostics;
//using System.Windows.Forms;

//namespace DefectoScope
//{
//    /// <summary>
//    /// Представление работы с Siemens
//    /// </summary>
//    public static class SiemensS
//    {
//        /// <summary>
//        /// Связь с Siemens устанавливалась?
//        /// </summary>
//        public static bool Initialized;

//        /// <summary>
//        /// Включен?
//        /// </summary>
//        public static bool IsEnabled;

//        /// <summary>
//        /// Ошибка происходила в работе Siemens?
//        /// </summary>
//        public static bool ErrorOccurred;

//        /// <summary>
//        /// Необходимо остановить Siemens
//        /// </summary>
//        public static bool IsNeedStopWorkSiemens;

//        /// <summary>
//        ///     Тип соединения
//        /// </summary>
//        private static libnodave.daveOSserialType _fd;

//        /// <summary>
//        ///     Интерфейс соединения
//        /// </summary>
//        private static libnodave.daveInterface _di;

//        /// <summary>
//        ///     Соединение
//        /// </summary>
//        private static libnodave.daveConnection _dc;

//        /// <summary>
//        /// Настройки Siemens
//        /// </summary>
//        public static SiemensSettings Settings = new SiemensSettings();



//        /// <summary>
//        ///     Устанавливает соединение с Siemens
//        /// </summary>
//        public static void Connection()
//        {
//            //G.Logger.WriteToLog("Вошли в Connection");

//            try
//            {
//                //G.Logger.WriteToLog("Пробуем установить связь по IP: " + Settings.Ip);

//                //Устанавливаем соединение с Siemens
//                _fd.rfd = libnodave.openSocket(Settings.Port, Settings.Ip);
//                _fd.wfd = _fd.rfd;

//                //G.Logger.WriteToLog("Операция открытия сокета прошла успешно по IP: " + Settings.Ip);

//                if (_fd.rfd > 0)
//                {
//                    _di = new libnodave.daveInterface(_fd, "DefrazerIF", 0, libnodave.daveProtoISOTCP,
//                        libnodave.daveSpeed187k);
//                    _di.setTimeout(Settings.TimeOut);
//                    _dc = new libnodave.daveConnection(_di, 0, Settings.CpuRack, Settings.CpuSlot);

//                    //G.Logger.WriteToLog("Операция открытия соединения TCP прошла успешно по IP: " + Settings.Ip);

//                    IsEnabled = _dc.connectPLC() == 0;

//                    if (!IsEnabled)
//                    {
//                        ErrorOccurred = true;
//                        U.WriteToLog($"Не удается связаться с Siemens TCP по IP: {Settings.Ip}");
//                    }
//                }
//                else
//                {
//                    //LogDebug("Не открыто соединение TCP по IP: " + Settings.Ip);
//                    //G.Logger.WriteToLog("Переполнение открытых соединений TCP по IP: " + Settings.Ip);
//                    IsEnabled = false;
//                    ErrorOccurred = true;
//                    U.WriteToLog($"Не удается открыть сокет с Siemens TCP по IP: {Settings.Ip}");
//                }
//            }
//            catch
//            {
//                //G.Logger.WriteToLog("Не открыто соединение TCP из-за ошибки по IP: " + Settings.Ip);
//                IsEnabled = false;
//                ErrorOccurred = true;
//                U.WriteToLog($"Ошибка связи с Siemens TCP по IP: {Settings.Ip}");
//            }

//            Initialized = true; //Попытка соединения была
//        }

//        /// <summary>
//        ///     Пытается закрыть соединение с Siemens
//        /// </summary>
//        public static void Disconnection()
//        {
//            //G.Logger.WriteToLog("Вошли в Disconnection");

//            try
//            {
//                _dc.disconnectPLC();
//                libnodave.closeSocket(_fd.rfd);
//                //G.Logger.WriteToLog("Закрыто соединение TCP по IP: " + Settings.Ip);
//            }
//            catch
//            {
//                // ignored
//            }
//        }

//        /// <summary>
//        ///     Осуществляет всю необходимую работу с Siemens
//        /// </summary>
//        public static void MainWork()
//        {
//            //G.Logger.WriteToLog("Вошли в bwSiemens_DW");

//            //Disconnection();
//            //Connection();

//            if (IsEnabled)
//                try
//                {
//                    if (_dc.readBytes(libnodave.daveDB, Settings.DbNumber, 0, 72, null) == 0)
//                    {

//                    }

//                    //G.Logger.WriteToLog("После записи в Siemens");
//                }
//                catch (Exception exc)
//                {
//                    U.WriteToLog("Возникла ошибка при работе с Siemens. " + exc.Message);
//                }

//            //Если форма начала закрытие
//            if (!IsNeedStopWorkSiemens) return;

//            Disconnection();
//            U.WriteToLog($"Завершение работы программы {Application.ProductName}!");
//            Process.GetCurrentProcess().Kill(); //Убиваем процесс программы
//        }
//    }
//}