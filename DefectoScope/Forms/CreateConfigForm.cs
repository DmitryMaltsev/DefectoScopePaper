#region

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    public sealed partial class CreateConfigForm : Form
    {
        #region Поля

        /// <summary>
        ///     Существующие Id
        /// </summary>
        private readonly string[] _existIds = new string[0];

        #endregion

        #region События контролов

        private void tbId_TextChanged(object sender, EventArgs e)
        {
            if (_existIds.Contains(tbId.Text))
            {
                bOK.Enabled = false;
                bOK.BackColor = Color.Red;
            }
            else
            {
                bOK.Enabled = true;
                bOK.ResetBackColor();
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        ///     Создает окно создания конфигурации
        /// </summary>
        /// <param name="existIds">Существующие идентификаторы (запрещенные)</param>
        /// <param name="text">Текущий текст</param>
        public CreateConfigForm(string[] existIds = null, string text = null)
        {
            InitializeComponent();

            if (existIds != null)
                _existIds = existIds;

            tbId.Text = text;
        }

        public CreateConfigForm() : this(null) { }

        #endregion

        #region Методы

        /// <summary>
        /// Получает Id новой конфигурации (если неудачно, то вернет null)
        /// </summary>
        /// <returns></returns>
        public string GetId() => ShowDialog() == DialogResult.OK ? tbId.Text : null;

        #endregion

        private void CreateConfigForm_FormClosing(object sender, FormClosingEventArgs e) => 
            G.Logger.Info($"{nameof(CreateConfigForm)}: Пользователь закрыл окно");

        private void BOK_Click(object sender, EventArgs e) => 
            G.Logger.Info($"{nameof(CreateConfigForm)}: Пользователь нажал на кнопку создания");

        private void BCancel_Click(object sender, EventArgs e) => 
            G.Logger.Info($"{nameof(CreateConfigForm)}: Пользователь нажал на кнопку отмены");
    }
}