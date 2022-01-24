using System;
using System.Windows.Forms;

namespace DefectoScope
{
    public partial class TestAutoFillSettingsForm : Form
    {
        /// <summary>
        ///     Диалоговое окно для файла настроек
        /// </summary>
        private OpenFileDialog _ofd;

        /// <summary>
        /// Массив необходимых ID
        /// </summary>
        private string[] _ids;

        /// <summary>
        /// Разделения на датчики
        /// </summary>
        private string[] _endings;

        public TestAutoFillSettingsForm()
        {
            InitializeComponent();

            _ofd = new OpenFileDialog
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = @"Image Files(*.xml)|*.xml",
                ShowReadOnly = true,
                RestoreDirectory = true,
                InitialDirectory = Application.StartupPath
            };

            _ids = new[]
            {
                "30-35 грамм",
                "35-40 грамм",
                "40-45 грамм",
                "45-50 грамм",
                "50-55 грамм",
                "55-60 грамм",
                "60-65 грамм",
                "65-70 грамм",
                "70-75 грамм",
                "75-80 грамм",
                "80-85 грамм",
                "85-90 грамм",
                "90-95 грамм",
                "95-100 грамм",

                "30-35 грамм, цветной",
                "35-40 грамм, цветной",
                "40-45 грамм, цветной",
                "45-50 грамм, цветной",
                "50-55 грамм, цветной",
                "55-60 грамм, цветной",
                "60-65 грамм, цветной",
                "65-70 грамм, цветной",
                "70-75 грамм, цветной",
                "75-80 грамм, цветной",
                "80-85 грамм, цветной",
                "85-90 грамм, цветной",
                "90-95 грамм, цветной",
                "95-100 грамм, цветной",
            };

            _endings = new []
            {
                "(.10)",
                "(.15)",
                "(.9)"
            };
        }

        private void BOpen_Click(object sender, EventArgs e)
        {
            if (_ofd.ShowDialog() != DialogResult.OK) return;

            var s = new SensorSettings[_endings.Length];

            //Создаем базовые настройки
            for (var i = 0; i < _endings.Length; i++)
            {
                s[i] = new SensorSettings {PathToDoc = _ofd.FileName};
                s[i].LoadSettingsFromFile($"default {_endings[i]}");
            }

            foreach (var id in _ids)
            {
                for (var i = 0; i < _endings.Length; i++)
                {
                    s[i].Id = $"{id} {_endings[i]}";
                    s[i].SaveSettingsInFile();
                }
            }

            MessageBox.Show(@"Готово");
        }
    }
}
