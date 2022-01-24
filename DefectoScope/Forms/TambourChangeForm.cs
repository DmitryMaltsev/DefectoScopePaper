using System;
using System.Windows.Forms;

namespace DefectoScope
{
    /// <summary>
    /// Окно смены тамбура
    /// </summary>
    public partial class TambourChangeForm : Form
    {
        #region Конструкторы

        /// <summary>
        /// Создает окно смены тамбура
        /// </summary>
        public TambourChangeForm()
        {
            InitializeComponent();

            nudNTambour.Value = G.SqlClient.NTambour;
            cbNShift.Checked = G.SqlClient.NShift == 1;
        }

        #endregion

        #region Свойства

        /// <summary>
        ///     Номер тамбура
        /// </summary>
        public int NTambour { get; private set; }

        /// <summary>
        ///     Номер смены
        /// </summary>
        public short NShift { get; private set; }

        #endregion

        #region События

        private void nudNTambour_ValueChanged(object sender, EventArgs e) => 
            NTambour = (int) nudNTambour.Value;

        private void cbNShift_CheckedChanged(object sender, EventArgs e) =>
            NShift = (short) (cbNShift.Checked ? 1 : 0);

        #endregion

        private void TambourChangeForm_FormClosing(object sender, FormClosingEventArgs e) => 
            G.Logger.Info($"{nameof(TambourChangeForm)}: Пользователь закрыл окно");

        private void BOK_Click(object sender, EventArgs e) => 
            G.Logger.Info($"{nameof(TambourChangeForm)}: Пользователь нажал на кнопку создания");

        private void BCancel_Click(object sender, EventArgs e) => 
            G.Logger.Info($"{nameof(TambourChangeForm)}: Пользователь нажал на кнопку отмены");
    }
}