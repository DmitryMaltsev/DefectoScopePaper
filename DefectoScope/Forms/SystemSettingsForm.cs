#region

using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно настроек программы
    /// </summary>
    public partial class SystemSettingsForm : Form
    {
        /// <summary>
        /// Главное окно, для применения изменений
        /// </summary>
        private readonly MainForm _mainForm;

        /// <summary>
        /// Контрол системных настроек
        /// </summary>
        private readonly SystemSettingsControl _systemSettingsControl;

        /// <summary>
        /// Создает окно настроек программы
        /// </summary>
        /// <param name="mainForm"></param>
        public SystemSettingsForm(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();

            _systemSettingsControl = new SystemSettingsControl();
            Controls.Add(_systemSettingsControl);
        }

        private void SystemSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_systemSettingsControl.Changed || _systemSettingsControl.SensorsChanged)
            {
                var result = MessageBox.Show(@"Вы действительно желаете закрыть окно настроек? Не сохраненные изменения будут потеряны.",
                    @"Закрыть настройки?",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

                //Если пользователь не хочет закрывать окно, то отменяем закрытие
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }


                Enabled = false;
                var loadForm = new LoadForm();
                loadForm.Show();

                //Запускаем зависающую инициализацию в другом потоке с ожиданием
                var bwInitialization = new BackgroundWorker();
                bwInitialization.DoWork += bwInitialization_DoWork;
                bwInitialization.RunWorkerCompleted += bwInitialization_RunWorkerCompleted;
                bwInitialization.RunWorkerAsync(_systemSettingsControl.SensorsChanged);
                while (bwInitialization.IsBusy) Application.DoEvents();
                bwInitialization.Dispose();

                loadForm.Close();

                Enabled = true;

                G.IsAuto = true;
                _mainForm.bStop_Click(null, null);
                G.IsAuto = false;
            }
        }

        #region BW

        private void bwInitialization_DoWork(object sender, DoWorkEventArgs e) =>
            _mainForm.ReInit((bool) e.Argument);

        private void bwInitialization_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) throw e.Error;
        }

        #endregion

        private void SystemSettingsForm_FormClosed(object sender, FormClosedEventArgs e) => 
            G.Logger.Info($"{nameof(SystemSettingsForm)}: Пользователь закрыл окно");
    }
}