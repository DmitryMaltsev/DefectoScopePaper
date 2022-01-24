#region

using System.IO;
using System.Reflection;
using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно "О программе"
    /// </summary>
    internal sealed partial class AboutProgramForm : Form
    {
        /// <summary>
        /// Создает окно "О программе"
        /// </summary>
        public AboutProgramForm()
        {
            InitializeComponent();

            Text = $@"О программе {AssemblyTitle}";
            lProductName.Text = AssemblyProduct;
            lVersion.Text = $@"Версия: {AssemblyVersion}";
            lVersionDll.Text = $@"Версия {SensorE.DriverDllName}: {VersionDll}";
            lCompanyName.Text = AssemblyCompany;
            tbDescription.Text = AssemblyDescription;
        }

        public string VersionDll => G.VersionDll;

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    if (titleAttribute.Title != "") return titleAttribute.Title;
                }

                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        //public string AssemblyCopyright
        //{
        //    get
        //    {
        //        var attributes = Assembly.GetExecutingAssembly()
        //            .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        //        if (attributes.Length == 0) return "";
        //        return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
        //    }
        //}

        public string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        #endregion

        private void bOk_Click(object sender, System.EventArgs e) => Close();

        private void AboutProgramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            G.Logger.Info($"{nameof(AboutProgramForm)}: Пользователь закрыл окно");
        }
    }
}