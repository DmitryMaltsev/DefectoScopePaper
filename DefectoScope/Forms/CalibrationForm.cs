#region

using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Kogerent;
using Timer = System.Timers.Timer;
#endregion

namespace DefectoScope
{
    /// <summary>
    ///     Окно калибровки
    /// </summary>
    public partial class CalibrationForm : Form
    {
        /// <summary>
        ///     Профили для усреднения
        /// </summary>
        private readonly List<byte[]> _profiles;

        /// <summary>
        /// Текущий профиль
        /// </summary>
        private byte[] _currentProfile;

        /// <summary>
        ///     Таймер по обновлению метки
        /// </summary>
        private readonly Timer _tLabelUpdater;

        /// <summary>
        ///     Таймер по обновлению профиля
        /// </summary>
        private readonly Timer _tProfileUpdater;

        /// <summary>
        ///     Профили нужны?
        /// </summary>
        private bool _isNeedProfiles;

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        ///     Создает окно калибровки
        /// </summary>
        public CalibrationForm()
        {
            InitializeComponent();

            tbFileName.Text = Calibration.GetCurrentCalibrationFileName();
            
            _profiles = new List<byte[]>(G.Settings.NCalibrationProfiles);
            _currentProfile = new byte[0];

            _tLabelUpdater = new Timer(G.UpdatePeriodMs) {SynchronizingObject = this};
            _tLabelUpdater.Elapsed += LabelUpdaterTick;

            _tProfileUpdater = new Timer(G.UpdatePeriodMs) { SynchronizingObject = this };
            _tProfileUpdater.Elapsed += ProfileUpdaterTick;

            dynamicSensorControl1.Sensors = G.Sensors;
        }

        private void CalibrationForm_Load(object sender, EventArgs e)
        {
            if (G.Settings.DebugMode) Text += $@" (Поток №{Thread.CurrentThread.ManagedThreadId})";

            UtilsD.InitProfileChart(cProfile);
            Calibration.ThresholdChanged += ThresholdChanged;

            _tLabelUpdater.Enabled = true;
            _tProfileUpdater.Enabled = true;

            //G.ProfileBuffer.AddedProfile += ShowLastProfile;
            G.ProfileBuffer.AddedProfile += ChangeCurrentProfile;

            G.IsCalibration = true;
        }

        private void ChangeCurrentProfile(object sender, AddedProfileEventArgs e)
        {
            _currentProfile = e.Profile;

            if (_isNeedProfiles)
            {
                _profiles.Add(e.Profile);

                //Если достигли необходимого количества профилей, то
                if (_profiles.Count == G.Settings.NCalibrationProfiles) this.InvokeIfRequired(() => ToggleControls(true));
            }
        }

        private void ThresholdChanged(object sender, EventArgs e) => UtilsD.InitProfileChart(cProfile);

        private void CalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tLabelUpdater.Elapsed -= LabelUpdaterTick;
            _tProfileUpdater.Elapsed -= ProfileUpdaterTick;

            _tLabelUpdater.Enabled = false;
            _tProfileUpdater.Enabled = false;

            //G.ProfileBuffer.AddedProfile -= ShowLastProfile;
            G.ProfileBuffer.AddedProfile -= ChangeCurrentProfile;
            Calibration.ThresholdChanged -= ThresholdChanged;

            Enabled = false;
            Thread.Sleep(500);

            G.IsCalibration = false;
            G.Logger.Info($"{nameof(CalibrationForm)}: Пользователь закрыл окно");
        }


        /// <summary>
        ///     Показывает профиль
        /// </summary>
        /// <param name="profile"></param>
        private void ShowProfile(IReadOnlyList<byte> profile)
        {
            var length = profile.Count;
            var step = length / cProfile.Width * 5;

            this.TryInvokeIfRequired(
                () =>
                {
                    cProfile.Series[3].Points.Clear();

                    for (var i = 0; i < length; i += step)
                    {
                        var x = G.Settings.LabelsInMm ? KUtils.GetRoundValue(UtilsD.GetGlFromGx(i)) : i;
                        cProfile.Series[3].Points.AddXY(x, profile[i]);
                    }

                    //cProfile.Series[3].Points.DataBindY(profile);
                }
            );

            Application.DoEvents();
        }

        private void ProfileUpdaterTick(object sender, ElapsedEventArgs e)
        {
            //_tProfileUpdater.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tProfileUpdater.Enabled = false;
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
                ShowProfile(_currentProfile);
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tProfileUpdater.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        ///// <summary>
        /////     Показывает последний профиль
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="addedProfileEventArgs"></param>
        //private async void ShowLastProfile(object sender, AddedProfileEventArgs addedProfileEventArgs) =>
        //    await Task.Run(() => ShowProfile(addedProfileEventArgs.Profile));
        //    //ShowLastProfile(addedProfileEventArgs.Profile);

        /// <summary>
        ///     Переключает положение контролов и флагов?
        /// </summary>
        /// <param name="key">Включить контролы?</param>
        private void ToggleControls(bool key)
        {
            bSave.Enabled = key;
            bStart.Enabled = key;
            bCancel.Enabled = !key;
            _isNeedProfiles = !key;
        }

        /// <summary>
        ///     Запускает процесс сохранения пришедших профилей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bStart_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(CalibrationForm)}: Пользователь нажал на кнопку старта");

            ToggleControls(false);
            _profiles.Clear();
        }

        private void LabelUpdaterTick(object sender, EventArgs e)
        {
            //_tLabelUpdater.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tLabelUpdater.Enabled = false;
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
                    lCountValue.Text = _profiles.Count.ToString();
                    lCountValue.Update();
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tLabelUpdater.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        /// <summary>
        ///     Очищает список сохраненных профилей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bReset_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(CalibrationForm)}: Пользователь нажал на кнопку сброса");

            _profiles.Clear();
        }

        /// <summary>
        ///     Отменяет процесс сохранения пришедших профилей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bCancel_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(CalibrationForm)}: Пользователь нажал на кнопку отмены");

            ToggleControls(true);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(CalibrationForm)}: Пользователь нажал на кнопку сохранения");

            //Если сохраненных профилей нет вовсе, то ничего не делаем
            if (_profiles.Count == 0)
            {
                MessageBox.Show(@"Количество собранных профилей равно нулю! Нечего сохранять.",
                    @"Пустые данные", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UtilsD.IsValidFileName(FileName, out var name))
            {
                if (FileName != name)
                {
                    var dialog = MessageBox.Show(
                        $@"Имя файла имеет недопустимые символы, оно будет преобразовано к имени: {name}",
                        @"Имя файла",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk
                    );

                    if (dialog != DialogResult.OK)
                        return;
                }

                FileName = name;
            }
            else
            {
                MessageBox.Show(
                    @"Имя файла недопустимо! Измените имя!",
                    @"Имя файла",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );
                return;
            }
            
            //Сохраняем порог в файл
            Calibration.SaveCalibration(UtilsD.GetMeanProfile(_profiles), FileName);

            //Применяем новую калибровку
            G.Settings.CalibrationFileName = FileName;
            G.Settings.SaveSettingsInFile();

            Calibration.LoadCalibration();

            G.ResetNProfiles();

            MessageBox.Show(@"Калибровка была успешно сохранена и применена к текущей конфигурации.");
        }


        //private void bOpen_Click(object sender, EventArgs e)
        //{
        //    var dialog = new OpenFileDialog
        //                 {
        //                     Title  = @"Загрузить калибровку",
        //                     Filter = @"Бинарный файл (*.bin)|*.bin"
        //                 };
        //    if (dialog.ShowDialog() == DialogResult.OK) Calibration.OpenCalibration(dialog.FileName);
        //}

        /// <summary>
        ///     Зум по вращению колесика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chData_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                var area = cProfile.ChartAreas[0];

                if (e.Delta < 0)
                {
                    area.AxisX.ScaleView.ZoomReset();
                    area.AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    var xMin = area.AxisX.ScaleView.ViewMinimum;
                    var xMax = area.AxisX.ScaleView.ViewMaximum;
                    var yMin = area.AxisY.ScaleView.ViewMinimum;
                    var yMax = area.AxisY.ScaleView.ViewMaximum;

                    var posXStart = area.AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2;
                    var posXFinish = area.AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2;
                    var posYStart = area.AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2;
                    var posYFinish = area.AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2;

                    area.AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    area.AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch
            {
                // ignored
            }
        }


        #region Смена размера окна

        private void CalibrationForm_ResizeBegin(object sender, EventArgs e) => KUtils.SuspendPainting(this);

        private void CalibrationForm_ResizeEnd(object sender, EventArgs e) => KUtils.ResumePainting(this);

        #endregion

        private void TbFileName_TextChanged(object sender, EventArgs e) =>
            FileName = ((TextBox)sender).Text;
    }
}