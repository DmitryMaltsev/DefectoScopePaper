#region

using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно времени
    /// </summary>
    public partial class TimeForm : Form
    {
        /// <summary>
        /// Таймер обновления
        /// </summary>
        private readonly Timer _tUpdater;

        /// <summary>
        /// Создает окно времени
        /// </summary>
        public TimeForm()
        {
            InitializeComponent();
            _tUpdater = new Timer(1000) { SynchronizingObject = this };
            _tUpdater.Elapsed += tUpdater_Tick;
        }

        private void TimeForm_Load(object sender, EventArgs e)
        {
            //Добавляем столбец
            DataGridViewColumn column = new DataGridViewTextBoxColumn {HeaderText = @"Время (мс)"};
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns.Add(column);

            //Добавляем строки
            dgv.Rows.Add(6);
            dgv.Rows[0].HeaderCell.Value = "Получение строк для датчиков:";
            dgv.Rows[1].HeaderCell.Value = "Объединение строк в профиль:";
            dgv.Rows[2].HeaderCell.Value = "Добавление профиля во входной буфер:";
            dgv.Rows[3].HeaderCell.Value = "Отрисовка буфера:";
            dgv.Rows[4].HeaderCell.Value = "Весь цикл:";

            _tUpdater.Enabled = true;
            Time.IsNeed = true;
        }

        private void tUpdater_Tick(object sender, EventArgs e)
        {
            //_tUpdater.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tUpdater.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                    dgv.Rows[0].Cells[0].Value = Time.GetRows.ToString("0.####");
                    dgv.Rows[1].Cells[0].Value = Time.ConcatAllRows.ToString("0.####");
                    dgv.Rows[2].Cells[0].Value = Time.AddInputBuffer.ToString("0.####");
                    dgv.Rows[3].Cells[0].Value = Time.AddRowsBufferForm.ToString("0.####");
                    dgv.Rows[4].Cells[0].Value = Time.Cycle.ToString("0.####");

                    dgv.Update();
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tUpdater.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }


        private void TimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Time.IsNeed = false;

            _tUpdater.Enabled = false;
            _tUpdater.Elapsed -= tUpdater_Tick;
            Enabled = false;
            Thread.Sleep(500);

            G.Logger.Info($"{nameof(TimeForm)}: Пользователь закрыл окно");
        }
    }
}