//using System;
//using System.IO;
//using System.Windows.Forms;

//namespace DefectoScope
//{
//    public static class G.Logger
//    {
//        #region Логирование

//        /// <summary>
//        /// Блокиратор логирования
//        /// </summary>
//        private static readonly object LogLocker = new object();

//        /// <summary>
//        ///     Записывает текст в лог-файл с указанием времени
//        /// </summary>
//        /// <param name="text">Логируемый текст</param>
//        public static void WriteToLog(string text)
//        {
//            lock (LogLocker)
//            {
//                //Пытаемся записать данные в файл, и выводим сообщение о результате этой операции
//                try
//                {
//                    var now = DateTime.Now; //Запоминаем текущую дату и время
//                    var path = Application.StartupPath + G.PathToLog;
//                    Directory.CreateDirectory(path); //Создаем папку если ее не существует
//                    var fileName = "Журнал за " + now.ToString("yyyy.MM.dd") + ".txt"; //Имя файл-лога

//                    using (var file = new FileStream(path + fileName, FileMode.Append, FileAccess.Write,
//                        FileShare.None))
//                    {
//                        var writer = new StreamWriter(file);

//                        writer.WriteLine(now.ToString("HH:mm:ss:fff ") + text);
//                        writer.Flush();
//                        file.Close();
//                    }
//                }
//                catch (Exception e)
//                {
//                    WriteProgramException(e);
//                }
//            }
//        }

//        /// <summary>
//        /// Записывает текст в лог-файл если требуется
//        /// </summary>
//        /// <param name="text">Логируемый текст</param>
//        public static void WriteMessageToLog(string text)
//        {
//            if (G.Settings.LogMessages)
//                WriteToLog(text);
//        }


//        /// <summary>
//        /// Записывает данные по ошибке в лог-файл если требуется
//        /// </summary>
//        /// <param name="message">Сообщение (заголовок)</param>
//        /// <param name="text">Логируемый текст ошибки</param>
//        public static void WriteErrorToLog(string text, string message = "ОШИБКА")
//        {
//            if (G.Settings.LogErrors)
//                WriteToLog($"{message}: {text}");
//        }

//        /// <summary>
//        /// Записывает данные по исключению в лог-файл
//        /// </summary>
//        /// <param name="e">Логируемое исключение</param>
//        /// <param name="message">Сообщение до исключения</param>
//        public static void WriteException(Exception e, string message = "OFF_CRITICAL:")
//        {
//            WriteToLog(message);

//            WriteToLog(e?.GetType().ToString());
//            WriteToLog(e?.Message);
//            WriteToLog(e?.StackTrace);
//            if (e?.InnerException != null)
//            {
//                WriteToLog(e.InnerException.GetType().ToString());
//                WriteToLog(e.InnerException.Message);
//                WriteToLog(e.InnerException.StackTrace);
//            }
//        }

//        /// <summary>
//        /// Записывает данные по исключению программы в лог-файл если требуется
//        /// </summary>
//        /// <param name="e">Логируемое исключение</param>
//        /// <param name="message">Сообщение до исключения</param>
//        public static void WriteProgramException(Exception e, string message = "OFF:")
//        {
//            if (G.Settings.LogExceptions) WriteException(e, message);
//        }

//        #endregion
//    }
//}