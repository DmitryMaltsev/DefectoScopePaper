using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DefectoScope
{
    /// <summary>
    /// Окно создания отчета
    /// </summary>
    public partial class CreateReportForm : Form
    {
        /// <summary>
        /// Начало диапазона
        /// </summary>
        public DateTime RangeBegin { get; private set; }

        /// <summary>
        /// Конец диапазона
        /// </summary>
        public DateTime RangeEnd { get; private set; }

        /// <summary>
        /// С тамбуром?
        /// </summary>
        public bool WithTambour { get; private set; }

        /// <summary>
        /// Номер тамбура
        /// </summary>
        public int NTambour { get; private set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Создает окно создания отчетов
        /// </summary>
        public CreateReportForm()
        {
            InitializeComponent();

            dtpRangeBegin.Value = DateTime.Now.Date;
            dtpRangeEnd.Value = DateTime.Now;
            nudNTambour.Value = G.SqlClient.NTambour;
            tbFileName.Text = $@"Отчет - {DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
        }

        //private void CreateReportForm_Load(object sender, EventArgs e)
        //{
        //    var loadForm = new LoadForm();
        //    loadForm.Show();
        //    bw.RunWorkerAsync(ClientMode.Reading);
        //    while (bw.IsBusy) Application.DoEvents();
        //    loadForm.Close();

        //    Activate();
        //}

        private void CreateReportForm_Shown(object sender, EventArgs e)
        {
            if (!G.SqlClient.IsOk)
            {
                MessageBox.Show(@"Не удалось установить связь с базой данных! Создание отчета невозможно!");
                Close();
            }
        }

        private void CreateReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Enabled = false;

            //if (!G.Settings.WriteInDatabase && G.SqlClient.Mode == ClientMode.Reading) return;

            //var loadForm = new LoadForm();
            //loadForm.Show();
            //bw.RunWorkerAsync(ClientMode.Writing);
            //while (bw.IsBusy) Application.DoEvents();
            //loadForm.Close();

            G.Logger.Info($"{nameof(CreateReportForm)}: Пользователь закрыл окно");
        }

        //private void tbFileName_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Path.GetInvalidFileNameChars().Contains(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        //}

        private void bCreateReport_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(CreateReportForm)}: Пользователь нажал на кнопку создания отчета");

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

            var success = G.SqlClient.CreateReport(
                RangeBegin,
                RangeEnd,
                $"{FileName}.xlsx",
                WithTambour,
                NTambour
            );

            if (success)
            {
                MessageBox.Show(
                    $@"Отчет с именем {tbFileName.Text}.xlsx успешно создан!",
                    @"Результат создания отчета",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    @"Не удалось создать отчет!",
                    @"Результат создания отчета",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //private void bw_DoWork(object sender, DoWorkEventArgs e) => 
        //    G.SqlClient.Mode = (ClientMode)e.Argument;

        //private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (e.Error != null) throw e.Error;
        //}

        private void DtpRangeBegin_ValueChanged(object sender, EventArgs e) => 
            RangeBegin = ((DateTimePicker)sender).Value;

        private void DtpRangeEnd_ValueChanged(object sender, EventArgs e) => 
            RangeEnd = ((DateTimePicker)sender).Value;

        private void NudNTambour_ValueChanged(object sender, EventArgs e) => 
            NTambour = (int) ((NumericUpDown)sender).Value;

        private void TbFileName_TextChanged(object sender, EventArgs e) => 
            FileName = ((TextBox)sender).Text;

        private void CbIncludeTambour_CheckedChanged(object sender, EventArgs e)
        {
            WithTambour = ((CheckBox) sender).Checked;
            nudNTambour.Enabled = WithTambour;
        }
    }
}
