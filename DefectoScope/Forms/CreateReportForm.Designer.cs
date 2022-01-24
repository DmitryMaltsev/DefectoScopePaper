namespace DefectoScope
{
    partial class CreateReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpRangeBegin = new System.Windows.Forms.DateTimePicker();
            this.dtpRangeEnd = new System.Windows.Forms.DateTimePicker();
            this.nudNTambour = new System.Windows.Forms.NumericUpDown();
            this.lCalibrationFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.lNTambour = new System.Windows.Forms.Label();
            this.lFileName = new System.Windows.Forms.Label();
            this.bCreateReport = new System.Windows.Forms.Button();
            this.cbIncludeTambour = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudNTambour)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpRangeBegin
            // 
            this.dtpRangeBegin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRangeBegin.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpRangeBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRangeBegin.Location = new System.Drawing.Point(178, 12);
            this.dtpRangeBegin.Name = "dtpRangeBegin";
            this.dtpRangeBegin.Size = new System.Drawing.Size(163, 20);
            this.dtpRangeBegin.TabIndex = 0;
            this.dtpRangeBegin.ValueChanged += new System.EventHandler(this.DtpRangeBegin_ValueChanged);
            // 
            // dtpRangeEnd
            // 
            this.dtpRangeEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRangeEnd.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpRangeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRangeEnd.Location = new System.Drawing.Point(178, 38);
            this.dtpRangeEnd.Name = "dtpRangeEnd";
            this.dtpRangeEnd.Size = new System.Drawing.Size(163, 20);
            this.dtpRangeEnd.TabIndex = 0;
            this.dtpRangeEnd.ValueChanged += new System.EventHandler(this.DtpRangeEnd_ValueChanged);
            // 
            // nudNTambour
            // 
            this.nudNTambour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNTambour.Location = new System.Drawing.Point(178, 64);
            this.nudNTambour.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudNTambour.Name = "nudNTambour";
            this.nudNTambour.Size = new System.Drawing.Size(73, 20);
            this.nudNTambour.TabIndex = 9;
            this.nudNTambour.ValueChanged += new System.EventHandler(this.NudNTambour_ValueChanged);
            // 
            // lCalibrationFileName
            // 
            this.lCalibrationFileName.Location = new System.Drawing.Point(12, 14);
            this.lCalibrationFileName.Name = "lCalibrationFileName";
            this.lCalibrationFileName.Size = new System.Drawing.Size(160, 20);
            this.lCalibrationFileName.TabIndex = 90;
            this.lCalibrationFileName.Text = "Начало диапазона:";
            this.lCalibrationFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
            this.label1.TabIndex = 90;
            this.label1.Text = "Конец диапазона:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(178, 90);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(163, 20);
            this.tbFileName.TabIndex = 98;
            this.tbFileName.TextChanged += new System.EventHandler(this.TbFileName_TextChanged);
            // 
            // lNTambour
            // 
            this.lNTambour.Location = new System.Drawing.Point(12, 64);
            this.lNTambour.Name = "lNTambour";
            this.lNTambour.Size = new System.Drawing.Size(160, 20);
            this.lNTambour.TabIndex = 90;
            this.lNTambour.Text = "Номер тамбура:";
            this.lNTambour.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lFileName
            // 
            this.lFileName.Location = new System.Drawing.Point(12, 90);
            this.lFileName.Name = "lFileName";
            this.lFileName.Size = new System.Drawing.Size(160, 20);
            this.lFileName.TabIndex = 90;
            this.lFileName.Text = "Имя файла отчета:";
            this.lFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bCreateReport
            // 
            this.bCreateReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreateReport.Location = new System.Drawing.Point(12, 117);
            this.bCreateReport.Name = "bCreateReport";
            this.bCreateReport.Size = new System.Drawing.Size(329, 23);
            this.bCreateReport.TabIndex = 100;
            this.bCreateReport.Text = "Создать отчет";
            this.bCreateReport.UseVisualStyleBackColor = true;
            this.bCreateReport.Click += new System.EventHandler(this.bCreateReport_Click);
            // 
            // cbIncludeTambour
            // 
            this.cbIncludeTambour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIncludeTambour.AutoSize = true;
            this.cbIncludeTambour.Checked = true;
            this.cbIncludeTambour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeTambour.Location = new System.Drawing.Point(257, 67);
            this.cbIncludeTambour.Name = "cbIncludeTambour";
            this.cbIncludeTambour.Size = new System.Drawing.Size(84, 17);
            this.cbIncludeTambour.TabIndex = 101;
            this.cbIncludeTambour.Text = "По тамбуру";
            this.cbIncludeTambour.UseVisualStyleBackColor = true;
            this.cbIncludeTambour.CheckedChanged += new System.EventHandler(this.CbIncludeTambour_CheckedChanged);
            // 
            // CreateReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 152);
            this.Controls.Add(this.cbIncludeTambour);
            this.Controls.Add(this.bCreateReport);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.lFileName);
            this.Controls.Add(this.lNTambour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lCalibrationFileName);
            this.Controls.Add(this.nudNTambour);
            this.Controls.Add(this.dtpRangeEnd);
            this.Controls.Add(this.dtpRangeBegin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Создание отчета";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateReportForm_FormClosing);
            this.Shown += new System.EventHandler(this.CreateReportForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudNTambour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpRangeBegin;
        private System.Windows.Forms.DateTimePicker dtpRangeEnd;
        private System.Windows.Forms.NumericUpDown nudNTambour;
        private System.Windows.Forms.Label lCalibrationFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label lNTambour;
        private System.Windows.Forms.Label lFileName;
        private System.Windows.Forms.Button bCreateReport;
        private System.Windows.Forms.CheckBox cbIncludeTambour;
    }
}