using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Kogerent;

namespace DefectoScope
{
    /// <summary>
    /// Окно загрузки кадров
    /// </summary>
    public partial class LoadFrameForm : Form
    {
        /// <summary>
        /// Ручной режим загрузки кадров
        /// </summary>
        public ManualMode LoadFrameMode;

        /// <summary>
        /// Количество необходимых загрузок (для прерывистого режима)
        /// </summary>
        public ushort NTimes;

        /// <summary>
        /// Количество загрузок (в текущем запуске)
        /// </summary>
        private ushort _countStrobes;

        /// <summary>
        /// Необходимо остановить загрузку?
        /// </summary>
        private bool _needStop;

        /// <summary>
        /// Периодичность загрузки (мс)
        /// </summary>
        public int Period;

        /// <summary>
        /// Строки загруженного кадра
        /// </summary>
        private byte[][] _rows = new byte[0][];

        /// <summary>
        ///     Диалоговое окно для кадров
        /// </summary>
        private OpenFileDialog _ofd;

        /// <summary>
        /// Создает окно загрузки кадров
        /// </summary>
        public LoadFrameForm()
        {
            InitializeComponent();

            _ofd = new OpenFileDialog
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = @"Image Files(*.BMP)|*.BMP",
                ShowReadOnly = true,
                RestoreDirectory = true,
                InitialDirectory = Application.StartupPath
            };
        }

        private void bApplyChanges_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(LoadFrameForm)}: Пользователь нажал на кнопку сохранения изменений");

            bStartLoad.Enabled = true;

            if (rbRepeatedly.Checked)
                LoadFrameMode = ManualMode.Repeatedly;
            if (rbConstantly.Checked)
                LoadFrameMode = ManualMode.Constantly;

            NTimes = (ushort)nudNTimes.Value;
            Period = (int)nudPeriod.Value;
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(LoadFrameForm)}: Пользователь нажал на кнопку загрузки");

            if (_ofd.ShowDialog() != DialogResult.OK) return;

            ToggleControls(false);

            var bwLoader = new BackgroundWorker();
            bwLoader.DoWork += bwLoader_DoWork;
            bwLoader.RunWorkerCompleted += bwLoader_RunWorkerCompleted;
            bwLoader.RunWorkerAsync(_ofd.FileName);
            while (bwLoader.IsBusy) Application.DoEvents();
            bwLoader.Dispose();

            ToggleControls(true);
        }

        private void bwLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) throw e.Error;
        }

        private void bwLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            //Считываем открываемое изображение
            var bitmap = new Bitmap(e.Argument.ToString());
            _rows = bitmap.ConvertBitmapToArrayByteArray(Color.Black, Color.White);
        }

        private void bStartLoad_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(LoadFrameForm)}: Пользователь нажал на кнопку старта загрузки");

            ToggleControls(false);

            _countStrobes = 0;
            _needStop = false;

            bwLoad.RunWorkerAsync();
        }

        private void bwLoad_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            switch (LoadFrameMode)
            {
                case ManualMode.Repeatedly:

                    while (!_needStop)
                    {
                        foreach (var r in _rows)
                        {
                            G.InputBuffer.Write(r);
                            //Thread.Sleep(1);
                        }

                        //UtilsD.TempFillInputBuffer(5000);

                        if (++_countStrobes >= NTimes)
                            _needStop = true;
                    }

                    break;
                case ManualMode.Constantly:

                    while (!_needStop)
                    {
                        //UtilsD.TempFillInputBuffer3(10);
                        foreach (var b in _rows)
                        {
                            G.InputBuffer.Write(b);
                            //Thread.Sleep(1);
                        }
                        Thread.Sleep(Period);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.InvokeIfRequired(() => ToggleControls(true));
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(LoadFrameForm)}: Пользователь нажал на кнопку останова");

            _needStop = true;
        }

        /// <summary>
        /// Переключает положение контролов?
        /// </summary>
        /// <param name="key">Включить контролы?</param>
        private void ToggleControls(bool key)
        {
            gbModeLoad.Enabled = key;
            bStartLoad.Enabled = key;
            bLoad.Enabled = key;
        }

        private void LoadFrameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _needStop = true;

            G.Logger.Info($"{nameof(LoadFrameForm)}: Пользователь закрыл окно");
        }
    }
}
