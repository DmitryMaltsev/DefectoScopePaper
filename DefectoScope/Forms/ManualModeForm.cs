using System;
using System.Threading;
using System.Windows.Forms;
using Kogerent;
using Timer = System.Timers.Timer;

namespace DefectoScope
{
    /// <summary>
    /// Окно ручной работы
    /// </summary>
    public partial class ManualModeForm : Form
    {
        /// <summary>
        /// Режим ручного стробирования
        /// </summary>
        public ManualMode StrobesManualMode;

        /// <summary>
        /// Количество стробов (для прерывистого режима)
        /// </summary>
        public ushort NStrobes;

        /// <summary>
        /// Подсчитано стробов (в текущем запуске)
        /// </summary>
        private ushort _countStrobes;

        /// <summary>
        /// Необходимо остановить посылку стробов?
        /// </summary>
        private bool _needStop;

        ///// <summary>
        ///// Периодичность стробов (мс)
        ///// </summary>
        //public int PeriodStrobes;

        /// <summary>
        /// Частота стробов (Гц)
        /// </summary>
        public int FreqStrobes;

        /// <summary>
        ///     Таймер по обновлению
        /// </summary>
        private readonly Timer _tUpdater;

        /// <summary>
        /// Создает окно ручной работы
        /// </summary>
        public ManualModeForm()
        {
            InitializeComponent();

            _tUpdater = new Timer(10);
            _tUpdater.Elapsed += tUpdater_Tick;
        }

        private void ManualModeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Enabled = false;

            foreach (var sensor in G.Sensors)
            {
                if (!sensor.SyncExt())
                {
                    MessageBox.Show(
                        $@"Не удалось установить режим внешней синхронизации для датчика с IP = {sensor.Settings.Ip}");

                    sensor.IsEnabled = false;
                    return;
                }
            }

            G.Logger.Info($"{nameof(ManualModeForm)}: Пользователь закрыл окно");
        }

        private void bApplyChanges_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ManualModeForm)}: Пользователь нажал на кнопку сохранения изменений");

            bStartManualMode.Enabled = true;

            if (rbRepeatedly.Checked)
                StrobesManualMode = ManualMode.Repeatedly;
            if (rbConstantly.Checked)
                StrobesManualMode = ManualMode.Constantly;

            NStrobes = (ushort)nudNStrobes.Value;
            FreqStrobes = (int)nudFreqStrobes.Value;
        }

        private void bStartManualMode_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ManualModeForm)}: Пользователь нажал на кнопку запуска ручного режима");

            foreach (var sensor in G.Sensors)
            {
                //SensorE.SetRegisterValue(sensor.Handle, 70, 255);
                //SensorE.SetRegisterValue(sensor.Handle, 71, 255);

                switch (StrobesManualMode)
                {
                    case ManualMode.Repeatedly:
                        if (!sensor.SyncCmd())
                        {
                            MessageBox.Show(
                                $@"Не удалось установить режим синхронизации по команде для датчика с IP = {sensor.Settings.Ip}");

                            sensor.IsEnabled = false;
                            return;
                        }

                        Thread.Sleep(1);

                        break;
                    case ManualMode.Constantly:
                        //var frameRate = KUtils.GetRoundValue(1000f / PeriodStrobes);
                        sensor.FrameRate = FreqStrobes;
                        if (!sensor.SyncNone())
                        {
                            MessageBox.Show(
                                $@"Не удалось установить непрерывный режим работы для датчика с IP = {sensor.Settings.Ip}");

                            sensor.IsEnabled = false;
                            return;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            ToggleControls(false);

            _countStrobes = 0;

            _needStop = false;
            bStop.Enabled = true;

            //_tUpdater.Interval = PeriodStrobes;
            _tUpdater.Enabled = true;
        }

        private void tUpdater_Tick(object sender, EventArgs e)
        {
            _tUpdater.Enabled = false;

            switch (StrobesManualMode)
            {
                case ManualMode.Repeatedly:
                    {
                        foreach (var sensor in G.Sensors)
                        {
                            if (!sensor.SendSync())
                            {
                                MessageBox.Show(
                                    $@"Не удалось отправить команду на выдачу для датчика с IP = {sensor.Settings.Ip}");

                                sensor.IsEnabled = false;
                            }
                        }

                        _countStrobes++;

                        if (_countStrobes >= NStrobes || _needStop)
                            this.InvokeIfRequired(() => ToggleControls(true));
                        else
                            _tUpdater.Enabled = true;

                        break;
                    }
                case ManualMode.Constantly:
                    if (_needStop)
                    {
                        foreach (var sensor in G.Sensors)
                        {
                            if (!sensor.SyncCmd())
                            {
                                MessageBox.Show(
                                    $@"Не удалось установить режим синхронизации по команде для датчика с IP = {sensor.Settings.Ip}");

                                sensor.IsEnabled = false;
                            }
                        }

                        this.InvokeIfRequired(() => ToggleControls(true));
                    }
                    else
                        _tUpdater.Enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ManualModeForm)}: Пользователь нажал на кнопку останова");

            _needStop = true;
            bStop.Enabled = false;
        }

        /// <summary>
        /// Переключает положение контролов?
        /// </summary>
        /// <param name="key">Включить контролы?</param>
        private void ToggleControls(bool key)
        {
            gbStrobes.Enabled = key;
            bStartManualMode.Enabled = key;
        }
    }
}
