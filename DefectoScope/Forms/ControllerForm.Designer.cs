namespace DefectoScope
{
    partial class ControllerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllerForm));
            this.lStatusValue = new System.Windows.Forms.Label();
            this.gbRead = new System.Windows.Forms.GroupBox();
            this.cbReading = new System.Windows.Forms.CheckBox();
            this.lCountStrobeValue = new System.Windows.Forms.Label();
            this.lCountStrobe = new System.Windows.Forms.Label();
            this.nudDivider = new System.Windows.Forms.NumericUpDown();
            this.bWriteKeys = new System.Windows.Forms.Button();
            this.cbKey17 = new System.Windows.Forms.CheckBox();
            this.cbKey16 = new System.Windows.Forms.CheckBox();
            this.lDivider = new System.Windows.Forms.Label();
            this.gbKeys = new System.Windows.Forms.GroupBox();
            this.bStop = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.bSetName = new System.Windows.Forms.Button();
            this.lStatus = new System.Windows.Forms.Label();
            this.bWriteConfig = new System.Windows.Forms.Button();
            this.tbComPortName = new System.Windows.Forms.TextBox();
            this.lComPortName = new System.Windows.Forms.Label();
            this.gbController = new System.Windows.Forms.GroupBox();
            this.pbTestOpen = new System.Windows.Forms.PictureBox();
            this.bCurrentPortName = new System.Windows.Forms.Button();
            this.gbRead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivider)).BeginInit();
            this.gbKeys.SuspendLayout();
            this.gbController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestOpen)).BeginInit();
            this.SuspendLayout();
            // 
            // lStatusValue
            // 
            this.lStatusValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatusValue.AutoSize = true;
            this.lStatusValue.Location = new System.Drawing.Point(56, 296);
            this.lStatusValue.Name = "lStatusValue";
            this.lStatusValue.Size = new System.Drawing.Size(25, 13);
            this.lStatusValue.TabIndex = 96;
            this.lStatusValue.Text = "???";
            // 
            // gbRead
            // 
            this.gbRead.Controls.Add(this.cbReading);
            this.gbRead.Controls.Add(this.lCountStrobeValue);
            this.gbRead.Controls.Add(this.lCountStrobe);
            this.gbRead.Location = new System.Drawing.Point(9, 226);
            this.gbRead.Name = "gbRead";
            this.gbRead.Size = new System.Drawing.Size(203, 63);
            this.gbRead.TabIndex = 93;
            this.gbRead.TabStop = false;
            this.gbRead.Text = "Чтение";
            // 
            // cbReading
            // 
            this.cbReading.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbReading.Location = new System.Drawing.Point(7, 19);
            this.cbReading.Name = "cbReading";
            this.cbReading.Size = new System.Drawing.Size(191, 20);
            this.cbReading.TabIndex = 97;
            this.cbReading.Text = "Процесс чтения стробов";
            this.cbReading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbReading.UseVisualStyleBackColor = true;
            this.cbReading.CheckedChanged += new System.EventHandler(this.cbReading_CheckedChanged);
            // 
            // lCountStrobeValue
            // 
            this.lCountStrobeValue.AutoSize = true;
            this.lCountStrobeValue.Location = new System.Drawing.Point(101, 42);
            this.lCountStrobeValue.Name = "lCountStrobeValue";
            this.lCountStrobeValue.Size = new System.Drawing.Size(25, 13);
            this.lCountStrobeValue.TabIndex = 95;
            this.lCountStrobeValue.Text = "???";
            // 
            // lCountStrobe
            // 
            this.lCountStrobe.AutoSize = true;
            this.lCountStrobe.Location = new System.Drawing.Point(4, 42);
            this.lCountStrobe.Name = "lCountStrobe";
            this.lCountStrobe.Size = new System.Drawing.Size(94, 13);
            this.lCountStrobe.TabIndex = 94;
            this.lCountStrobe.Text = "Счетчик стробов:";
            // 
            // nudDivider
            // 
            this.nudDivider.Location = new System.Drawing.Point(162, 69);
            this.nudDivider.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDivider.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDivider.Name = "nudDivider";
            this.nudDivider.Size = new System.Drawing.Size(50, 20);
            this.nudDivider.TabIndex = 87;
            this.nudDivider.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // bWriteKeys
            // 
            this.bWriteKeys.Location = new System.Drawing.Point(7, 46);
            this.bWriteKeys.Name = "bWriteKeys";
            this.bWriteKeys.Size = new System.Drawing.Size(187, 21);
            this.bWriteKeys.TabIndex = 82;
            this.bWriteKeys.Text = "Записать состояние ключей";
            this.bWriteKeys.UseVisualStyleBackColor = true;
            this.bWriteKeys.Click += new System.EventHandler(this.bWriteKeys_Click);
            // 
            // cbKey17
            // 
            this.cbKey17.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbKey17.Location = new System.Drawing.Point(103, 20);
            this.cbKey17.Name = "cbKey17";
            this.cbKey17.Size = new System.Drawing.Size(91, 20);
            this.cbKey17.TabIndex = 1;
            this.cbKey17.Text = "Ключ 17";
            this.cbKey17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbKey17.UseVisualStyleBackColor = true;
            // 
            // cbKey16
            // 
            this.cbKey16.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbKey16.Location = new System.Drawing.Point(6, 20);
            this.cbKey16.Name = "cbKey16";
            this.cbKey16.Size = new System.Drawing.Size(91, 20);
            this.cbKey16.TabIndex = 0;
            this.cbKey16.Text = "Ключ 16";
            this.cbKey16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbKey16.UseVisualStyleBackColor = true;
            // 
            // lDivider
            // 
            this.lDivider.Location = new System.Drawing.Point(9, 69);
            this.lDivider.Name = "lDivider";
            this.lDivider.Size = new System.Drawing.Size(147, 20);
            this.lDivider.TabIndex = 86;
            this.lDivider.Text = "Делитель:";
            this.lDivider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbKeys
            // 
            this.gbKeys.Controls.Add(this.bWriteKeys);
            this.gbKeys.Controls.Add(this.cbKey17);
            this.gbKeys.Controls.Add(this.cbKey16);
            this.gbKeys.Location = new System.Drawing.Point(9, 149);
            this.gbKeys.Name = "gbKeys";
            this.gbKeys.Size = new System.Drawing.Size(203, 71);
            this.gbKeys.TabIndex = 85;
            this.gbKeys.TabStop = false;
            this.gbKeys.Text = "Управление ключами";
            // 
            // bStop
            // 
            this.bStop.Location = new System.Drawing.Point(112, 122);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(100, 21);
            this.bStop.TabIndex = 89;
            this.bStop.Text = "Стоп";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(9, 122);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(97, 21);
            this.bStart.TabIndex = 88;
            this.bStart.Text = "Старт";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bSetName
            // 
            this.bSetName.Location = new System.Drawing.Point(112, 42);
            this.bSetName.Name = "bSetName";
            this.bSetName.Size = new System.Drawing.Size(90, 21);
            this.bSetName.TabIndex = 83;
            this.bSetName.Text = "Установить";
            this.bSetName.UseVisualStyleBackColor = true;
            this.bSetName.Click += new System.EventHandler(this.bSetName_Click);
            // 
            // lStatus
            // 
            this.lStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(6, 296);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(44, 13);
            this.lStatus.TabIndex = 82;
            this.lStatus.Text = "Статус:";
            // 
            // bWriteConfig
            // 
            this.bWriteConfig.Location = new System.Drawing.Point(9, 95);
            this.bWriteConfig.Name = "bWriteConfig";
            this.bWriteConfig.Size = new System.Drawing.Size(203, 21);
            this.bWriteConfig.TabIndex = 81;
            this.bWriteConfig.Text = "Записать конфигурацию";
            this.bWriteConfig.UseVisualStyleBackColor = true;
            this.bWriteConfig.Click += new System.EventHandler(this.bWriteConfig_Click);
            // 
            // tbComPortName
            // 
            this.tbComPortName.Location = new System.Drawing.Point(162, 16);
            this.tbComPortName.Name = "tbComPortName";
            this.tbComPortName.Size = new System.Drawing.Size(50, 20);
            this.tbComPortName.TabIndex = 1;
            // 
            // lComPortName
            // 
            this.lComPortName.Location = new System.Drawing.Point(6, 15);
            this.lComPortName.Name = "lComPortName";
            this.lComPortName.Size = new System.Drawing.Size(150, 20);
            this.lComPortName.TabIndex = 0;
            this.lComPortName.Text = "Имя COM-порта:";
            this.lComPortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbController
            // 
            this.gbController.Controls.Add(this.lStatusValue);
            this.gbController.Controls.Add(this.gbRead);
            this.gbController.Controls.Add(this.bStop);
            this.gbController.Controls.Add(this.bStart);
            this.gbController.Controls.Add(this.nudDivider);
            this.gbController.Controls.Add(this.lDivider);
            this.gbController.Controls.Add(this.gbKeys);
            this.gbController.Controls.Add(this.pbTestOpen);
            this.gbController.Controls.Add(this.bCurrentPortName);
            this.gbController.Controls.Add(this.bSetName);
            this.gbController.Controls.Add(this.lStatus);
            this.gbController.Controls.Add(this.bWriteConfig);
            this.gbController.Controls.Add(this.tbComPortName);
            this.gbController.Controls.Add(this.lComPortName);
            this.gbController.Location = new System.Drawing.Point(12, 12);
            this.gbController.Name = "gbController";
            this.gbController.Size = new System.Drawing.Size(221, 312);
            this.gbController.TabIndex = 1;
            this.gbController.TabStop = false;
            this.gbController.Text = "Контроллер синхронизации";
            // 
            // pbTestOpen
            // 
            this.pbTestOpen.BackColor = System.Drawing.SystemColors.Window;
            this.pbTestOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbTestOpen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTestOpen.Image = global::DefectoScope.Properties.Resources.unknown;
            this.pbTestOpen.Location = new System.Drawing.Point(196, 42);
            this.pbTestOpen.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pbTestOpen.Name = "pbTestOpen";
            this.pbTestOpen.Size = new System.Drawing.Size(16, 21);
            this.pbTestOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestOpen.TabIndex = 84;
            this.pbTestOpen.TabStop = false;
            // 
            // bCurrentPortName
            // 
            this.bCurrentPortName.Location = new System.Drawing.Point(9, 42);
            this.bCurrentPortName.Name = "bCurrentPortName";
            this.bCurrentPortName.Size = new System.Drawing.Size(97, 21);
            this.bCurrentPortName.TabIndex = 83;
            this.bCurrentPortName.Text = "Текущее имя";
            this.bCurrentPortName.UseVisualStyleBackColor = true;
            this.bCurrentPortName.Click += new System.EventHandler(this.BCurrentPortName_Click);
            // 
            // ControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 339);
            this.Controls.Add(this.gbController);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ControllerForm";
            this.ShowIcon = false;
            this.Text = "Контроллер синхронизации";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControllerForm_FormClosing);
            this.Load += new System.EventHandler(this.ControllerForm_Load);
            this.gbRead.ResumeLayout(false);
            this.gbRead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDivider)).EndInit();
            this.gbKeys.ResumeLayout(false);
            this.gbController.ResumeLayout(false);
            this.gbController.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestOpen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lStatusValue;
        private System.Windows.Forms.GroupBox gbRead;
        private System.Windows.Forms.CheckBox cbReading;
        private System.Windows.Forms.Label lCountStrobeValue;
        private System.Windows.Forms.Label lCountStrobe;
        private System.Windows.Forms.NumericUpDown nudDivider;
        private System.Windows.Forms.Button bWriteKeys;
        private System.Windows.Forms.CheckBox cbKey17;
        private System.Windows.Forms.CheckBox cbKey16;
        private System.Windows.Forms.Label lDivider;
        private System.Windows.Forms.GroupBox gbKeys;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.PictureBox pbTestOpen;
        private System.Windows.Forms.Button bSetName;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Button bWriteConfig;
        private System.Windows.Forms.TextBox tbComPortName;
        private System.Windows.Forms.Label lComPortName;
        private System.Windows.Forms.GroupBox gbController;
        private System.Windows.Forms.Button bCurrentPortName;
    }
}