#region

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Kogerent;

#endregion

namespace DefectoScope
{
    internal static class Program
    {
        /// <summary>
        ///     Примитив синхронизации (для запуска лишь одного экземпляра программы)
        /// </summary>
        public static Mutex MutexProgram { get; private set; }

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!IsFirstRunApplication())
            {
                if (MessageBox.Show(
                        $@"Программа {Application.ProductName} уже запущена! Повторный запуск проигнорирован...",
                        @"Внимание: Открытие программы проигнорировано!", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning) == DialogResult.OK)
                    Process.GetCurrentProcess().Kill();
            }

            //Задаем высокий приоритет программе
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            //Отлавливаем все невыловленные исключения
            Application.ThreadException += ThreadException;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            //Выполняем то, что должно быть выполнено даже при аварийном выполнении программы
            AppDomain.CurrentDomain.DomainUnload += DomainUnload;

            //Сперва инициализируем и запускаем логгер
            G.Logger = new OFFLogger(G.PathToLogs);

            G.Logger.Info($"{nameof(Program)}: _____________________________________");
            G.Logger.Info($"{nameof(Program)}: Запуск программы {Application.ProductName} {Application.ProductVersion}");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Run(new TestAutoFillSettingsForm());
        }

        /// <summary>
        ///     Это первый запуск приложения? (первый экземпляр программы?)
        /// </summary>
        /// <returns></returns>
        private static bool IsFirstRunApplication()
        {
            try
            {
                //Проверяем на наличие мутекса в системе
                Mutex.OpenExisting($"MY_UNIQUE_MUTEX_NAME_{Application.ProductName}");
            }
            catch
            {
                //Если получили исключение значит такого мутекса нет, и его нужно создать
                MutexProgram = new Mutex(true, $"MY_UNIQUE_MUTEX_NAME_{Application.ProductName}");
                return true;
            }

            //Если исключения не было, то процесс с таким мутексом уже запущен
            return false;
        }

        /// <summary>
        /// Обратабывает фатальные исключения
        /// </summary>
        private static void FatalException(Exception e)
        {
            G.Logger.Fatal(e.ToString());

            if (MessageBox.Show(
                    $@"Программа {Application.ProductName} аварийно завершила свою работу! Перезапустите её...",
                    @"Ошибка: Аварийное завершение программы!", MessageBoxButtons.OK, MessageBoxIcon.Stop) ==
                DialogResult.OK)
                Close();
        }

        /// <summary>
        ///     Записывает исключение связанное с потоком
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            FatalException(e.Exception);
        }

        /// <summary>
        ///     Записывает не перехваченное исключение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FatalException((Exception)e.ExceptionObject);
        }

        /// <summary>
        ///     Выполняет действия, которые необходимо выполнить даже при аварийном завершении программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DomainUnload(object sender, EventArgs e) => Close();

        /// <summary>
        /// Закрывает приложение
        /// </summary>
        public static void Close()
        {
            try
            {
                G.Logger.Info($"{nameof(Program)}: Завершение работы программы {Application.ProductName} {Application.ProductVersion}");
                G.SyncController.StopController();
                G.SyncController.TrySafeClose();
                Sensor.StopSensors();
            }
            catch
            {
                // ignored
            }
            finally
            {
                G.Logger.Stop();
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}