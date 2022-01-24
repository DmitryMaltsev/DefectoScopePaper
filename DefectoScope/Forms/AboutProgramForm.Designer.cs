namespace DefectoScope
{
    sealed partial class AboutProgramForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutProgramForm));
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lProductName = new System.Windows.Forms.Label();
            this.lVersion = new System.Windows.Forms.Label();
            this.lVersionDll = new System.Windows.Forms.Label();
            this.lCompanyName = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.tlp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tlp.Controls.Add(this.pbLogo, 0, 0);
            this.tlp.Controls.Add(this.lProductName, 1, 0);
            this.tlp.Controls.Add(this.lVersion, 1, 1);
            this.tlp.Controls.Add(this.lVersionDll, 1, 2);
            this.tlp.Controls.Add(this.lCompanyName, 1, 3);
            this.tlp.Controls.Add(this.tbDescription, 1, 4);
            this.tlp.Controls.Add(this.bOk, 1, 5);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(9, 9);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 6;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp.Size = new System.Drawing.Size(417, 265);
            this.tlp.TabIndex = 0;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Image = global::DefectoScope.Properties.Resources.Gear_2s_200px;
            this.pbLogo.Location = new System.Drawing.Point(3, 3);
            this.pbLogo.Name = "pbLogo";
            this.tlp.SetRowSpan(this.pbLogo, 6);
            this.pbLogo.Size = new System.Drawing.Size(131, 259);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 12;
            this.pbLogo.TabStop = false;
            // 
            // lProductName
            // 
            this.lProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lProductName.Location = new System.Drawing.Point(143, 0);
            this.lProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.lProductName.Name = "lProductName";
            this.lProductName.Size = new System.Drawing.Size(271, 17);
            this.lProductName.TabIndex = 19;
            this.lProductName.Text = "Название продукта";
            this.lProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lVersion
            // 
            this.lVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lVersion.Location = new System.Drawing.Point(143, 26);
            this.lVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(271, 17);
            this.lVersion.TabIndex = 0;
            this.lVersion.Text = "Версия";
            this.lVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lVersionDll
            // 
            this.lVersionDll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lVersionDll.Location = new System.Drawing.Point(143, 52);
            this.lVersionDll.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lVersionDll.MaximumSize = new System.Drawing.Size(0, 17);
            this.lVersionDll.Name = "lVersionDll";
            this.lVersionDll.Size = new System.Drawing.Size(271, 17);
            this.lVersionDll.TabIndex = 21;
            this.lVersionDll.Text = "Версия dll";
            this.lVersionDll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lCompanyName
            // 
            this.lCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lCompanyName.Location = new System.Drawing.Point(143, 78);
            this.lCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.lCompanyName.Name = "lCompanyName";
            this.lCompanyName.Size = new System.Drawing.Size(271, 17);
            this.lCompanyName.TabIndex = 22;
            this.lCompanyName.Text = "Название организации";
            this.lCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDescription
            // 
            this.tbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDescription.Location = new System.Drawing.Point(143, 107);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDescription.Size = new System.Drawing.Size(271, 126);
            this.tbDescription.TabIndex = 23;
            this.tbDescription.TabStop = false;
            this.tbDescription.Text = "Описание";
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bOk.Location = new System.Drawing.Point(339, 239);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 24;
            this.bOk.Text = "&ОК";
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // AboutProgramForm
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.tlp);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutProgramForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutProgramForm_FormClosing);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lProductName;
        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.Label lVersionDll;
        private System.Windows.Forms.Label lCompanyName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button bOk;
    }
}
