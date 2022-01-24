using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Kogerent;

namespace DefectoScope
{
    /// <summary>
    ///     Карта дефектов
    /// </summary>
    public partial class DefectsMap : UserControl
    {
        /// <summary>
        ///     Смещение от границ
        /// </summary>
        private const int Div = 5;

        /// <summary>
        ///     Высота меток
        /// </summary>
        private const int HLabel = 20;

        /// <summary>
        ///     Ячейки карты дефектов
        /// </summary>
        private DefectsMapCell[,] _cells = new DefectsMapCell[0, 0];

        /// <summary>
        ///     Массив ячеек карты дефектов
        /// </summary>
        private DefectsMapCell[] _cellsArray = new DefectsMapCell[0];

        /// <summary>
        ///     Горизонтальные метки карты
        /// </summary>
        private Label[] _hLabels = new Label[0];

        /// <summary>
        ///     Вертикальные метки карты
        /// </summary>
        private Label[] _vLabels = new Label[0];

        /// <summary>
        /// Количество строк
        /// </summary>
        public byte NRows { get; private set; }

        /// <summary>
        /// Количество столбцов
        /// </summary>
        public byte NColumns { get; private set; }

        public DefectsMap() : this(4, 4) { }

        public DefectsMap(byte rowCount, byte columnCount)
        {
            InitializeComponent();

            InitMap(rowCount, columnCount);

            //G.Defects.AddedDefect += UpdateWhenAddedDefect;
            //G.Defects.RemovedDefect += UpdateWhenRemovedDefect;
        }

        //private void UpdateWhenAddedDefect(object sender, AddedDefectEventArgs e)
        //{
        //    this.InvokeIfRequired(UpdateAllCells);
        //}


        //private void UpdateWhenRemovedDefect(object sender, RemovedDefectEventArgs e)
        //{
        //    this.InvokeIfRequired(UpdateAllCells);
        //}

        /// <summary>
        ///     Очищает ячейки
        /// </summary>
        private static void ClearCells(IEnumerable<DefectsMapCell> defectsMapCells)
        {
            foreach (var cell in defectsMapCells) cell.Defects.Clear();
        }

        /// <summary>
        ///     Обновляет ячейки (перерисовка)
        /// </summary>
        /// <param name="defectsMapCells"></param>
        private static void ReDrawCells(IEnumerable<DefectsMapCell> defectsMapCells)
        {
            foreach (var cell in defectsMapCells) cell.Refresh();
        }

        /// <summary>
        ///     Ищет подходящую для дефекта ячейку
        /// </summary>
        /// <param name="defect">Дефект</param>
        /// <param name="defectsMapCells">Массив ячеек, в котором ищем подходящую</param>
        /// <returns></returns>
        private static DefectsMapCell FindCellForDefect(Defect defect, IEnumerable<DefectsMapCell> defectsMapCells) => 
            defectsMapCells.FirstOrDefault(cell => cell.DefectsRectangle.Contains(defect.Center));

        /// <summary>
        ///     Обновляет все ячейки
        /// </summary>
        public void UpdateAllCells()
        {
            ClearCells(_cellsArray);

            for (var i = G.Defects.Count - 1; i >= 0; i--)
            {
                var defect = G.Defects[i];
                var cell = FindCellForDefect(defect, _cellsArray);

                //Если ячейка не будет найдена, то дефект удаляется
                if (cell != null)
                    cell.Defects.AddDefect(defect);
                else
                    G.Defects.RemoveAt(i);
            }

            //Refresh(); //Будет мигать сетка
            ReDrawCells(_cellsArray); //Возможен баг рисовки всех символов в ячейке
            Update();
        }

        /// <summary>
        ///     Обновляет позиции меток при смене размера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpDefectsMap_SizeChanged(object sender, EventArgs e) => ReLocationLabels();

        /// <summary>
        ///     Очищает все данные
        /// </summary>
        private void Clear()
        {
            //Удаляем ячейки карты
            tlpDefectsMap.Controls.Clear();

            //Очищаем массив ячеек
            _cellsArray = new DefectsMapCell[0];

            //Очищаем матрицу ячеек
            _cells = new DefectsMapCell[0, 0];

            //Удаляем старые вертикальные метки
            foreach (var vLabel in _vLabels) gbDefectsMap.Controls.Remove(vLabel);

            //Удаляем старые горизонтальные метки
            foreach (var hLabel in _hLabels) gbDefectsMap.Controls.Remove(hLabel);

            //Очищаем списки меток
            _vLabels = new Label[0];
            _hLabels = new Label[0];
        }

        public void InitMap(byte rowCount, byte columnCount)
        {
            NRows = rowCount;
            NColumns = columnCount;

            //Устанавливаем новую сетку
            tlpDefectsMap.RowCount = rowCount;
            tlpDefectsMap.ColumnCount = columnCount;

            //Очищаем имеющиеся стили
            tlpDefectsMap.RowStyles.Clear();
            tlpDefectsMap.ColumnStyles.Clear();

            var nRows = tlpDefectsMap.RowCount;
            var nColumns = tlpDefectsMap.ColumnCount;

            //Очищаем прошлые данные, если они имелись
            if (_vLabels.Length != 0 || _hLabels.Length != 0) Clear();

            //Инициализируем списки меток
            _vLabels = new Label[nRows + 1];
            _hLabels = new Label[nColumns + 1];

            //Устанавливаем стили строк
            for (var y = 0; y < nRows; y++) tlpDefectsMap.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / nRows));

            //Устанавливаем стили столбцов
            for (var x = 0; x < nColumns; x++)
                tlpDefectsMap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / nColumns));

            //Заполняем карту дефектов ячейками
            _cells = new DefectsMapCell[nColumns, nRows];
            for (var y = 0; y < nRows; y++)
            for (var x = 0; x < nColumns; x++)
            {
                var tCell = new DefectsMapCell {Dock = DockStyle.Fill, Padding = Padding.Empty, Margin = Padding.Empty};
                //tCell.BackColor = Color.Aqua;

                _cells[x, nRows - y - 1] = tCell;
                tlpDefectsMap.Controls.Add(_cells[x, nRows - y - 1], x, y);
            }

            _cellsArray = tlpDefectsMap.Controls.Cast<DefectsMapCell>().ToArray();

            //Необходимые родительские координаты для связи координат новых контролов
            var tplLoc = tlpDefectsMap.Location;
            var wLabel = tplLoc.X - Div;
            var gbLoc = gbDefectsMap.Location;

            Point loc;
            Label label;

            //Наносим вертикальные метки
            for (var y = 0; y < nRows; y++)
            {
                var cell = _cellsArray.FirstOrDefault(s => tlpDefectsMap.GetRow(s) == y);
                if (cell != null)
                {
                    loc = cell.Location;
                    label = new Label
                    {
                        Text = $@"{nRows - y}V",
                        Size = new Size(wLabel, HLabel),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(gbLoc.X + Div, tplLoc.Y + loc.Y - HLabel / 2)
                    };
                    _vLabels[nRows - y] = label;
                    gbDefectsMap.Controls.Add(label);
                }
            }

            //Наносим крайнюю вертикальную метку
            label = new Label
            {
                Text = @"0V",
                Size = new Size(wLabel, HLabel),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(gbLoc.X + Div, tlpDefectsMap.Bottom - HLabel)
            };
            _vLabels[0] = label;
            gbDefectsMap.Controls.Add(label);

            //Наносим горизонтальные метки
            for (var x = 0; x < nColumns; x++)
            {
                var cell = _cellsArray.FirstOrDefault(s => tlpDefectsMap.GetColumn(s) == x);
                if (cell != null)
                {
                    loc = cell.Location;
                    label = new Label
                    {
                        Text = $@"{x}H",
                        Size = new Size(wLabel, HLabel),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(tplLoc.X + loc.X - wLabel / 2, tlpDefectsMap.Bottom)
                    };
                    _hLabels[x] = label;
                    gbDefectsMap.Controls.Add(label);
                }
            }

            //Наносим крайнюю горизонтальную метку отдельно
            //Если бы выделить место под полноценную метку, то заменили бы
            label = new Label
            {
                Text = $@"{nColumns}H",
                Size = new Size(wLabel, HLabel),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(tlpDefectsMap.Right - wLabel, tlpDefectsMap.Bottom)
            };
            _hLabels[nColumns] = label;
            gbDefectsMap.Controls.Add(label);
        }

        /// <summary>
        ///     Обновляет позиции всех меток карты
        /// </summary>
        public void ReLocationLabels()
        {
            var nRows = tlpDefectsMap.RowCount;
            var nColumns = tlpDefectsMap.ColumnCount;

            //Необходимые родительские координаты для связи координат новых контролов
            var tplLoc = tlpDefectsMap.Location;
            var wLabel = tplLoc.X - Div;
            var gbLoc = gbDefectsMap.Location;

            Point loc;

            //Обновляем вертикальные метки
            for (var y = 0; y < nRows; y++)
            {
                var cell = _cellsArray.FirstOrDefault(s => tlpDefectsMap.GetRow(s) == y);
                if (cell != null)
                {
                    loc = cell.Location;
                    _vLabels[nRows - y].Location = new Point(gbLoc.X + Div, tplLoc.Y + loc.Y - HLabel / 2);
                }
            }

            //Меняем крайнюю вертикальную метку
            _vLabels[0].Location = new Point(gbLoc.X + Div, tlpDefectsMap.Bottom - HLabel);

            //Меняем горизонтальные метки
            for (var x = 0; x < nColumns; x++)
            {
                var cell = _cellsArray.FirstOrDefault(s => tlpDefectsMap.GetColumn(s) == x);
                if (cell != null)
                {
                    loc = cell.Location;
                    _hLabels[x].Location = new Point(tplLoc.X + loc.X - wLabel / 2, tlpDefectsMap.Bottom);
                }
            }

            //Меняем крайнюю горизонтальную метку
            _hLabels[nColumns].Location = new Point(tlpDefectsMap.Right - wLabel, tlpDefectsMap.Bottom);

            //Refresh();
        }

        /// <summary>
        ///     Изменяет текст меток
        /// </summary>
        /// <param name="vLabelTexts">Тексты вертикальных меток</param>
        /// <param name="hLabelTexts">Тексты горизонтальных меток</param>
        public void ChangeLabelTexts(string[] vLabelTexts, string[] hLabelTexts)
        {
            for (var i = 0; i < vLabelTexts.Length && i < _vLabels.Length; i++) _vLabels[i].Text = vLabelTexts[i];
            for (var i = 0; i < hLabelTexts.Length && i < _hLabels.Length; i++) _hLabels[i].Text = hLabelTexts[i];
        }

        /// <summary>
        ///     Изменяет размер карты
        /// </summary>
        /// <param name="nRows">Количество строк</param>
        /// <param name="nColumns">Количество столбцов</param>
        public void ChangeMapSize(byte nRows, byte nColumns) => InitMap(nRows, nColumns);


        /// <summary>
        ///     Устанавливает диапазоны карты дефектов (в пикселях)
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="width">Ширина в пикселях</param>
        /// <param name="d">Дальность в мм</param>
        public void SetMapRange(int xStart, int yStart, int width, float d)
        {
            var nColumns = _cells.GetLength(0);
            var nRows = _cells.GetLength(1);

            //Дальность в пикселях
            var range = UtilsD.GetGyFromGd(d);

            var widthCell = width / nColumns;
            var rangeCell = range / nRows;

            //Разбиваем диапазон карты на ячейки
            for (var y = 0; y < nRows; y++)
            for (var x = 0; x < nColumns; x++)
            {
                _cells[x, y].DefectsRectangle =
                    new Rectangle(xStart + widthCell * x, yStart + rangeCell * y, widthCell, rangeCell);
            }

            //Заполняем массив имен вертикальных меток
            var vLabelTexts = new string[nRows + 1];
            for (var i = 0; i <= nRows; i++)
            {
                var label = yStart + rangeCell * i;
                vLabelTexts[i] = G.Settings.LabelsInMm
                    ? KUtils.GetRoundValue(UtilsD.GetGdFromGy(label)).ToString()
                    : label.ToString();
            }

            //Заполняем массив имен горизонтальных меток
            var hLabelTexts = new string[nColumns + 1];
            for (var i = 0; i <= nColumns; i++)
            {
                var label = xStart + widthCell * i;
                hLabelTexts[i] = G.Settings.LabelsInMm
                    ? KUtils.GetRoundValue(UtilsD.GetGlFromGx(label)).ToString()
                    : label.ToString();
            }

            //Применяем метки
            ChangeLabelTexts(vLabelTexts, hLabelTexts);

            Refresh();
        }

        private void tlpDefectsMap_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //e.Graphics.FillRectangle((e.Column + e.Row) % 2 == 1 ? Brushes.Black : Brushes.White, e.CellBounds);
        }


        /// <summary>
        ///     Рисует маркерную линию
        /// </summary>
        private void DrawMarkerLine()
        {
            var k = 1 - G.Settings.MarkerLinePosition / G.Settings.MapRange;

            var pos = KUtils.GetClientValue(tlpDefectsMap.Height, k);

            _markerLine.LineHeight = G.Settings.MarkerLineWidth;
            _markerLine.LineColor = G.Settings.MarkerLineColor;
            _markerLine.Location = new Point(
                tlpDefectsMap.Left,
                pos + tlpDefectsMap.Top - G.Settings.MarkerLineWidth / 2
            );

            if (!_markerLine.Visible) _markerLine.Visible = true;
        }

        /// <summary>
        ///     Рисует маркерную линию
        /// </summary>
        private void TryDrawMarkerLine()
        {
            try { DrawMarkerLine(); }
            catch (Exception e) { G.Logger.Error(e.ToString()); }
        }

        /// <summary>
        /// Возвращает количество дефект в столбце карты.
        /// </summary>
        /// <param name="column">Индекс столбца</param>
        /// <returns></returns>
        public int GetCountInColumn(int column)
        {
            if (_cells == null)
                return 0;

            var nColumns = _cells.GetLength(0);
            if (column >= nColumns)
                throw new ArgumentException(@"Задан несуществующий столбец", $"{column}");

            var nRows = _cells.GetLength(1);

            var sum = 0;
            for (var i = 0; i < nRows; i++)
                sum += _cells[column, i].Defects.Count;

            return sum;
        }


        /// <summary>
        /// Допустимое состояние границ? (не много ли дефектов на границах?)
        /// </summary>
        /// <returns></returns>
        public bool IsValidBorders()
        {
            const int invalidCount = 10;

            try
            {
                var nColumns = _cells.GetLength(0);

                return GetCountInColumn(0) < invalidCount && GetCountInColumn(nColumns - 1) < invalidCount;
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());

                return true;
            }

        }

        /// <summary>
        /// Допустимое состояние левой границы? (не много ли дефектов на границе?)
        /// </summary>
        /// <returns></returns>
        public bool IsValidLeftBorder()
        {
            const int invalidCount = 10;

            try
            {
                return GetCountInColumn(0) < invalidCount;
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());

                return true;
            }

        }

        /// <summary>
        /// Допустимое состояние правой границы? (не много ли дефектов на границе?)
        /// </summary>
        /// <returns></returns>
        public bool IsValidRightBorder()
        {
            const int invalidCount = 10;

            try
            {
                var nColumns = _cells.GetLength(0);

                return GetCountInColumn(nColumns - 1) < invalidCount;
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());

                return true;
            }

        }

        private void tlpDefectsMap_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed; //Долой сглаживание
            g.CompositingQuality = CompositingQuality.HighSpeed; //Скорость отрисовки
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            //g.InterpolationMode = InterpolationMode.NearestNeighbor; //Интерполяция ближайшего соседа

            base.OnPaint(e); //Всё остальное от предка

            if (G.Settings.MarkerLine)
                TryDrawMarkerLine();
            else if (_markerLine.Visible) _markerLine.Visible = false;
        }
    }
}