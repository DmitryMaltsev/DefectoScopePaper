using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DefectoScope
{
    public partial class PasswordForm : Form
    {
        private string _user;

        public PasswordForm()
        {
            InitializeComponent();
        }
        private readonly Dictionary<string, string> _users = new Dictionary<string, string>
        {
            {"ТБФ", "12345678"},
            {"КОГЕРЕНТ", "kogerent2012"}
        };

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            //Utilities.WriteToLog("Вошли в tbPassword_TC");

            _user = _users.Where(x => x.Value == tbPassword.Text).Select(p => p.Key).FirstOrDefault();

            if (_user == null)
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

        private void Password_Load(object sender, EventArgs e) => G.Logger.Info($"{nameof(PasswordForm)}: Произведен запрос инженерного доступа");

        private void bOK_Click(object sender, EventArgs e) => G.Logger.Info($"{nameof(PasswordForm)}: Произведен инженерный доступ по паролю пользователя " + _user);

        private void Password_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Utilities.WriteToLog("Вошли в Password_FC");

            if (e.CloseReason == CloseReason.UserClosing)
                G.Logger.Info($"{nameof(PasswordForm)}: Запрос инженерного доступа был отменен");
        }

        private void bCancel_Click(object sender, EventArgs e) => G.Logger.Info($"{nameof(PasswordForm)}: Запрос инженерного доступа был отменен");
    }
}