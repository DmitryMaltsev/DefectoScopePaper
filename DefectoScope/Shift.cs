using System;

namespace DefectoScope
{
    /// <summary>
    /// Рабочая смена
    /// </summary>
    public enum ShiftE
    {
        /// <summary>
        /// Неизвестная
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// 7:45 - 15:45
        /// </summary>
        Day,

        /// <summary>
        /// 15:45 - 23:55
        /// </summary>
        Evening,

        /// <summary>
        /// 23:55 - 7:45
        /// </summary>
        Night
    }

    /// <summary>
    /// Рабочие смены
    /// </summary>
    public class Shift
    {
        public Shift()
        {
            Current = ShiftE.Unknown;

            var now = DateTime.Now;
            PrevEndTime = now;
            BeginTime = now;
            EndTime = now;
        }

        #region Поля

        /// <summary>
        /// Ключевые времена
        /// </summary>
        private readonly string[] _times = { "7:45", "15:45", "23:55" };

        #endregion

        #region Свойства
        
        /// <summary>
        /// Текущая смена
        /// </summary>
        public ShiftE Current { get; private set; }

        /// <summary>
        /// Конец времен предыдущей смены
        /// </summary>
        public DateTime PrevEndTime { get; private set; }

        /// <summary>
        /// Начало времен
        /// </summary>
        public DateTime BeginTime { get; private set; }

        /// <summary>
        /// Конец времен
        /// </summary>
        public DateTime EndTime { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Считывает и возвращает актуальные дата-время
        /// </summary>
        public DateTime[] GetActualTimes(DateTime now)
        {
            var actual = new DateTime[_times.Length];

            //Преобразуем текстовый массив в даты
            for (var i = 0; i < _times.Length; i++)
            {
                var timeParts = _times[i].Split(':');
                actual[i] = new DateTime(now.Year, now.Month, now.Day,
                    int.Parse(timeParts[0]), int.Parse(timeParts[1]), 0);
            }

            //Актуальные даты всегда больше текущей
            for (var i = 0; i < actual.Length; i++)
            {
                var time = actual[i];
                if (now > time)
                    actual[i] = time.AddDays(1);
            }

            return actual;
        }

        /// <summary>
        /// Смена в действительности не соответствует текущей <see cref="Current"/>?
        /// </summary>
        /// <returns></returns>
        private bool Changed(out ShiftE current, out DateTime beginTime, out DateTime endTime)
        {
            var now = DateTime.Now;
            var actual = GetActualTimes(now);

            var length = actual.Length;
            var lastIndex = length - 1;

            DateTime begin, end;
            for (var i = 0; i < lastIndex; i++)
            {
                begin = actual[i];
                end = actual[i + 1];

                if (begin.TimeOfDay <= now.TimeOfDay && now.TimeOfDay < end.TimeOfDay)
                {
                    if (Current == ShiftE.Unknown)
                        EndTime = i == 0 ? actual[lastIndex] : actual[i - 1];

                    else if (Current == (ShiftE)i)
                    {
                        current = Current;
                        beginTime = BeginTime;
                        endTime = EndTime;

                        return false;
                    }

                    current = (ShiftE)i;
                    beginTime = begin;
                    endTime = end;

                    return true;
                }
            }

            begin = actual[lastIndex];
            end = actual[0];

            if (begin.TimeOfDay <= now.TimeOfDay && now.TimeOfDay < end.TimeOfDay)
            {
                if (Current == ShiftE.Unknown)
                    EndTime = actual[lastIndex - 1];

                else if (Current == ShiftE.Night)
                {
                    current = Current;
                    beginTime = BeginTime;
                    endTime = EndTime;

                    return false;
                }

                current = ShiftE.Night;
                beginTime = begin;
                endTime = end;

                return true;
            }

            current = Current;
            beginTime = BeginTime;
            endTime = EndTime;

            return false;
        }

        /// <summary>
        /// Обновляет текущую смену если она изменилась
        /// </summary>
        public bool UpdateCurrent()
        {
            var res = Changed(out var current, out var beginTime, out var endTime);

            if (res)
            {
                PrevEndTime = EndTime;
                Current = current;
                BeginTime = beginTime;
                EndTime = endTime;
            }

            return res;
        }
        
        #endregion


    }
}