#region

using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно статистики
    /// </summary>
    public partial class StatisticForm : Form
    {
        /// <summary>
        /// Таймер обновления
        /// </summary>
        private readonly Timer _tUpdater;

        /// <summary>
        /// Создает окно статистики
        /// </summary>
        public StatisticForm()
        {
            InitializeComponent();
            _tUpdater = new Timer(1000) { SynchronizingObject = this };
            _tUpdater.Elapsed += tUpdater_Tick;
        }

        private void StatisticForm_Load(object sender, EventArgs e)
        {
            //Добавляем по столбцу на каждый датчик
            foreach (var sensor in G.Sensors)
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn { HeaderText = $@"Датчик №{sensor.NDevice}" };
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns.Add(column);
            }

            //Добавляем строки статистики
            dgv.Rows.Add(16);
            dgv.Rows[0].HeaderCell.Value = "Реальная частота профилей (Гц):";
            //dgv.Rows[1].HeaderCell.Value = "Количество пропущенных программой профилей:";
            //dgv.Rows[2].HeaderCell.Value = "Количество фактов пропущенных профилей датчиком:";
            dgv.Rows[1].HeaderCell.Value = "Номер кадра/профиля:";
            dgv.Rows[2].HeaderCell.Value = "Состояние дискретных входов:";
            dgv.Rows[3].HeaderCell.Value = "Количество точек в кадре/профиле:";
            dgv.Rows[4].HeaderCell.Value = "Номер строба:";
            dgv.Rows[5].HeaderCell.Value = "Количество фактов переполнения входного буфера dll:";
            dgv.Rows[6].HeaderCell.Value = "Количество фактов неполучения данных по таймауту:";
            dgv.Rows[7].HeaderCell.Value = "Количество фактов неполных кадров/профилей по dll:";
            dgv.Rows[8].HeaderCell.Value = "Количество фактов пропущенных стробов:";

            dgv.Rows[9].HeaderCell.Value = "Заполнение входного буфера:";

            dgv.Rows[10].HeaderCell.Value = "Количество прошедших профилей в тамбуре:";
            dgv.Rows[11].HeaderCell.Value = "Количество обращений к OPC серверу:";
            dgv.Rows[12].HeaderCell.Value = "break / break2 / smena_tambura:";
            dgv.Rows[13].HeaderCell.Value = "Плотность бумаги, г/м2:";
            _tUpdater.Enabled = true;
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
                    for (var i = 0; i < G.Sensors.Length; i++)
                    {
                        var s = G.Sensors[i];

                        dgv.Rows[0].Cells[i].Value = s.RealFrequencyProfiles.ToString("F");
                        //dgv.Rows[1].Cells[i].Value = sensor.NProgramSkippedProfiles;
                        //dgv.Rows[2].Cells[i].Value = sensor.NSensorSkippedProfiles;
                        dgv.Rows[1].Cells[i].Value = s.NFrame;
                        dgv.Rows[2].Cells[i].Value = s.StatusDiscreteInputs;
                        dgv.Rows[3].Cells[i].Value = s.Settings.WidthWindow;
                        dgv.Rows[4].Cells[i].Value = s.NStrobe;
                        dgv.Rows[5].Cells[i].Value = s.NOverflowsBuffer;
                        dgv.Rows[6].Cells[i].Value = s.NTimeouts;
                        dgv.Rows[7].Cells[i].Value = s.NBreakFrames;
                        dgv.Rows[8].Cells[i].Value = s.NSkipStrobes;
                    }

                    dgv.Rows[9].Cells[0].Value = $"{G.InputBuffer.Length}/{G.InputBuffer.Capacity}";
                    dgv.Rows[10].Cells[0].Value = $"{G.NProfiles}";
                    dgv.Rows[11].Cells[0].Value = $"{G.OpcClient.NRequests}";
                    dgv.Rows[12].Cells[0].Value = $"{(G.OpcClient.Break1 ? "1" : "0")} / {(G.OpcClient.Break2 ? "1" : "0")} / {(G.OpcClient.TambourChange ? "1" : "0")}";
                    dgv.Rows[13].Cells[0].Value = $"{G.OpcClient.Weight}";

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

        private void StatisticForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tUpdater.Enabled = false;
            _tUpdater.Elapsed -= tUpdater_Tick;
            Enabled = false;
            Thread.Sleep(500);

            G.Logger.Info($"{nameof(StatisticForm)}: Пользователь закрыл окно");
        }
    }
}