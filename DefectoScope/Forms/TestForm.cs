#region

using System;
using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Тестовое окно
    /// </summary>
    public partial class TestForm : Form
    {
        /// <summary>
        /// Создает тестовое окно
        /// </summary>
        public TestForm() => InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            defectsMap1.ChangeLabelTexts(new[] {"123"}, new[] {"123"});
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var r = new Random();

            defectsMap1.ChangeMapSize((byte) r.Next(1, 10), (byte) r.Next(1, 10));

            // defectsMap1.ChangeMapSize(4, 4);
            //defectsMap1.ChangeLabelTexts(new[] { "1в", "2в"}, new[] { "1г", "2г" });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            defectsMap1.SetMapRange(0, 300, 2000, 10000);
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            tFillInput.Enabled = true;
        }

        private void tFillInput_Tick(object sender, EventArgs e)
        {
            if (!bwFillInput.IsBusy)
                bwFillInput.RunWorkerAsync();
        }

        private void bwFillInput_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            UtilsD.TempFillInputBuffer2(5000);
        }
    }
}