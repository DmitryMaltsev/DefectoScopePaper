namespace DefectoScope
{
    partial class LoadFrameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadFrameForm));
            this.gbModeLoad = new System.Windows.Forms.GroupBox();
            this.nudNTimes = new System.Windows.Forms.NumericUpDown();
            this.bApplyChanges = new System.Windows.Forms.Button();
            this.rbRepeatedly = new System.Windows.Forms.RadioButton();
            this.rbConstantly = new System.Windows.Forms.RadioButton();
            this.nudPeriod = new System.Windows.Forms.NumericUpDown();
            this.lPeriod = new System.Windows.Forms.Label();
            this.bStartLoad = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bLoad = new System.Windows.Forms.Button();
            this.bwLoad = new System.ComponentModel.BackgroundWorker();
            this.gbModeLoad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // gbModeLoad
            // 
            this.gbModeLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbModeLoad.Controls.Add(this.nudNTimes);
            this.gbModeLoad.Controls.Add(this.bApplyChanges);
            this.gbModeLoad.Controls.Add(this.rbRepeatedly);
            this.gbModeLoad.Controls.Add(this.rbConstantly);
            this.gbModeLoad.Controls.Add(this.nudPeriod);
            this.gbModeLoad.Controls.Add(this.lPeriod);
            this.gbModeLoad.Location = new System.Drawing.Point(12, 12);
            this.gbModeLoad.Name = "gbModeLoad";
            this.gbModeLoad.Size = new System.Drawing.Size(207, 101);
            this.gbModeLoad.TabIndex = 0;
            this.gbModeLoad.TabStop = false;
            this.gbModeLoad.Text = "Режим загрузки";
            // 
            // nudNTimes
            // 
            this.nudNTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNTimes.Location = new System.Drawing.Point(114, 19);
            this.nudNTimes.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudNTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNTimes.Name = "nudNTimes";
            this.nudNTimes.Size = new System.Drawing.Size(87, 20);
            this.nudNTimes.TabIndex = 2;
            this.nudNTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bApplyChanges
            // 
            this.bApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bApplyChanges.Location = new System.Drawing.Point(6, 71);
            this.bApplyChanges.Name = "bApplyChanges";
            this.bApplyChanges.Size = new System.Drawing.Size(195, 23);
            this.bApplyChanges.TabIndex = 5;
            this.bApplyChanges.Text = "Сохранить изменения";
            this.bApplyChanges.UseVisualStyleBackColor = true;
            this.bApplyChanges.Click += new System.EventHandler(this.bApplyChanges_Click);
            // 
            // rbRepeatedly
            // 
            this.rbRepeatedly.AutoSize = true;
            this.rbRepeatedly.Location = new System.Drawing.Point(6, 19);
            this.rbRepeatedly.Name = "rbRepeatedly";
            this.rbRepeatedly.Size = new System.Drawing.Size(102, 17);
            this.rbRepeatedly.TabIndex = 0;
            this.rbRepeatedly.TabStop = true;
            this.rbRepeatedly.Text = "Несколько раз";
            this.rbRepeatedly.UseVisualStyleBackColor = true;
            // 
            // rbConstantly
            // 
            this.rbConstantly.AutoSize = true;
            this.rbConstantly.Location = new System.Drawing.Point(6, 48);
            this.rbConstantly.Name = "rbConstantly";
            this.rbConstantly.Size = new System.Drawing.Size(89, 17);
            this.rbConstantly.TabIndex = 1;
            this.rbConstantly.TabStop = true;
            this.rbConstantly.Text = "Непрерывно";
            this.rbConstantly.UseVisualStyleBackColor = true;
            // 
            // nudPeriod
            // 
            this.nudPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPeriod.Location = new System.Drawing.Point(101, 45);
            this.nudPeriod.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPeriod.Name = "nudPeriod";
            this.nudPeriod.Size = new System.Drawing.Size(73, 20);
            this.nudPeriod.TabIndex = 3;
            this.nudPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lPeriod
            // 
            this.lPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lPeriod.AutoSize = true;
            this.lPeriod.Location = new System.Drawing.Point(180, 50);
            this.lPeriod.Name = "lPeriod";
            this.lPeriod.Size = new System.Drawing.Size(21, 13);
            this.lPeriod.TabIndex = 2;
            this.lPeriod.Text = "мс";
            this.lPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bStartLoad
            // 
            this.bStartLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStartLoad.Enabled = false;
            this.bStartLoad.Location = new System.Drawing.Point(12, 116);
            this.bStartLoad.Name = "bStartLoad";
            this.bStartLoad.Size = new System.Drawing.Size(60, 23);
            this.bStartLoad.TabIndex = 4;
            this.bStartLoad.Text = "Запуск";
            this.bStartLoad.UseVisualStyleBackColor = true;
            this.bStartLoad.Click += new System.EventHandler(this.bStartLoad_Click);
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bStop.Location = new System.Drawing.Point(159, 116);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(60, 23);
            this.bStop.TabIndex = 5;
            this.bStop.Text = "Стоп";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoad.Location = new System.Drawing.Point(78, 116);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(75, 23);
            this.bLoad.TabIndex = 6;
            this.bLoad.Text = "Открыть";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bwLoad
            // 
            this.bwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoad_DoWork);
            // 
            // LoadFrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 151);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bStartLoad);
            this.Controls.Add(this.gbModeLoad);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadFrameForm";
            this.ShowIcon = false;
            this.Text = "Загрузка кадра";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadFrameForm_FormClosing);
            this.gbModeLoad.ResumeLayout(false);
            this.gbModeLoad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbModeLoad;
        private System.Windows.Forms.Label lPeriod;
        private System.Windows.Forms.NumericUpDown nudNTimes;
        private System.Windows.Forms.RadioButton rbConstantly;
        private System.Windows.Forms.RadioButton rbRepeatedly;
        private System.Windows.Forms.Button bStartLoad;
        private System.Windows.Forms.NumericUpDown nudPeriod;
        private System.Windows.Forms.Button bApplyChanges;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bLoad;
        private System.ComponentModel.BackgroundWorker bwLoad;
    }
}