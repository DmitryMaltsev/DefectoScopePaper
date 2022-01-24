using System;
using System.Windows.Forms;
using Kogerent;

namespace DefectoScope
{
    /// <summary>
    ///     Расширения облегчающие работу с элементами управления в многопоточной среде.
    /// </summary>
    public static class ControlExtensions
    {
        #region Invoke
        
        /// <summary>
        /// Пытается выполнить действие через вызов делегата различными способами в зависимости от параметров, если это необходимо.
        /// Если <paramref name="control"/> = null, то ничего не будет вызвано, ошибки не будет.
        /// </summary>
        /// <typeparam name="T">Тип параметра делегата</typeparam>
        /// <param name="control">Элемент управления</param>
        /// <param name="doIt">Делегат с некоторым действием</param>
        /// <param name="arg">Аргумент делагата с действием</param>
        /// <param name="callAsync">Вызов делегата асинхронно?</param>
        public static void TryInvokeIfRequired<T>(this Control control, Action<T> doIt, T arg, bool callAsync = false)
        {
            try
            {
                control.InvokeIfRequired(doIt, arg, callAsync);
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());
            }
        }

        /// <summary>
        /// Пытается выполнить действие через вызов делегата различными способами в зависимости от параметров, если это необходимо.
        /// Если <paramref name="control"/> = null, то ничего не будет вызвано, ошибки не будет.
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <param name="doIt">Делегат с некоторым действием</param>
        /// <param name="callAsync">Вызов делегата асинхронно?</param>
        public static void TryInvokeIfRequired(this Control control, Action doIt, bool callAsync = false)
        {
            try
            {
                control.InvokeIfRequired(doIt, callAsync);
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());
            }
        }
        
        #endregion
    }
}