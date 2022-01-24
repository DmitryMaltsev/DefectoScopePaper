#region

using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно авто-смены
    /// </summary>
    public partial class AutoShiftSettingsForm : Form
    {
        ///// <summary>
        ///// Контрол авто-смены
        ///// </summary>
        //private readonly AutoShiftSettingsControl _autoShiftSettingsControl;

        /// <summary>
        /// Создает окно авто-смены
        /// </summary>
        public AutoShiftSettingsForm()
        {
            InitializeComponent();

            //_autoShiftSettingsControl = new AutoShiftSettingsControl {SystemSettings = G.Settings};
            //Controls.Add(_autoShiftSettingsControl);
        }

        private void AutoShiftSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (autoShiftSettingsControl1.Changed)
            {
                var result = MessageBox.Show(@"Вы действительно желаете закрыть окно настройки авто-смен? Не сохраненные изменения будут потеряны.",
                    @"Закрыть настройки авто-смен?",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);

                //Если пользователь не хочет закрывать окно, то отменяем закрытие
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                UtilsSettings.LoadConfigSettings();
                UtilsSettings.LoadAutoShiftSettings();
            }
        }

        private void AutoShiftSettingsForm_FormClosed(object sender, FormClosedEventArgs e) =>
            G.Logger.Info($"{nameof(AutoShiftSettingsForm)}: Пользователь закрыл окно");
    }
}