using System;
using System.Collections.Generic;
using System.Drawing;
using Kogerent;

namespace DefectoScope
{
    /// <inheritdoc />
    /// <summary>
    ///     Буфер профилей
    /// </summary>
    public class ProfileBuffer : RecentItemsList<byte[]>
    {
        #region Поля

        /// <summary>
        ///     Профили для усреднения
        /// </summary>
        private readonly List<byte[]> _profiles;

        #endregion

        #region Свойства



        #endregion

        /// <inheritdoc />
        /// <summary>
        ///     Создает буфер профилей с заданной максимальной емкостью
        /// </summary>
        /// <param name="maxCapacity"></param>
        public ProfileBuffer(int maxCapacity) : base(maxCapacity) =>
            _profiles = G.Settings.AutoBorders ?
            new List<byte[]>(G.Settings.NCalibrationProfiles) :
            new List<byte[]>();

        /// <summary>
        ///     Событие при добавленном профиле в буфер
        /// </summary>
        public event AddedProfileEventHandler AddedProfile;

        /// <summary>
        ///     Добавляет профиль в буфер с анализом полученного пространства
        /// </summary>
        /// <param name="item"></param>
        public new void Add(byte[] item)
        {
            base.Add(item);

            ApplySwift();

            var count = G.Settings.NCalibrationProfiles - G.NProfiles;

            if (count < 0)
            {
                try
                {
                    FindDefects(item);
                }
                catch (Exception e)
                {
                    //System.ArgumentOutOfRangeException: Индекс за пределами диапазона.
                    //Индекс должен быть положительным числом, а его размер не должен превышать размер коллекции.
                    //G.Logger.Error(e.ToString());
                }
            }

            else if (count > 0)
                _profiles.Add(item);

            else
            {
                if (G.Settings.AutoBorders)
                {
                    UtilsD.GetBordersFromProfile(UtilsD.GetMeanProfile(_profiles),
                        out G.Left, out G.Right, out G.Mean);

                    G.Logger.Debug($"{nameof(ProfileBuffer)}: Левая граница = {G.Left}; Правая граница = {G.Right}");
                }
                else
                    G.Mean = UtilsD.GetMeanProfile(_profiles).GetMeanValue();

                _profiles.Clear();

                G.Logger.Debug($"{nameof(ProfileBuffer)}: Среднее = {G.Mean}");

                //Определение авто-рецепта
                if (G.OpcClient != null && G.ConfigSettings.AutoShifts)
                {
                    var w = G.OpcClient.Weight;
                    
                    foreach (var autoShift in G.AutoShifts)
                    {
                        if (KUtils.ValueInRange(w, autoShift.Left, autoShift.Right))
                        {
                            G.NeedIdSettings = autoShift.SystemSettingId;
                            break;
                        }
                    }
                }

            }

            G.NProfiles++;
            AddedProfile?.Invoke(this, new AddedProfileEventArgs(item));
        }

        /// <summary>
        ///     Применяет сдвиг к имеющимся потенциальным дефектам
        /// </summary>
        private void ApplySwift()
        {
            var length = G.PotentialDefects.Count;

            for (var i = length - 1; i >= 0; i--)
            {
                var defect = G.PotentialDefects[i];
                defect.SwiftY();

                //Если потенциальный дефект выходит за границу
                if (defect.End.Y == MaxCapacity - 1 && Count == MaxCapacity)
                {
                    //Если дефект реальный, то добавляем потенциальный дефект в список дефектов
                    if (defect.IsRealDefect)
                    {
                        //G.Logger.WriteToLog(defect.Length.ToString());

                        if (G.Settings.Zoom)
                            defect.UpdateFrame();

                        G.Defects.Add(defect);
                    }

                    //Удаляем из потенциальных дефектов
                    G.PotentialDefects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        ///     Точка принадлежит дефекту?
        /// </summary>
        /// <param name="index">Индекс точки</param>
        /// <param name="value">Значение точки</param>
        /// <param name="isLight">Осветление?</param>
        /// <returns></returns>
        private static bool IsDefectPoint(int index, byte value, out bool isLight)
        {
            if (value > Calibration.ToleranceLight[index])
            {
                isLight = true;

                return true;
            }

            if (value < Calibration.ToleranceDark[index])
            {
                isLight = false;

                return true;
            }

            isLight = default;

            return false;
        }

        /// <summary>
        ///     Ищет дефекты в полученном пространстве
        /// </summary>
        private static void FindDefects(byte[] lastProfile)
        {
            //Никаких поисков дефектов во время смены тамбура или остановок!
            //Также во время калибровки если это выбрано.
            if (G.OpcClient != null && G.OpcClient.DontWork && G.Launched ||
                G.Settings.CalibrationWithoutDefects && G.IsCalibration)
                return;

            ////Дефектов не будет, если буфер пуст
            //if (Count <= 0)
            //    return;

            //Длина профиля
            var profileLength = lastProfile.Length;

            //Временный список дефектов (в пределах одной строки)
            var tDefects = new List<Defect>();

            //Временный дефект
            var tDefect = new Defect();

            //Отыскание дефектов в пределах последнего профиля
            for (var i = 0; i < profileLength; i++)
            {
                //Определяем, не находится ли точка в исключаемой зоне
                //var isNeed = G.ExcludedZones.All(excludedZone => excludedZone.LeftX > i || i > excludedZone.RightX);

                var isNeed = true;

                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var excludedZone in G.ExcludedZones)
                {
                    if (excludedZone.LeftX <= i && i <= excludedZone.RightX)
                    {
                        isNeed = false;
                        break;
                    }
                }

                //Возможно точка принадлежит шуму
                if (isNeed && (0 <= i && i <= G.Left || G.Right <= i && i < G.ProfileSize))
                    isNeed = false;

                //Если находится в исключаемой зоне или шуме, то переходим к следующей точке
                if (!isNeed)
                    continue;

                var isDefect = IsDefectPoint(i, lastProfile[i], out var isLight);
                var isEmpty = tDefect.End == Point.Empty;

                if (isDefect)
                {
                    //Если дефект еще не задан размерами, то ссылаемся на 1 точку
                    if (isEmpty)
                    {
                        tDefect.Begin = new Point(i, 0);
                        tDefect.End = new Point(i + 1, 1);
                        tDefect.IsLight = isLight;
                    }

                    //Если с тем же типом, то объединяем
                    else if (tDefect.IsLight == isLight)
                        tDefect.End.X++;
                    else
                    {
                        //Если площадь дефекта больше 1мм, то добавляем в список
                        if (tDefect.Square > 1)
                        {
                            tDefect.IdentifyType();
                            tDefects.Add(tDefect);
                        }

                        //Создаем новый объект дефекта
                        tDefect = new Defect();
                    }
                }

                //Если дефект был найден ранее, то сохраняем информацию о нем
                else if (!isEmpty)
                {
                    //Если площадь дефекта больше 1мм, то добавляем в список
                    if (tDefect.Square > 1)
                    {
                        tDefect.IdentifyType();
                        tDefects.Add(tDefect);
                    }

                    //Создаем новый объект дефекта
                    tDefect = new Defect();
                }
            }

            //Добавляем дефекты строки в глобальный список
            if (tDefects.Count > 0)
                G.PotentialDefects.AddRange(tDefects);

            //Проходим по всем дефектам еще раз, чтобы попытаться их объединить
            for (var i = 0; i < G.PotentialDefects.Count; i++)
            {
                var d1 = G.PotentialDefects[i];

                for (var j = G.PotentialDefects.Count - 1; j > i; j--)
                {
                    var d2 = G.PotentialDefects[j];

                    //Если дефекты разных типов, то их нельзя объединять
                    if (d1.IsLight != d2.IsLight)
                        continue;

                    //var k12 = (d1.Begin.Y >= d2.Begin.Y && d1.Begin.Y <= d2.End.Y ||
                    //           d1.End.Y >= d2.Begin.Y && d1.End.Y <= d2.End.Y) &&
                    //          (d1.Begin.X >= d2.Begin.X && d1.Begin.X <= d2.End.X ||
                    //          d1.End.X >= d2.Begin.X && d1.End.X <= d2.End.X);

                    //var k21 = (d2.Begin.Y >= d1.Begin.Y && d2.Begin.Y <= d1.End.Y ||
                    //           d2.End.Y >= d1.Begin.Y && d2.End.Y <= d1.End.Y) &&
                    //          (d2.Begin.X >= d1.Begin.X && d2.Begin.X <= d1.End.X ||
                    //          d2.End.X >= d1.Begin.X && d2.End.X <= d1.End.X);

                    //var needMerge = k12 || k21;
                    //var needMerge = d1.Rectangle.IntersectsWith(d2.Rectangle);

                    var needMerge = d1.IntersectWith(d2);

                    //Если объединяем, то сдвигаем габариты при необходимости
                    if (needMerge)
                    {
                        if (d2.Begin.X < d1.Begin.X)
                            d1.Begin.X = d2.Begin.X;
                        if (d2.Begin.Y < d1.Begin.Y)
                            d1.Begin.Y = d2.Begin.Y;
                        if (d2.End.X > d1.End.X)
                            d1.End.X = d2.End.X;
                        if (d2.End.Y > d1.End.Y)
                            d1.End.Y = d2.End.Y;

                        //Определяем тип дефекта
                        d1.IdentifyType();

                        //Удаляем второй дефект из списка дефектов
                        G.PotentialDefects.RemoveAt(j);
                    }
                }
            }
        }
    }
}