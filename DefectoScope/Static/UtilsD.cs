using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Kogerent;

namespace DefectoScope
{
    public static class UtilsD
    {

        /// <summary>
        /// Является ли валидным IP адрес указанный в строке? (с преобразованием к нужному виду)
        /// </summary>
        /// <param name="ip">Строка с IP адресом</param>
        /// <param name="newIp">Строчное представление IP адреса (после преобразования)</param>
        /// <returns></returns>
        public static bool IsValidIp(string ip, out string newIp)
        {
            if (ip.Contains(" "))
                ip = ip.Replace(" ", "");

            if (IPAddress.TryParse(ip, out var ipAddress))
            {
                newIp = ipAddress.ToString();
                return true;
            }

            newIp = default;
            return false;
        }

        /// <summary>
        /// Является ли валидным имя COM-порта указанное в строке? (с преобразованием к нужному виду)
        /// </summary>
        /// <param name="fileName">Строка с именем COM-порта</param>
        /// <param name="newFileName">Имя COM-порта, приведенное к нормальному виду</param>
        /// <returns></returns>
        public static bool IsValidComPortName(string fileName, out string newFileName)
        {
            var numbersInPortName = String.Join("", fileName.Where(Char.IsDigit));

            if (numbersInPortName.Length == 0)
            {
                newFileName = default;
                return false;
            }

            //Обрезаем под 3 цифры максимум (ручное ограничение)
            if (numbersInPortName.Length > 3)
                numbersInPortName = numbersInPortName.Remove(3);

            //Собираем имя
            newFileName = "COM" + numbersInPortName;
            return true;
        }

        /// <summary>
        /// Является ли валидным имя файла указанное в строке? (с преобразованием к нужному виду)
        /// </summary>
        /// <param name="fileName">Строка с именем файла</param>
        /// <param name="newFileName">Имя файла, приведенное к нормальному виду</param>
        /// <returns></returns>
        public static bool IsValidFileName(string fileName, out string newFileName)
        {
            //Получаем строку без невалидных для имени файла символов
            newFileName = fileName;
            foreach (var nameChar in Path.GetInvalidFileNameChars())
                newFileName = newFileName.Replace(nameChar.ToString(), "");

            //Если в результате получим пустую строчку, то файл не удался
            if (newFileName.Length == 0)
            {
                newFileName = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Получает профиль (объединенную строку) из буферов всех датчиков.
        /// Если у датчика нет данных в буфере, то заполняется опорными данными.
        /// </summary>
        /// <returns></returns>
        public static byte[] GetProfile()
        {
            //Изначально результирующий профиль копирует опорный профиль
            var result = (byte[])Calibration.Threshold.Clone();
            //var result = new byte[G.ProfileSize];
            var index = 0;

            foreach (var s in G.Sensors)
            {
                var widthWindow = s.Settings.WidthWindow;

                if (s.GotData)
                {
                    //Если буфер будет имеет меньше элементов, чем заявлена строка, то оставшиеся элементы будут заполнены также опорными элементами
                    var lBuffer = s.Buffer.Length;
                    var length = widthWindow < lBuffer ? widthWindow : lBuffer;

                    Array.Copy(s.Buffer, 0, result, index, length);

                    //G.Logger.Trace("Скопировали локальный в объединенный");
                }

                index += widthWindow;
            }

            return result;
        }

        /// <summary>
        /// Возвращает усредненный профиль из списка профилей
        /// </summary>
        /// <param name="profiles">Усредняемые профили</param>
        /// <returns></returns>
        public static byte[] GetMeanProfile(List<byte[]> profiles)
        {
            var result = new byte[G.ProfileSize];

            for (var i = 0; i < G.ProfileSize; i++)
            {
                var sum = 0;
                foreach (var profile in profiles) sum += profile[i];
                result[i] = (byte)Math.Round(sum / (float)profiles.Count, MidpointRounding.AwayFromZero);
            }

            return result;
        }

        /// <summary>
        /// Выделение подматрицы из списка массивов
        /// </summary>
        /// <param name="matrix">Список массивов</param>
        /// <param name="xBegin">Начало по X</param>
        /// <param name="xEnd">Конец по X</param>
        /// <param name="yBegin">Начало по Y</param>
        /// <param name="yEnd">Конец по Y</param>
        /// <returns></returns>
        public static T[,] ExtractionMatrix<T>(List<T[]> matrix, int xBegin, int xEnd, int yBegin, int yEnd)
        {
            var result = new T[xEnd + 1 - xBegin, yEnd + 1 - yBegin];

            var ty = 0;
            for (var y = yBegin; y <= yEnd; y++)
            {
                var tx = 0;
                for (var x = xBegin; x <= xEnd; x++)
                {
                    result[tx, ty] = matrix[y][x];
                    tx++;
                }
                ty++;
            }
            return result;
        }

        ///// <summary>
        ///// Возвращает найденные границы рабочей зоны на профиле.
        ///// Алгоритм основан на скользящем и арифметическом средних, с эмпирическими коэффициентами.
        ///// </summary>
        ///// <param name="profile">Профиль</param>
        ///// <param name="left">Левая граница</param>
        ///// <param name="right">Правая граница</param>
        ///// <param name="mean">Средний уровень профиля</param>
        //public static void GetBordersFromProfile(byte[] profile, out int left, out int right, out byte mean)
        //{
        //    var length = profile.Length;
        //    var halfLength = length / 2;
        //    var lastIndex = length - 1;

        //    //Скользящее окно в 5% от всей длины
        //    var window = (int) (length * 0.05);

        //    left = 0;
        //    right = lastIndex;

        //    //Получаем массив скользящего среднего
        //    var sma = KUtils.SimpleMovingAverage(profile, window);

        //    //Получаем среднее сглаженного массива
        //    mean = sma.GetMeanValue();
        //    G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Общее среднее = {mean}");

        //    //Берем уменьшенный в 4 раза верхний допуск от левого среднего
        //    var leftMean = sma.GetMeanValue(0, halfLength);
        //    var leftTolerance = KUtils.GetRoundValue(leftMean + 256 * G.Settings.ToleranceLight / 400f);
        //    if (leftTolerance < 0) leftTolerance = 0;
        //    if (leftTolerance > 255) leftTolerance = 255;
        //    G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Левое среднее = {leftMean}; Левый допуск = {leftTolerance}");

        //    //Берем уменьшенный в 4 раза верхний допуск от правого среднего
        //    var rightMean = sma.GetMeanValue(halfLength, lastIndex);
        //    var rightTolerance = KUtils.GetRoundValue(rightMean + 256 * G.Settings.ToleranceLight / 400f);
        //    if (rightTolerance < 0) rightTolerance = 0;
        //    if (rightTolerance > 255) rightTolerance = 255;
        //    G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Правое среднее = {leftMean}; Правый допуск = {leftTolerance}");

        //    //Ограничиваем поле допустимой ширины, чтобы точно были границы
        //    sma[0] = 255;
        //    sma[lastIndex] = 255;

        //    //G.Logger.Trace($"{nameof(GetBordersFromProfile)}: Среднее = {mean}");

        //    //Находим все пересечения скользящего среднего со средним арифметическим
        //    //var upDown = new List<int>();   //Пересечения сверху вниз
        //    //var downUp = new List<int>();   //Пересечения снизу вверх
        //    for (var i = 1; i <= halfLength; i++)
        //    {
        //        //Если разница выступов больше чем два окна, то перестаем искать
        //        if (i - left > 2*window)
        //        {
        //            G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Левая граница найдена ({i} - {left} > {2*window})");
        //            break;
        //        }

        //        //Находим последнее до середины профиля пересечение сверху вниз
        //        //При следующем превышении также смещать
        //        if (sma[i] > leftTolerance || sma[i - 1] >= leftTolerance && leftTolerance > sma[i])
        //        {
        //            left = i;
        //            //G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Смещение левой границы ({sma[i - 1]}({i-1}) >= {mean} > {sma[i]}({i}))");
        //        }
        //    }

        //    for (var i = lastIndex - 1; i > halfLength; i--)
        //    {
        //        //Если разница выступов больше чем два окна, то перестаем искать
        //        if (right - i > 2*window)
        //        {
        //            G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Правая граница найдена ({right} - {i} > {2 * window})");
        //            break;
        //        }

        //        //Находим последнее до середины профиля пересечение снизу вверх
        //        //При следующем превышении также смещать
        //        if (sma[i] > rightTolerance || sma[i - 1] < rightTolerance && rightTolerance <= sma[i])
        //        {
        //            right = i;
        //            //G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Смещение правой границы ({sma[i - 1]}({i - 1}) < {mean} <= {sma[i]}({i}))");
        //        }
        //    }

        //    ////Двигаем границы внутрь на 0.5% от общей длины профиля.
        //    ////OFF! Эксперименты...
        //    //var lengthPart = (int) (length * 0.005);
        //    //left += lengthPart;
        //    //right -= lengthPart;
        //}

        /// <summary>
        /// Возвращает найденные границы рабочей зоны на профиле.
        /// Алгоритм основан на скользящем и арифметическом средних, с эмпирическими коэффициентами.
        /// </summary>
        /// <param name="profile">Профиль</param>
        /// <param name="left">Левая граница</param>
        /// <param name="right">Правая граница</param>
        /// <param name="mean">Средний уровень профиля</param>
        public static void GetBordersFromProfile(byte[] profile, out int left, out int right, out byte mean)
        {
            var length = profile.Length;
            var halfLength = length / 2;
            var lastIndex = length - 1;

            //Скользящее окно в 1% от всей длины
            var window = (int)(length * 0.01);

            left = 0;
            right = lastIndex;

            //Получаем массив скользящего среднего
            var sma = KUtils.SimpleMovingAverage(profile, window);

            //Получаем среднее сглаженного массива
            mean = sma.GetMeanValue();
            G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Общее среднее = {mean}");

            var tolerance = 256 * G.Settings.ToleranceLight / 400f;

            //Берем уменьшенный в 4 раза верхний допуск от левого среднего
            var leftMean = sma.GetMeanValue(0, halfLength);
            var leftTolerance = KUtils.GetRoundValue(leftMean + tolerance);
            if (leftTolerance < 0) leftTolerance = 0;
            if (leftTolerance > 255) leftTolerance = 255;
            G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Левое среднее = {leftMean}; Левый допуск = {leftTolerance}");

            //Берем уменьшенный в 4 раза верхний допуск от правого среднего
            var rightMean = sma.GetMeanValue(halfLength, lastIndex);
            var rightTolerance = KUtils.GetRoundValue(rightMean + tolerance);
            if (rightTolerance < 0) rightTolerance = 0;
            if (rightTolerance > 255) rightTolerance = 255;
            G.Logger.Debug($"{nameof(GetBordersFromProfile)}: Правое среднее = {leftMean}; Правый допуск = {leftTolerance}");

            //Ограничиваем поле допустимой ширины, чтобы точно были границы
            sma[0] = 255;
            sma[lastIndex] = 255;

            //G.Logger.Trace($"{nameof(GetBordersFromProfile)}: Среднее = {mean}");

            //Находим все пересечения скользящего среднего со средним арифметическим
            for (var i = halfLength; i >= 1; i--)
            {
                //Находим первое от середины профиля пересечение снизу вверх
                if (sma[i - 1] >= leftTolerance && leftTolerance > sma[i])
                {
                    //G.Logger.Trace($"{nameof(GetBordersFromProfile)}: Левая смещается в {i}");
                    left = i;
                    break;
                }
                //else
                //{
                //    G.Logger.Trace($"{nameof(GetBordersFromProfile)}: Левая НЕТ: {sma[i]}({i}) >= {leftTolerance} > {sma[i-1]}({i-1})");
                //}
            }

            for (var i = halfLength + 1; i <= lastIndex - 1; i++)
            {
                //Находим первое от середины профиля пересечение сверху вниз
                if (sma[i - 1] < rightTolerance && rightTolerance <= sma[i])
                {
                    //G.Logger.Trace($"{nameof(GetBordersFromProfile)}: Правая смещается в {i}");
                    right = i;
                    break;
                }
                //else
                //{
                //    G.Logger.Trace($"{nameof(GetBordersFromProfile)}: Правая НЕТ: {sma[i-1]}({i-1}) < {rightTolerance} <= {sma[i]}({i})");
                //}
            }
        }

        /// <summary>
        /// Все датчики в порядке? (проверка)
        /// </summary>
        /// <param name="status">Массив состояний датчиков: не инициализировано (-1), исправен (0), не исправен (1)</param>
        public static bool SensorsIsOk(out SensorStatus[] status)
        {
            var isOk = true;
            var nSensors = G.Sensors.Length;
            status = new SensorStatus[nSensors];

            for (var i = 0; i < G.Sensors.Length; i++)
            {
                var sensor = G.Sensors[i];

                if (!sensor.Initialized)
                {
                    status[i] = SensorStatus.NoInitialization;
                    continue;
                }

                if (!sensor.IsEnabled || !sensor.GetTemper())
                {
                    status[i] = SensorStatus.Bad;
                    sensor.IsEnabled = false;
                    isOk = false;

                }
                else
                    status[i] = SensorStatus.Good;
            }

            return isOk;
        }

        /// <summary>
        /// Средний уровень профилей в порядке? Последний "калибровочный" профиль должен быть в диапазоне от 64 до 192.
        /// </summary>
        /// <returns></returns>
        public static bool MeanIsOk() => G.NProfiles <= G.Settings.NCalibrationProfiles || KUtils.ValueInRange(G.Mean, 32, 224);

        public static void TempFillInputBuffer(int count = 1000)
        {
            var r = new Random();
            var profiles = new byte[count][];
            for (var i = 0; i < profiles.Length; i++)
            {
                profiles[i] = new byte[G.ProfileSize];
                for (var j = 0; j < profiles[i].Length; j++)
                    profiles[i][j] = (byte)r.Next(Calibration.ToleranceDark[j], Calibration.ToleranceLight[j]);
            }

            foreach (var profile in profiles) G.InputBuffer.Write(profile);
        }

        public static void TempFillInputBuffer2(int count = 1000)
        {
            var r = new Random();
            var profiles = new byte[count][];
            for (var i = 0; i < profiles.Length; i++)
            {
                profiles[i] = new byte[G.ProfileSize];
                for (var j = 0; j < profiles[i].Length; j++)
                    profiles[i][j] = (byte)r.Next(0, 255);
            }

            foreach (var profile in profiles) G.InputBuffer.Write(profile);
        }

        public static void TempFillInputBuffer3(int count = 1000)
        {
            var r = new Random();
            var profiles = new byte[count][];
            for (var i = 0; i < profiles.Length; i++)
            {
                profiles[i] = new byte[G.ProfileSize];
                for (var j = 0; j < profiles[i].Length; j++)
                {
                    var dark = Calibration.ToleranceDark[j] / 2;
                    if (dark < 0) dark = 0;

                    var light = Calibration.ToleranceLight[j] * 2;
                    if (light > 255) light = 255;

                    profiles[i][j] = (byte)r.Next(dark, light);
                }
            }

            foreach (var profile in profiles) G.InputBuffer.Write(profile);
        }

        /// <summary>
        /// Инициализирует график профиля
        /// </summary>
        /// <param name="chart">График</param>
        public static void InitProfileChart(Chart chart)
        {
            chart.ChartAreas.Clear();
            var area = new ChartArea { BackColor = SystemColors.Control };
            chart.ChartAreas.Add(area);

            chart.Series.Clear();
            chart.Series.Add(new Series { Color = G.Settings.CalibrationDataColor });
            chart.Series.Add(new Series { Color = G.Settings.ToleranceDarkColor });
            chart.Series.Add(new Series { Color = G.Settings.ToleranceLightColor });
            chart.Series.Add(new Series { Color = G.Settings.ProfileColor });

            foreach (var series in chart.Series)
            {
                series.ChartType = SeriesChartType.FastPoint;
                series.ChartArea = chart.ChartAreas[0].Name;
                series.Palette = ChartColorPalette.None;
                series.MarkerSize = 1;
            }
            chart.Series[3].MarkerSize = 3;

            chart.TryInvokeIfRequired(
                () =>
                {
                    chart.Series[0].Points.Clear();

                    for (var i = 0; i < Calibration.Threshold.Length; i++)
                    {
                        var x = G.Settings.LabelsInMm ? KUtils.GetRoundValue(GetGlFromGx(i)) : i;
                        chart.Series[0].Points.AddXY(x, Calibration.Threshold[i]);
                    }
                }
            );

            chart.TryInvokeIfRequired(
                () =>
                {
                    chart.Series[1].Points.Clear();

                    for (var i = 0; i < Calibration.ToleranceDark.Length; i++)
                    {
                        var x = G.Settings.LabelsInMm ? KUtils.GetRoundValue(GetGlFromGx(i)) : i;
                        chart.Series[1].Points.AddXY(x, Calibration.ToleranceDark[i]);
                    }
                }
            );

            chart.TryInvokeIfRequired(
                () =>
                {
                    chart.Series[2].Points.Clear();

                    for (var i = 0; i < Calibration.ToleranceLight.Length; i++)
                    {
                        var x = G.Settings.LabelsInMm ? KUtils.GetRoundValue(GetGlFromGx(i)) : i;
                        chart.Series[2].Points.AddXY(x, Calibration.ToleranceLight[i]);
                    }
                }
            );
        }

        /// <summary>
        /// Инициализирует список конфигураций
        /// </summary>
        /// <param name="cbId"></param>
        public static void InitListIdConfig(ComboBox cbId)
        {
            cbId.SelectedIndex = -1;
            cbId.Items.Clear();
            cbId.Items.AddRange(G.Settings.GetNames());

            //Выбираем конфигурацию в соответствие с настройками
            var id = G.ConfigSettings.SystemSettings;
            cbId.SelectedIndex = cbId.Items.IndexOf(id);
        }

        /// <summary>
        /// Инициализирует таблицу дефектов
        /// </summary>
        /// <param name="dgv"></param>
        public static void InitDefectsTable(DataGridView dgv)
        {
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;

            dgv.MultiSelect = false;
            dgv.ReadOnly = true;

            //Виртуальный режим был применен к задаче ранее
            //Но быстродействие системы хуже
            dgv.VirtualMode = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.BackgroundColor = SystemColors.Control;

            dgv.Columns.Clear();

            string[] header = { "Тип дефекта", "Площадь (мм2)", "На длине (м)", "На ширине (м)", "Время" };

            foreach (var s in header)
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn
                {
                    HeaderText = s,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns.Add(column);
            }
        }

        /// <summary>
        /// Возвращает прямоугольник в координатах клиентской области из координат дефекта
        /// </summary>
        /// <param name="defect">Дефект</param>
        /// <param name="kWidth">Коэффициент увеличения ширины</param>
        /// <param name="kHeight">Коэффициент увеличения высоты</param>
        /// <returns></returns>
        public static Rectangle GetClientRectangleFromDefect(Defect defect, float kWidth, float kHeight)
        {
            var x = KUtils.GetClientValue(defect.Begin.X, kWidth);
            var y = KUtils.GetClientValue(defect.Begin.Y, kHeight);
            var width = KUtils.GetClientValue(defect.Width, kWidth);
            var height = KUtils.GetClientValue(defect.Length, kHeight);

            return new Rectangle(x, y, width, height);
        }

        ///// <summary>
        ///// Объединяет строки всех камер в единую строку
        ///// </summary>
        ///// <returns></returns>
        //public static byte[] ConcatAllRows()
        //{
        //    //Если датчиков нет, то выводим пустую строку
        //    if (G.Sensors.Length <= 0)
        //        return new byte[0];

        //    //Размер строки установлен при настройках
        //    var result = new byte[S.ProfileSize];

        //    //Копируем первый строку первого датчика
        //    G.Sensors[0].Row.CopyTo(result, 0);
        //    //Buffer.BlockCopy(G.Sensors[0].Row, 0, result, 0, G.Sensors[0].Row.Length);

        //    //Копируем оставшиеся строки
        //    for (var i = 1; i < G.Sensors.Length; i++)
        //        G.Sensors[i].Row.CopyTo(result, G.Sensors[i - 1].Row.Length);
        //    //Buffer.BlockCopy(G.Sensors[i].Row, 0, result, G.Sensors[i-1].Row.Length, G.Sensors[i].Row.Length);

        //    return result;
        //}

        #region Графика

        #region Маркеры

        /// <summary>
        /// Рисует квадрат
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        /// <param name="center">Точка центра</param>
        /// <param name="size">Размер</param>
        /// <param name="color">Цвет</param>
        private static void DrawSquare(Graphics g, PointF center, float size, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                var rect = new RectangleF(center.X - size, center.Y - size, size * 2, size * 2);
                g.FillRectangle(brush, rect);
            }

        }

        /// <summary>
        /// Рисует треугольник
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        /// <param name="center">Точка центра</param>
        /// <param name="size">Размер</param>
        /// <param name="color">Цвет</param>
        private static void DrawTriangle(Graphics g, PointF center, float size, Color color)
        {
            var points = new[] { new PointF(1, 1), new PointF(0, -1), new PointF(-1, 1) };
            var tPoints = KUtils.TransformIdentityPolygon(points, center, size);
            using (var brush = new SolidBrush(color)) g.FillPolygon(brush, tPoints);
        }

        /// <summary>
        /// Рисует круг
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        /// <param name="center">Точка центра</param>
        /// <param name="size">Размер</param>
        /// <param name="color">Цвет</param>
        private static void DrawCircle(Graphics g, PointF center, float size, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                var rect = new RectangleF(center.X - size, center.Y - size, size * 2, size * 2);
                g.FillEllipse(brush, rect);
            }
        }

        /// <summary>
        /// Рисует круг
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        /// <param name="center">Точка центра</param>
        /// <param name="size">Размер</param>
        /// <param name="color">Цвет</param>
        private static void DrawRhombus(Graphics g, PointF center, float size, Color color)
        {
            var points = new[] { new PointF(-1, 0), new PointF(0, 1), new PointF(1, 0), new PointF(0, -1) };
            var tPoints = KUtils.TransformIdentityPolygon(points, center, size);
            using (var brush = new SolidBrush(color)) g.FillPolygon(brush, tPoints);
        }

        /// <summary>
        /// Отрисовывает маркер на графике
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        /// <param name="marker">Тип маркера</param>
        /// <param name="center">Центр расположения маркера</param>
        /// <param name="size">Размер маркера</param>
        /// <param name="color">Цвет маркера</param>
        public static void DrawMarker(Graphics g, Marker marker, PointF center, float size, Color color)
        {
            switch (marker)
            {
                case Marker.Square:
                    DrawSquare(g, center, size, color);
                    break;
                case Marker.Triangle:
                    DrawTriangle(g, center, size, color);
                    break;
                case Marker.Circle:
                    DrawCircle(g, center, size, color);
                    break;
                case Marker.Rhombus:
                    DrawRhombus(g, center, size, color);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #endregion


        #region Переводы

        #region Пиксели

        /// <summary>
        /// Возвращает номер датчика, который владеет данной глобальной координатой X.
        /// </summary>
        /// <param name="gX">Глобальная координата X</param>
        /// <returns></returns>
        public static byte GetNSensorFromGx(int gX)
        {
            var tX = 0;
            foreach (var sensor in G.Sensors)
            {
                if (gX > sensor.Settings.WidthWindow + tX)
                    tX += sensor.Settings.WidthWindow;
                else
                    return sensor.NDevice;
            }

            throw new Exception($"Глобальная координата X = {gX} не принадлежит ни одному датчику!");
        }

        /// <summary>
        /// Ищет номер датчика, который владеет данной глобальной координатой X.
        /// <para>True - если датчик нашелся.</para>
        /// </summary>
        /// <param name="gX">Глобальная координата X</param>
        /// <param name="nSensor">Номер датчика</param>
        /// <returns>Датчик нашелся?</returns>
        public static bool FindNSensorFromGx(int gX, out byte nSensor)
        {
            try
            {
                nSensor = GetNSensorFromGx(gX);
                return true;
            }
            catch
            {
                nSensor = default;
                return false;
            }
        }

        /// <summary>
        /// Возвращает локальную координату X из глобальной.
        /// </summary>
        /// <param name="gX">Глобальная координата X</param>
        /// <param name="nSensor">Номер датчика</param>
        /// <returns></returns>
        public static int GetXFromGx(int gX, out byte nSensor)
        {
            var tX = 0;
            foreach (var sensor in G.Sensors)
            {
                if (gX >= sensor.Settings.WidthWindow + tX)
                    tX += sensor.Settings.WidthWindow;
                else
                {
                    nSensor = sensor.NDevice;
                    return gX - tX;
                }
            }

            throw new Exception($"Глобальная координата X = {gX} не принадлежит ни одному датчику!");
        }

        /// <summary>
        /// Ищет локальную координату X из глобальной.
        /// <para>True - если локальная координата X нашлась.</para>
        /// </summary>
        /// <param name="x">Глобальная координата X</param>
        /// <param name="gX">Локальная координата X</param>
        /// <param name="nSensor">Номер датчика</param>
        /// <returns>Локальная координата X нашлась?</returns>
        public static bool FindXFromGx(int x, out int gX, out byte nSensor)
        {
            try
            {
                gX = GetXFromGx(x, out nSensor);
                return true;
            }
            catch
            {
                gX = default;
                nSensor = default;
                return false;
            }
        }

        #endregion

        #region Миллиметры

        /// <summary>
        /// Возвращает номер датчика, который владеет данной глобальной координатой L.
        /// </summary>
        /// <param name="gL">Глобальная координата L</param>
        /// <returns></returns>
        public static byte GetNSensorFromGl(float gL)
        {
            double tL = 0;
            foreach (var sensor in G.Sensors)
            {
                var dL = sensor.Settings.LWindow;

                if (gL > dL + tL)
                    tL += dL;
                else
                    return sensor.NDevice;
            }

            throw new Exception($"Глобальная координата L = {gL} не принадлежит ни одному датчику!");
        }

        /// <summary>
        /// Ищет номер датчика, который владеет данной глобальной координатой L.
        /// <para>True - если датчик нашелся.</para>
        /// </summary>
        /// <param name="gL">Глобальная координата L</param>
        /// <param name="nSensor">Номер датчика</param>
        /// <returns>Датчик нашелся?</returns>
        public static bool FindNSensorFromGl(float gL, out byte nSensor)
        {
            try
            {
                nSensor = GetNSensorFromGl(gL);
                return true;
            }
            catch
            {
                nSensor = default;
                return false;
            }
        }

        /// <summary>
        /// Возвращает локальную координату L из глобальной.
        /// </summary>
        /// <param name="gL">Глобальная координата X</param>
        /// <param name="nSensor">Номер датчика</param>
        /// <returns></returns>
        public static float GetLFromGl(float gL, out byte nSensor)
        {
            double tL = 0;
            foreach (var sensor in G.Sensors)
            {
                var dL = sensor.Settings.LWindow;

                if (gL >= dL + tL)
                    tL += dL;
                else
                {
                    nSensor = sensor.NDevice;
                    return gL - (float)tL;
                }
            }

            throw new Exception($"Глобальная координата L = {gL} не принадлежит ни одному датчику!");
        }

        /// <summary>
        /// Ищет локальную координату L из глобальной.
        /// <para>True - если локальная координата L нашлась.</para>
        /// </summary>
        /// <param name="l">Глобальная координата L</param>
        /// <param name="gL">Локальная координата L</param>
        /// <param name="nSensor">Номер датчика</param>
        /// <returns>Локальная координата X нашлась?</returns>
        public static bool FindLFromGl(float l, out float gL, out byte nSensor)
        {
            try
            {
                gL = GetLFromGl(l, out nSensor);
                return true;
            }
            catch
            {
                gL = default;
                nSensor = default;
                return false;
            }
        }

        #endregion

        #region Пиксели ↔ Миллиметры

        /// <summary>
        /// Возвращает глобальную координату L из глобальной координаты X
        /// </summary>
        /// <param name="gX">Глобальная координата x</param>
        /// <returns></returns>
        public static float GetGlFromGx(int gX)
        {
            var tX = 0;
            double tL = 0;

            foreach (var sensor in G.Sensors)
            {
                if (gX > sensor.Settings.WidthWindow + tX)
                {
                    tX += sensor.Settings.WidthWindow;
                    tL += sensor.Settings.LWindow;
                }
                else
                    return (float)((gX - tX) / sensor.Settings.KPixelsToMm + tL);
            }

            throw new Exception($"Глобальная координата X = {gX} не принадлежит ни одному датчику!");
        }

        /// <summary>
        /// Возвращает глобальную координату X из глобальной координаты L.
        /// </summary>
        /// <param name="gL">Глобальная координата L</param>
        /// <returns></returns>
        public static int GetGxFromGl(float gL)
        {
            var tX = 0;
            double tL = 0;

            foreach (var sensor in G.Sensors)
            {
                var dL = sensor.Settings.LWindow;

                if (gL > dL + tL)
                {
                    tX += sensor.Settings.WidthWindow;
                    tL += dL;
                }
                else
                    return (int)Math.Round((gL - tL) * sensor.Settings.KPixelsToMm) + tX;
            }

            throw new Exception($"Глобальная координата L = {gL} не принадлежит ни одному датчику!");
        }

        /// <summary>
        /// Ищет глобальную координату L из глобальной координаты X.
        /// <para>True - если глобальная координата L нашлась.</para>
        /// </summary>
        /// <param name="gX">Глобальная координата X</param>
        /// <param name="gL">Глобальная координата L</param>
        /// <returns>Глобальная координата L нашлась?</returns>
        public static bool FindGlFromGx(int gX, out float gL)
        {
            try
            {
                gL = GetGlFromGx(gX);
                return true;
            }
            catch
            {
                gL = default;
                return false;
            }
        }

        /// <summary>
        /// Ищет глобальную координату X из глобальной координаты L.
        /// <para>True - если локальная координата X нашлась.</para>
        /// </summary>
        /// <param name="gL">Локальная координата L</param>
        /// <param name="gX">Локальная координата X</param>
        /// <returns>Локальная координата X нашлась?</returns>
        public static bool FindGxFromGl(float gL, out int gX)
        {
            try
            {
                gX = GetGxFromGl(gL);
                return true;
            }
            catch
            {
                gX = default;
                return false;
            }
        }

        /// <summary>
        /// Возвращает глобальную координату D из глобальной координаты Y
        /// </summary>
        /// <param name="gY">Глобальная координата Y</param>
        /// <returns></returns>
        public static float GetGdFromGy(int gY) => gY * G.Settings.MmInStep;

        /// <summary>
        /// Возвращает глобальную координату Y из глобальной координаты D
        /// </summary>
        /// <param name="gD">Глобальная координата D</param>
        /// <returns></returns>
        public static int GetGyFromGd(float gD) => KUtils.GetRoundValue(gD / G.Settings.MmInStep);

        #endregion

        #endregion

        /// <summary>
        /// Возвращает позицию центра дефекта по ширине в метрах.
        /// </summary>
        /// <param name="defect">Дефект</param>
        /// <returns></returns>
        public static float GetXCoordCenter(this Defect defect) => (GetGlFromGx(defect.Center.X) - GetGlFromGx(G.Left)) / 1000;

        /// <summary>
        /// Возвращает позицию центра дефекта по длине в метрах.
        /// </summary>
        /// <param name="defect">Дефект</param>
        /// <returns></returns>
        public static float GetYCoordCenter(this Defect defect) => (GetGdFromGy(defect.Center.Y) + GetGdFromGy(G.NProfiles)) / 1000;
    }
}