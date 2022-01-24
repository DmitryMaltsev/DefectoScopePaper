using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Kogerent;

namespace DefectoScope
{
    /// <inheritdoc />
    /// <summary>
    ///     Карта буфера профилей
    /// </summary>
    public class ProfileBufferMap : PictureBox
    {
        /// <summary>
        /// Коэффициент масштабирования по ширине
        /// </summary>
        public float KWidth { get; private set; } = 1;

        /// <summary>
        /// Коэффициент масштабирования по высоте
        /// </summary>
        public float KHeight { get; private set; } = 1;

        /// <summary>
        /// Обновляет коэффициенты масштабирования
        /// </summary>
        public void UpdateK()
        {
            try
            {
                KWidth = (float)Width / G.ProfileSize;
            }
            catch
            {
                KWidth = 1;
            }

            try
            {
                KHeight = (float)Height / G.Settings.ProfileBufferSize;
            }
            catch
            {
                KHeight = 1;
            }
        }

        ///// <summary>
        ///// Блокировщик кода
        ///// </summary>
        //public readonly object Locker = new object();

        ///// <summary>
        ///// Добавляет профиль сверху вниз на PictureBox
        ///// </summary>
        //public void DrawProfile(byte[] profile)
        //{
        //    Bitmap bmp = null;
        //    BitmapData bmpData = null;
        //    try
        //    {
        //        var width = Width < G.ProfileSize ? Width : G.ProfileSize;

        //        lock (_locker)
        //            bmp = ((Bitmap)Image).Clone(new Rectangle(0, 0, width, G.Settings.ProfileBufferSize), PixelFormat.Format24bppRgb);
        //        var bmpWidth = bmp.Width;
        //        var bmpHeight = bmp.Height;
        //        bmpData = bmp.LockBits(new Rectangle(0, 0, bmpWidth, bmpHeight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //        var stride = bmpData.Stride;
        //        var size = stride * bmpHeight;

        //        //Последняя строка изображения, в которую кладется текущий профиль
        //        var line = new byte[stride];

        //        var j = 0; //Счетчик строки изображения
        //        for (var cI = 0; cI < bmpWidth; cI++)
        //        {
        //            //var index = (int)Math.Round(i * G.ProfileBuffer[count - 1][i] / (float)bmpWidth, MidpointRounding.ToEven);

        //            var i = KUtils.GetTruncateValue(cI / _kWidth);
        //            var point = profile[i];

        //            var color = Color.Empty;
        //            var isNeed = true;

        //            //Возможно точка принадлежит шуму
        //            if (0 <= i && i <= G.Left || G.Right <= i && i < G.ProfileSize)
        //            {
        //                color = G.Settings.NoiseColor;
        //                isNeed = false;
        //            }

        //            //Возможно точка принадлежит исключаемой зоне
        //            foreach (var excludedZone in G.ExcludedZones)
        //            {
        //                if (excludedZone.LeftX <= i && i <= excludedZone.RightX)
        //                {
        //                    color = excludedZone.FillColor;
        //                    isNeed = false;
        //                    break;
        //                }
        //            }

        //            //Если не принадлежит исключаемой зоне или шуму
        //            if (isNeed)
        //                color = KUtils.GetColorFromValue(point);

        //            line[j] = color.B;
        //            line[j + 1] = color.G;
        //            line[j + 2] = color.R;

        //            j += 3;
        //        }

        //        //Сверху вниз - нормально
        //        if (true)
        //        {
        //            Win32.CopyMemory(IntPtr.Add(bmpData.Scan0, stride), bmpData.Scan0, (uint)(size - stride));
        //            Marshal.Copy(line, 0, bmpData.Scan0, stride);
        //        }
        //        ////Снизу вверх - имеет артефакты при изменении размера области, решается сбросом данных при смене размера
        //        //else
        //        //{
        //        //    Win32.CopyMemory(bmpData.Scan0, IntPtr.Add(bmpData.Scan0, stride), (uint)(size - stride));
        //        //    Marshal.Copy(line, 0, IntPtr.Add(bmpData.Scan0, size - stride), stride);
        //        //}

        //    }
        //    /*catch (Exception e)
        //    {
        //        WriteToLog($"Возникла ошибка при добавлении профиля на {pb.Name}. {e.Message}");
        //    }*/
        //    finally
        //    {
        //        //Освобождаем картинку от блокировки в памяти если это требуется
        //        if (bmpData != null)
        //            bmp.UnlockBits(bmpData);

        //        lock (_locker)
        //            Image = bmp;

        //        _tempCount++;
        //    }
        //}

        /// <summary>
        /// Рисует границы дефектов и их метки
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        private void DrawDefects(Graphics g)
        {
            const int widthDefectRectangle = 1; //1..10

            for (var i = G.PotentialDefects.Count - 1; i >= 0; i--)
            {
                var defect = G.PotentialDefects[i];
                if (defect.IsRealDefect)
                {
                    var clientRectangle = UtilsD.GetClientRectangleFromDefect(defect, KWidth, KHeight);

                    try
                    {
                        using (var brushRect = new SolidBrush(defect.TypeDefect.MarkerColor))
                        using (var penRect = new Pen(brushRect, widthDefectRectangle))
                            g.DrawRectangle(penRect, clientRectangle);
                    }
                    catch (Exception e)
                    {
                        G.Logger.Error(e.ToString());
                    }

                    var center = clientRectangle.Center();
                    var color = defect.TypeDefect.MarkerColor;

                    try
                    {
                        UtilsD.DrawMarker(g, defect.TypeDefect.Marker, center, G.Settings.MarkerSize, color);
                    }
                    catch (Exception e)
                    {
                        G.Logger.Error(e.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Рисует границы дефектов и их метки
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        private void TryDrawDefects(Graphics g)
        {
            try
            {
                DrawDefects(g);
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());
            }
        }



        /// <summary>
        /// Рисует шкалу
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        private void DrawScaleLine(Graphics g)
        {
            //Ширина линии
            var width = G.Settings.ScaleLineWidth;

            //Выпирание деления в одну сторону
            var div = width * 2;

            //Позиция снизу вверх
            var downUpPos = div + DefaultFont.Height * 2;

            //Смещение меток
            var labelOffset = div + DefaultFont.Height / 2;

            //var pos = KUtils.GetClientValue(G.Settings.ScaleLinePosition, _kHeight);
            var pos = Height - downUpPos;

            var begin = new Point(0, pos);
            var end = new Point(Width - 1, pos);

            using (var brush = new SolidBrush(G.Settings.ScaleLineColor))
            using (var pen = new Pen(brush, width))
                g.DrawLine(pen, begin, end);

            var points = new ScalePoint[G.Settings.NScaleLineDivisions + 2];
            var lastIndex = points.Length - 1;

            var format = new StringFormat
            { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
            {
                var tLabelPoint = new Point(begin.X, begin.Y + labelOffset);
                points[0] = new ScalePoint(begin, 0.ToString(), tLabelPoint, format);
            }

            format = new StringFormat
            { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
            {
                var tLabelPoint = new Point(end.X, end.Y + labelOffset);

                var label = G.Settings.LabelsInMm
                    ? KUtils.GetRoundValue(UtilsD.GetGlFromGx(G.ProfileSize))
                    : G.ProfileSize;
                points[lastIndex] = new ScalePoint(end, label.ToString(), tLabelPoint, format);
            }

            var dPoint = (end.X - begin.X) / (float)(G.Settings.NScaleLineDivisions + 1);
            var dLabel = G.ProfileSize / (float)(G.Settings.NScaleLineDivisions + 1);

            format = new StringFormat
            { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
            for (var i = 1; i <= G.Settings.NScaleLineDivisions; i++)
            {
                var tValue = begin.X + KUtils.GetRoundValue(dPoint * i);
                var tPoint = new Point(tValue, pos);
                var tLabel = KUtils.GetRoundValue(dLabel * i);

                var label = G.Settings.LabelsInMm
                    ? KUtils.GetRoundValue(UtilsD.GetGlFromGx(tLabel))
                    : tLabel;

                var tLabelPoint = new Point(tPoint.X, tPoint.Y + labelOffset);
                points[i] = new ScalePoint(tPoint, label.ToString(), tLabelPoint, format);
            }

            foreach (var point in points)
            {
                using (var brush = new SolidBrush(G.Settings.ScaleLineColor))
                using (var pen = new Pen(brush, width))
                {
                    g.DrawLine(pen, point.P.X, point.P.Y - div, point.P.X, point.P.Y + div);
                    g.DrawString(point.Label, DefaultFont, brush, point.LabelP, point.LabelFormat);
                }
            }
        }

        /// <summary>
        /// Рисует шкалу (из-за неадекватной проблемы g.DrawString)
        /// </summary>
        /// <param name="g">Графика, на которой рисуем</param>
        private void TryDrawScaleLine(Graphics g)
        {
            try
            {
                DrawScaleLine(g);
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());
            }
        }

        //private int _tempCount;

        protected override void OnPaint(PaintEventArgs pe)
        {
            //G.Logger.WriteToLog("Вошли в OnPaint");

            //lock (Locker)
            {
                //try
                //{

                //var g = Graphics.FromImage(Image);
                var g = pe.Graphics;
                g.SmoothingMode = SmoothingMode.HighSpeed; //Долой сглаживание
                g.CompositingQuality = CompositingQuality.HighSpeed; //Скорость отрисовки
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                //g.InterpolationMode = InterpolationMode.NearestNeighbor; //Интерполяция ближайшего соседа

                base.OnPaint(pe); //Всё остальное от предка

                //OFF!
                //g.DrawString(_tempCount.ToString(), new Font(FontFamily.GenericSansSerif, 30), new SolidBrush(Color.Red), 10, 10);
                //g.DrawString(G.TempCount.ToString(), new Font(FontFamily.GenericSansSerif, 30), new SolidBrush(Color.Red), 200, 10);

                UpdateK();


                TryDrawDefects(g);

               
                if (G.Settings.ScaleLine) TryDrawScaleLine(g);
                //}
                //catch (Exception e)
                //{
                //    G.Logger.WriteProgramException(e);
                //}
            }
        }
    }
}