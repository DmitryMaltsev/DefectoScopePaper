using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Kogerent;

namespace DefectoScope
{
    public class DefectZoom : PictureBox
    {
        ///// <summary>
        ///// Коэффициент масштабирования по ширине
        ///// </summary>
        //private float _kWidth = 1;

        ///// <summary>
        ///// Коэффициент масштабирования по высоте
        ///// </summary>
        //private float _kHeight = 1;

        ////Ширины различный линий
        ////OFF: сделать настраиваемыми
        //private const float WidthScaleLine = 1f;

        ////Размер маркера
        ////OFF: сделать настраиваемым
        //private const float MarkerSize = 5f;

        ///// <summary>
        ///// Блокировщик кода
        ///// </summary>
        //private readonly object _locker = new object();

        ///// <summary>
        ///// Обновляет коэффициенты под выбранный дефект
        ///// </summary>
        ///// <param name="defect">Дефект</param>
        //private void UpdateK(Defect defect)
        //{
        //    try
        //    {
        //        _kWidth = (float)Image.Width / defect.Frame.GetLength(0);
        //    }
        //    catch
        //    {
        //        _kWidth = 1;
        //    }

        //    try
        //    {
        //        _kHeight = (float)Image.Height / defect.Frame.GetLength(1);
        //    }
        //    catch
        //    {
        //        _kHeight = 1;
        //    }
        //}

        /// <summary>
        /// Рисует дефект
        /// </summary>
        /// <param name="defect">Дефект</param>
        public void DrawDefect(Defect defect)
        {
            Image = defect.Frame.ConvertByteMatrixToBitmap(G.Settings.DarkAreaColor, G.Settings.LightAreaColor);
            //SizeMode = defect.Frame.GetLength(0) < Width || defect.Frame.GetLength(1) < Height
            //    ? PictureBoxSizeMode.CenterImage
            //    : PictureBoxSizeMode.Zoom;
            //UpdateK(defect);
        }

        ///// <summary>
        ///// Рисует шкалу
        ///// </summary>
        ///// <param name="g">Графика, на которой рисуем</param>
        //private void DrawScaleLine(Graphics g)
        //{
        //    const int div = 3;

        //    var width = Width;
        //    var height = Height;

        //    var wm = width - div;
        //    var hm = height - div;
        //    var wp = width + div;
        //    var hp = height + div;

        //    var begin = new Point(0, hm);
        //    var endX = new Point(wm, hm);
        //    var endY = new Point(0, div);

        //    using (var brush = new SolidBrush(G.Settings.ScaleLineColor))
        //    using (var pen = new Pen(brush, WidthScaleLine))
        //    {
        //        g.DrawLine(pen, begin, endX);
        //        g.DrawLine(pen, begin, endY);
        //    }

        //    var points = new ScalePoint[1 + 2];
        //    var last = points.Length - 1;

        //    var format = new StringFormat
        //    { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        //    {
        //        var tLabelPoint = new Point(begin.X, begin.Y + div * 2);
        //        points[0] = new ScalePoint(begin, 0.ToString(), tLabelPoint, format);
        //    }

        //    format = new StringFormat
        //    { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        //    {
        //        var tLabelPoint = new Point(endY.X, endY.Y + div * 2);
        //        points[last] =
        //            new ScalePoint(endY, G.Settings.ScaleLineMaxValue.ToString(), tLabelPoint, format);
        //    }

        //    format = new StringFormat
        //        { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
        //    {
        //        var tLabelPoint = new Point(endX.X, endX.Y + div * 2);
        //        points[last] =
        //            new ScalePoint(endX, G.Settings.ScaleLineMaxValue.ToString(), tLabelPoint, format);
        //    }

        //    //var dPoint = (end.X - begin.X) / (float)(G.Settings.ScaleLineDivisionsNumbers + 1);
        //    //var dLabel = G.Settings.ScaleLineMaxValue / (float)(G.Settings.ScaleLineDivisionsNumbers + 1);

        //    //format = new StringFormat
        //    //{ Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        //    //for (var i = 1; i <= G.Settings.ScaleLineDivisionsNumbers; i++)
        //    //{
        //    //    var tValue = begin.X + KUtils.GetRoundValue(dPoint * i);
        //    //    var tPoint = new Point(tValue, position);
        //    //    var tLabel = KUtils.GetRoundValue(dLabel * i);
        //    //    var tLabelPoint = new Point(tPoint.X, tPoint.Y + div * 2);
        //    //    points[i] = new ScalePoint(tPoint, tLabel.ToString(), tLabelPoint, format);
        //    //}

        //    foreach (var point in points)
        //        using (var brush = new SolidBrush(G.Settings.ScaleLineColor))
        //        using (var pen = new Pen(brush, WidthScaleLine))
        //        {
        //            g.DrawLine(pen, point.P.X, point.P.Y - div, point.P.X, point.P.Y + div);
        //            g.DrawString(point.Label, DefaultFont, brush, point.LabelP, point.LabelFormat);
        //        }
        //}

        ///// <summary>
        ///// Рисует шкалу (из-за неадекватной проблемы g.DrawString)
        ///// </summary>
        ///// <param name="g">Графика, на которой рисуем</param>
        //private void TryDrawScaleLine(Graphics g)
        //{
        //    try
        //    {
        //        DrawScaleLine(g);
        //    }
        //    catch (Exception e)
        //    {
        //        G.Logger.WriteProgramException(e);
        //    }
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            //G.Logger.WriteToLog("Вошли в OnPaint");

            //lock (_locker)
            {
                //try
                //{
                var g = pe.Graphics;
                g.SmoothingMode = SmoothingMode.None;
                g.CompositingQuality = CompositingQuality.HighQuality; 
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.InterpolationMode = InterpolationMode.Low;

                base.OnPaint(pe); //Всё остальное от предка

                //if (G.Settings.ScaleLine) TryDrawScaleLine(g);
                //}
                //catch (Exception e)
                //{
                //    G.Logger.WriteProgramException(e);
                //}
            }
        }


    }
}