using System.Drawing;
using System.Windows.Forms;

namespace DefectoScope
{
    /// <summary>
    /// Ячейка карты дефектов
    /// </summary>
    public partial class DefectsMapCell : UserControl
    {
        /// <summary>
        /// Дефекты, находящиеся внутри этой ячейки (по возрастанию, чтобы малые удалялись)
        /// </summary>
        public Defects Defects { get; set; } = new Defects(100);

        /// <summary>
        /// Границы дефектов
        /// </summary>
        public Rectangle DefectsRectangle { get; set; } = new Rectangle();

        /// <summary>
        /// Текущая вместимость ячейки (символами)
        /// </summary>
        public int Capacity { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// При создании ячейки карты необходимо задать ей размер
        /// </summary>
        public DefectsMapCell()
        {
            InitializeComponent();
            //Defects.AddedDefect += (sender, args) => RefreshIfNeeded(args.Index);
            //Defects.RemovedDefect += (sender, args) => RefreshIfNeeded(args.Index);
        }

        //private void RefreshIfNeeded(int index)
        //{
        //    if (index < Capacity)
        //        Refresh();
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                base.OnPaint(pe);

                //var rnd = new Random(pe.GetHashCode());
                //var brush = new SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                ////pe.Graphics.FillRectangle(brush, new Rectangle(new Point(0, 0), Size));
                //pe.Graphics.FillRectangle(brush, pe.ClipRectangle);

                //var def = new Defect();
                //def.TypeDefect = G.TypeDefects[0];
                //this.Defects.Add(def);

                //G.ProfileBuffer.Add(new byte[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9});


                var g = pe.Graphics;
                var rect = pe.ClipRectangle;
                var halfMarkerSize = G.Settings.MarkerSize;
                var markerSize = halfMarkerSize * 2;


                var nRows = rect.Height / markerSize;
                var nColumns = rect.Width / markerSize;
                Capacity = nRows * nColumns;

                //Для центровки элементов
                var divRow = (rect.Height - markerSize * nRows) / 2;
                var divColumn = (rect.Width - markerSize * nColumns) / 2;

                var count = Defects.Count - 1;
                for (var r = 0; r < nRows; r++)
                {
                    if (count < 0) break;

                    for (var c = 0; c < nColumns; c++)
                    {
                        if (count < 0) break;

                        var defect = Defects[count--];
                        var type = defect.TypeDefect;

                        var center = new Point(markerSize * c + halfMarkerSize + divColumn,
                            markerSize * r + halfMarkerSize + divRow);

                        UtilsD.DrawMarker(g, type.Marker, center, halfMarkerSize, type.MarkerColor);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
