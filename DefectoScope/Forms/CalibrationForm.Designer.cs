using System.Windows.Forms.DataVisualization.Charting;

namespace DefectoScope
{
    partial class CalibrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationForm));
            this.cProfile = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bSave = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lCount = new System.Windows.Forms.Label();
            this.lCountValue = new System.Windows.Forms.Label();
            this.bReset = new System.Windows.Forms.Button();
            this.lFileName = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.gbCalibration = new System.Windows.Forms.GroupBox();
            this.gbSensors = new System.Windows.Forms.GroupBox();
            this.dynamicSensorControl1 = new DefectoScope.DynamicSensorControl();
            ((System.ComponentModel.ISupportInitialize)(this.cProfile)).BeginInit();
            this.gbCalibration.SuspendLayout();
            this.gbSensors.SuspendLayout();
            this.SuspendLayout();
            // 
            // cProfile
            // 
            this.cProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cProfile.BackColor = System.Drawing.SystemColors.Control;
            this.cProfile.BorderlineColor = System.Drawing.SystemColors.Control;
            this.cProfile.Location = new System.Drawing.Point(12, 33);
            this.cProfile.Name = "cProfile";
            this.cProfile.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.cProfile.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Lime,
        System.Drawing.Color.Blue};
            this.cProfile.Size = new System.Drawing.Size(460, 272);
            this.cProfile.TabIndex = 30;
            this.cProfile.Text = "cProfile";
            this.cProfile.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.chData_MouseWheel);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(6, 162);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(177, 23);
            this.bSave.TabIndex = 31;
            this.bSave.Text = "Сохранить и применить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bStart
            // 
            this.bStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bStart.Location = new System.Drawing.Point(6, 19);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(177, 23);
            this.bStart.TabIndex = 33;
            this.bStart.Text = "Начать калибровку";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Enabled = false;
            this.bCancel.Location = new System.Drawing.Point(6, 48);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(177, 23);
            this.bCancel.TabIndex = 34;
            this.bCancel.Text = "Отменить калибровку";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // lCount
            // 
            this.lCount.AutoSize = true;
            this.lCount.Location = new System.Drawing.Point(13, 9);
            this.lCount.Name = "lCount";
            this.lCount.Size = new System.Drawing.Size(180, 13);
            this.lCount.TabIndex = 35;
            this.lCount.Text = "Количество собранных профилей:";
            // 
            // lCountValue
            // 
            this.lCountValue.AutoSize = true;
            this.lCountValue.Location = new System.Drawing.Point(199, 9);
            this.lCountValue.Name = "lCountValue";
            this.lCountValue.Size = new System.Drawing.Size(25, 13);
            this.lCountValue.TabIndex = 36;
            this.lCountValue.Text = "???";
            // 
            // bReset
            // 
            this.bReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bReset.Location = new System.Drawing.Point(6, 77);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(177, 23);
            this.bReset.TabIndex = 37;
            this.bReset.Text = "Сбросить";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // lFileName
            // 
            this.lFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lFileName.Location = new System.Drawing.Point(6, 103);
            this.lFileName.Name = "lFileName";
            this.lFileName.Size = new System.Drawing.Size(177, 23);
            this.lFileName.TabIndex = 38;
            this.lFileName.Text = "Имя файла калибровки:";
            this.lFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(6, 129);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(177, 20);
            this.tbFileName.TabIndex = 39;
            this.tbFileName.TextChanged += new System.EventHandler(this.TbFileName_TextChanged);
            // 
            // gbCalibration
            // 
            this.gbCalibration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalibration.Controls.Add(this.bStart);
            this.gbCalibration.Controls.Add(this.bCancel);
            this.gbCalibration.Controls.Add(this.tbFileName);
            this.gbCalibration.Controls.Add(this.bReset);
            this.gbCalibration.Controls.Add(this.bSave);
            this.gbCalibration.Controls.Add(this.lFileName);
            this.gbCalibration.Location = new System.Drawing.Point(12, 311);
            this.gbCalibration.Name = "gbCalibration";
            this.gbCalibration.Size = new System.Drawing.Size(189, 188);
            this.gbCalibration.TabIndex = 41;
            this.gbCalibration.TabStop = false;
            this.gbCalibration.Text = "Калибровка";
            // 
            // gbSensors
            // 
            this.gbSensors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSensors.Controls.Add(this.dynamicSensorControl1);
            this.gbSensors.Location = new System.Drawing.Point(207, 311);
            this.gbSensors.Name = "gbSensors";
            this.gbSensors.Size = new System.Drawing.Size(265, 188);
            this.gbSensors.TabIndex = 42;
            this.gbSensors.TabStop = false;
            this.gbSensors.Text = "Датчики";
            // 
            // dynamicSensorControl1
            // 
            this.dynamicSensorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dynamicSensorControl1.Location = new System.Drawing.Point(3, 16);
            this.dynamicSensorControl1.MinimumSize = new System.Drawing.Size(250, 130);
            this.dynamicSensorControl1.Name = "dynamicSensorControl1";
            this.dynamicSensorControl1.Sensors = new DefectoScope.Sensor[0];
            this.dynamicSensorControl1.Size = new System.Drawing.Size(259, 169);
            this.dynamicSensorControl1.TabIndex = 40;
            // 
            // CalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 511);
            this.Controls.Add(this.gbSensors);
            this.Controls.Add(this.gbCalibration);
            this.Controls.Add(this.lCountValue);
            this.Controls.Add(this.lCount);
            this.Controls.Add(this.cProfile);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "CalibrationForm";
            this.ShowIcon = false;
            this.Text = "Окно калибровки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalibrationForm_FormClosing);
            this.Load += new System.EventHandler(this.CalibrationForm_Load);
            this.ResizeBegin += new System.EventHandler(this.CalibrationForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.CalibrationForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.cProfile)).EndInit();
            this.gbCalibration.ResumeLayout(false);
            this.gbCalibration.PerformLayout();
            this.gbSensors.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Chart cProfile;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lCount;
        private System.Windows.Forms.Label lCountValue;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Label lFileName;
        private System.Windows.Forms.TextBox tbFileName;
        private DynamicSensorControl dynamicSensorControl1;
        private System.Windows.Forms.GroupBox gbCalibration;
        private System.Windows.Forms.GroupBox gbSensors;
    }
}