namespace DefectoScope
{
    partial class ManualModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualModeForm));
            this.gbStrobes = new System.Windows.Forms.GroupBox();
            this.nudNStrobes = new System.Windows.Forms.NumericUpDown();
            this.bApplyChanges = new System.Windows.Forms.Button();
            this.rbRepeatedly = new System.Windows.Forms.RadioButton();
            this.rbConstantly = new System.Windows.Forms.RadioButton();
            this.nudFreqStrobes = new System.Windows.Forms.NumericUpDown();
            this.lFreqStrobes = new System.Windows.Forms.Label();
            this.bStartManualMode = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.gbStrobes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNStrobes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqStrobes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbStrobes
            // 
            this.gbStrobes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStrobes.Controls.Add(this.nudNStrobes);
            this.gbStrobes.Controls.Add(this.bApplyChanges);
            this.gbStrobes.Controls.Add(this.rbRepeatedly);
            this.gbStrobes.Controls.Add(this.rbConstantly);
            this.gbStrobes.Controls.Add(this.nudFreqStrobes);
            this.gbStrobes.Controls.Add(this.lFreqStrobes);
            this.gbStrobes.Location = new System.Drawing.Point(12, 12);
            this.gbStrobes.Name = "gbStrobes";
            this.gbStrobes.Size = new System.Drawing.Size(207, 101);
            this.gbStrobes.TabIndex = 0;
            this.gbStrobes.TabStop = false;
            this.gbStrobes.Text = "Стробы";
            // 
            // nudNStrobes
            // 
            this.nudNStrobes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNStrobes.Location = new System.Drawing.Point(137, 19);
            this.nudNStrobes.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudNStrobes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNStrobes.Name = "nudNStrobes";
            this.nudNStrobes.Size = new System.Drawing.Size(64, 20);
            this.nudNStrobes.TabIndex = 2;
            this.nudNStrobes.Value = new decimal(new int[] {
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
            this.rbRepeatedly.Size = new System.Drawing.Size(125, 17);
            this.rbRepeatedly.TabIndex = 0;
            this.rbRepeatedly.TabStop = true;
            this.rbRepeatedly.Text = "Несколько стробов";
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
            // nudFreqStrobes
            // 
            this.nudFreqStrobes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFreqStrobes.Location = new System.Drawing.Point(101, 45);
            this.nudFreqStrobes.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFreqStrobes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFreqStrobes.Name = "nudFreqStrobes";
            this.nudFreqStrobes.Size = new System.Drawing.Size(73, 20);
            this.nudFreqStrobes.TabIndex = 3;
            this.nudFreqStrobes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lFreqStrobes
            // 
            this.lFreqStrobes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lFreqStrobes.AutoSize = true;
            this.lFreqStrobes.Location = new System.Drawing.Point(180, 50);
            this.lFreqStrobes.Name = "lFreqStrobes";
            this.lFreqStrobes.Size = new System.Drawing.Size(19, 13);
            this.lFreqStrobes.TabIndex = 2;
            this.lFreqStrobes.Text = "Гц";
            this.lFreqStrobes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bStartManualMode
            // 
            this.bStartManualMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStartManualMode.Enabled = false;
            this.bStartManualMode.Location = new System.Drawing.Point(12, 116);
            this.bStartManualMode.Name = "bStartManualMode";
            this.bStartManualMode.Size = new System.Drawing.Size(131, 23);
            this.bStartManualMode.TabIndex = 4;
            this.bStartManualMode.Text = "Запуск ручной работы";
            this.bStartManualMode.UseVisualStyleBackColor = true;
            this.bStartManualMode.Click += new System.EventHandler(this.bStartManualMode_Click);
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStop.Location = new System.Drawing.Point(149, 116);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(70, 23);
            this.bStop.TabIndex = 5;
            this.bStop.Text = "Стоп";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // ManualModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 151);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bStartManualMode);
            this.Controls.Add(this.gbStrobes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManualModeForm";
            this.ShowIcon = false;
            this.Text = "Ручная работа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManualModeForm_FormClosing);
            this.gbStrobes.ResumeLayout(false);
            this.gbStrobes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNStrobes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqStrobes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStrobes;
        private System.Windows.Forms.Label lFreqStrobes;
        private System.Windows.Forms.NumericUpDown nudNStrobes;
        private System.Windows.Forms.RadioButton rbConstantly;
        private System.Windows.Forms.RadioButton rbRepeatedly;
        private System.Windows.Forms.Button bStartManualMode;
        private System.Windows.Forms.NumericUpDown nudFreqStrobes;
        private System.Windows.Forms.Button bApplyChanges;
        private System.Windows.Forms.Button bStop;
    }
}