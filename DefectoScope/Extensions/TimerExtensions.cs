namespace DefectoScope
{
    /// <summary>
    /// Была такая нормальная обертка.. но исключения вызываются слишком непрозрачные, мы не знаем где сломались...
    /// </summary>
    public static class TimerExtensions
    {
        ///// <summary>
        ///// Выполняет событие с остановкой таймера
        ///// </summary>
        ///// <param name="timer">Таймер</param>
        ///// <param name="action">Событие</param>
        //public static void DoEventWithTimerPause(this Timer timer, Action action)
        //{
        //    try
        //    {
        //        timer.Enabled = false;
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.Error(e.ToString());

        //        //Если таймера нет, ничего не делаем
        //        return;
        //    }

        //    try
        //    {
        //        action.Invoke();
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.Error(e.ToString());
        //    }

        //    try
        //    {
        //        timer.Enabled = true;
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.Error(e.ToString());
        //    }
        //}

        ///// <summary>
        ///// Выполняет событие с остановкой таймера
        ///// </summary>
        ///// <param name="timer">Таймер</param>
        ///// <param name="action">Событие</param>
        //public static async Task DoEventWithTimerPauseAsync(this Timer timer, Action action)
        //{
        //    try
        //    {
        //        timer.Enabled = false;
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.Error(e.ToString());

        //        //Если таймера нет, ничего не делаем
        //        return;
        //    }

        //    try
        //    {
        //        await Task.Run(action.Invoke);
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.Error(e.ToString());
        //    }

        //    try
        //    {
        //        timer.Enabled = true;
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.Error(e.ToString());
        //    }
        //}
    }
}