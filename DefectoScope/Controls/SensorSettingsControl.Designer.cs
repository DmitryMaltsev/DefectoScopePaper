namespace DefectoScope
{
    partial class SensorSettingsControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbSensorSettings = new System.Windows.Forms.GroupBox();
            this.bApply = new System.Windows.Forms.Button();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.bAdd = new System.Windows.Forms.Button();
            this.lKMmToPixel = new System.Windows.Forms.Label();
            this.nudKPixelsToMm = new System.Windows.Forms.NumericUpDown();
            this.lIp = new System.Windows.Forms.Label();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.lId = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.lPgaGain = new System.Windows.Forms.Label();
            this.nudPgaGain = new System.Windows.Forms.NumericUpDown();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.nudAdcGain = new System.Windows.Forms.NumericUpDown();
            this.lAdcGain = new System.Windows.Forms.Label();
            this.nudExposition = new System.Windows.Forms.NumericUpDown();
            this.nudLeftWindow = new System.Windows.Forms.NumericUpDown();
            this.lExposition = new System.Windows.Forms.Label();
            this.nudHeightWindow = new System.Windows.Forms.NumericUpDown();
            this.lLeftWindow = new System.Windows.Forms.Label();
            this.nudWidthWindow = new System.Windows.Forms.NumericUpDown();
            this.lHeightWindow = new System.Windows.Forms.Label();
            this.lTopWindow = new System.Windows.Forms.Label();
            this.nudTopWindow = new System.Windows.Forms.NumericUpDown();
            this.lWidthWindow = new System.Windows.Forms.Label();
            this.lNSensor = new System.Windows.Forms.Label();
            this.cbNSensor = new System.Windows.Forms.ComboBox();
            this.tCheckState = new System.Windows.Forms.Timer(this.components);
            this.nudNSensors = new System.Windows.Forms.NumericUpDown();
            this.lNSensors = new System.Windows.Forms.Label();
            this.gbSensorSettings.SuspendLayout();
            this.gbConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKPixelsToMm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPgaGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdcGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExposition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidthWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNSensors)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSensorSettings
            // 
            this.gbSensorSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSensorSettings.Controls.Add(this.bApply);
            this.gbSensorSettings.Controls.Add(this.gbConfig);
            this.gbSensorSettings.Controls.Add(this.lNSensor);
            this.gbSensorSettings.Controls.Add(this.cbNSensor);
            this.gbSensorSettings.Location = new System.Drawing.Point(3, 30);
            this.gbSensorSettings.Name = "gbSensorSettings";
            this.gbSensorSettings.Size = new System.Drawing.Size(294, 400);
            this.gbSensorSettings.TabIndex = 2;
            this.gbSensorSettings.TabStop = false;
            this.gbSensorSettings.Text = "Конфигурации датчиков";
            // 
            // bApply
            // 
            this.bApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bApply.Location = new System.Drawing.Point(6, 46);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(282, 23);
            this.bApply.TabIndex = 67;
            this.bApply.Text = "Применить текущую конфигурацию";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // gbConfig
            // 
            this.gbConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConfig.Controls.Add(this.bAdd);
            this.gbConfig.Controls.Add(this.lKMmToPixel);
            this.gbConfig.Controls.Add(this.nudKPixelsToMm);
            this.gbConfig.Controls.Add(this.lIp);
            this.gbConfig.Controls.Add(this.tbIp);
            this.gbConfig.Controls.Add(this.lId);
            this.gbConfig.Controls.Add(this.bSave);
            this.gbConfig.Controls.Add(this.pbDelete);
            this.gbConfig.Controls.Add(this.lPgaGain);
            this.gbConfig.Controls.Add(this.nudPgaGain);
            this.gbConfig.Controls.Add(this.cbId);
            this.gbConfig.Controls.Add(this.nudAdcGain);
            this.gbConfig.Controls.Add(this.lAdcGain);
            this.gbConfig.Controls.Add(this.nudExposition);
            this.gbConfig.Controls.Add(this.nudLeftWindow);
            this.gbConfig.Controls.Add(this.lExposition);
            this.gbConfig.Controls.Add(this.nudHeightWindow);
            this.gbConfig.Controls.Add(this.lLeftWindow);
            this.gbConfig.Controls.Add(this.nudWidthWindow);
            this.gbConfig.Controls.Add(this.lHeightWindow);
            this.gbConfig.Controls.Add(this.lTopWindow);
            this.gbConfig.Controls.Add(this.nudTopWindow);
            this.gbConfig.Controls.Add(this.lWidthWindow);
            this.gbConfig.Location = new System.Drawing.Point(6, 85);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(282, 309);
            this.gbConfig.TabIndex = 66;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Конфигурация";
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(9, 280);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(147, 23);
            this.bAdd.TabIndex = 68;
            this.bAdd.Text = "Создать";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lKMmToPixel
            // 
            this.lKMmToPixel.Location = new System.Drawing.Point(6, 252);
            this.lKMmToPixel.Name = "lKMmToPixel";
            this.lKMmToPixel.Size = new System.Drawing.Size(150, 20);
            this.lKMmToPixel.TabIndex = 63;
            this.lKMmToPixel.Text = "Пикселей на мм:";
            this.lKMmToPixel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudKPixelsToMm
            // 
            this.nudKPixelsToMm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudKPixelsToMm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudKPixelsToMm.DecimalPlaces = 6;
            this.nudKPixelsToMm.Location = new System.Drawing.Point(162, 254);
            this.nudKPixelsToMm.Name = "nudKPixelsToMm";
            this.nudKPixelsToMm.Size = new System.Drawing.Size(117, 20);
            this.nudKPixelsToMm.TabIndex = 64;
            this.nudKPixelsToMm.ValueChanged += new System.EventHandler(this.nudKPixelsToMm_ValueChanged);
            // 
            // lIp
            // 
            this.lIp.Location = new System.Drawing.Point(6, 44);
            this.lIp.Name = "lIp";
            this.lIp.Size = new System.Drawing.Size(150, 20);
            this.lIp.TabIndex = 29;
            this.lIp.Text = "IP-адрес:";
            this.lIp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbIp
            // 
            this.tbIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIp.BackColor = System.Drawing.SystemColors.Window;
            this.tbIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbIp.Location = new System.Drawing.Point(162, 46);
            this.tbIp.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.tbIp.MaxLength = 15;
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(117, 20);
            this.tbIp.TabIndex = 44;
            this.tbIp.TextChanged += new System.EventHandler(this.tbIp_TextChanged);
            // 
            // lId
            // 
            this.lId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lId.Location = new System.Drawing.Point(6, 18);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(150, 20);
            this.lId.TabIndex = 42;
            this.lId.Text = "Имя конфигурации:";
            this.lId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(162, 280);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(117, 23);
            this.bSave.TabIndex = 60;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // pbDelete
            // 
            this.pbDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDelete.BackColor = System.Drawing.SystemColors.Window;
            this.pbDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDelete.Image = global::DefectoScope.Properties.Resources.delete;
            this.pbDelete.Location = new System.Drawing.Point(263, 19);
            this.pbDelete.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(16, 21);
            this.pbDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDelete.TabIndex = 62;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            this.pbDelete.MouseEnter += new System.EventHandler(this.pbDeleteId_MouseEnter);
            this.pbDelete.MouseLeave += new System.EventHandler(this.pbDeleteId_MouseLeave);
            // 
            // lPgaGain
            // 
            this.lPgaGain.Location = new System.Drawing.Point(6, 226);
            this.lPgaGain.Name = "lPgaGain";
            this.lPgaGain.Size = new System.Drawing.Size(150, 20);
            this.lPgaGain.TabIndex = 30;
            this.lPgaGain.Text = "Аналоговое усиление:";
            this.lPgaGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudPgaGain
            // 
            this.nudPgaGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPgaGain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPgaGain.Location = new System.Drawing.Point(162, 228);
            this.nudPgaGain.Name = "nudPgaGain";
            this.nudPgaGain.Size = new System.Drawing.Size(117, 20);
            this.nudPgaGain.TabIndex = 57;
            this.nudPgaGain.ValueChanged += new System.EventHandler(this.nudPgaGain_ValueChanged);
            // 
            // cbId
            // 
            this.cbId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbId.Location = new System.Drawing.Point(162, 19);
            this.cbId.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(101, 21);
            this.cbId.TabIndex = 43;
            this.cbId.SelectedIndexChanged += new System.EventHandler(this.cbId_SelectedIndexChanged);
            // 
            // nudAdcGain
            // 
            this.nudAdcGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudAdcGain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudAdcGain.Location = new System.Drawing.Point(162, 202);
            this.nudAdcGain.Name = "nudAdcGain";
            this.nudAdcGain.Size = new System.Drawing.Size(117, 20);
            this.nudAdcGain.TabIndex = 57;
            this.nudAdcGain.ValueChanged += new System.EventHandler(this.nudAdcGain_ValueChanged);
            // 
            // lAdcGain
            // 
            this.lAdcGain.Location = new System.Drawing.Point(6, 200);
            this.lAdcGain.Name = "lAdcGain";
            this.lAdcGain.Size = new System.Drawing.Size(150, 20);
            this.lAdcGain.TabIndex = 30;
            this.lAdcGain.Text = "Цифровое усиление:";
            this.lAdcGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudExposition
            // 
            this.nudExposition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudExposition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudExposition.Location = new System.Drawing.Point(162, 176);
            this.nudExposition.Name = "nudExposition";
            this.nudExposition.Size = new System.Drawing.Size(117, 20);
            this.nudExposition.TabIndex = 56;
            this.nudExposition.ValueChanged += new System.EventHandler(this.nudExposition_ValueChanged);
            // 
            // nudLeftWindow
            // 
            this.nudLeftWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLeftWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudLeftWindow.Location = new System.Drawing.Point(162, 72);
            this.nudLeftWindow.Name = "nudLeftWindow";
            this.nudLeftWindow.Size = new System.Drawing.Size(117, 20);
            this.nudLeftWindow.TabIndex = 52;
            this.nudLeftWindow.ValueChanged += new System.EventHandler(this.nudLeftWindow_ValueChanged);
            // 
            // lExposition
            // 
            this.lExposition.Location = new System.Drawing.Point(6, 174);
            this.lExposition.Name = "lExposition";
            this.lExposition.Size = new System.Drawing.Size(150, 20);
            this.lExposition.TabIndex = 31;
            this.lExposition.Text = "Время экспозиции:";
            this.lExposition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudHeightWindow
            // 
            this.nudHeightWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHeightWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudHeightWindow.Location = new System.Drawing.Point(162, 150);
            this.nudHeightWindow.Name = "nudHeightWindow";
            this.nudHeightWindow.Size = new System.Drawing.Size(117, 20);
            this.nudHeightWindow.TabIndex = 55;
            this.nudHeightWindow.ValueChanged += new System.EventHandler(this.nudHeightWindow_ValueChanged);
            // 
            // lLeftWindow
            // 
            this.lLeftWindow.Location = new System.Drawing.Point(6, 70);
            this.lLeftWindow.Name = "lLeftWindow";
            this.lLeftWindow.Size = new System.Drawing.Size(150, 20);
            this.lLeftWindow.TabIndex = 34;
            this.lLeftWindow.Text = "Левая граница окна:";
            this.lLeftWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudWidthWindow
            // 
            this.nudWidthWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWidthWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudWidthWindow.Location = new System.Drawing.Point(162, 124);
            this.nudWidthWindow.Name = "nudWidthWindow";
            this.nudWidthWindow.Size = new System.Drawing.Size(117, 20);
            this.nudWidthWindow.TabIndex = 54;
            this.nudWidthWindow.ValueChanged += new System.EventHandler(this.nudWidthWindow_ValueChanged);
            // 
            // lHeightWindow
            // 
            this.lHeightWindow.Location = new System.Drawing.Point(6, 148);
            this.lHeightWindow.Name = "lHeightWindow";
            this.lHeightWindow.Size = new System.Drawing.Size(150, 20);
            this.lHeightWindow.TabIndex = 32;
            this.lHeightWindow.Text = "Высота окна:";
            this.lHeightWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lTopWindow
            // 
            this.lTopWindow.Location = new System.Drawing.Point(6, 96);
            this.lTopWindow.Name = "lTopWindow";
            this.lTopWindow.Size = new System.Drawing.Size(150, 20);
            this.lTopWindow.TabIndex = 33;
            this.lTopWindow.Text = "Верхняя граница окна:";
            this.lTopWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudTopWindow
            // 
            this.nudTopWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTopWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudTopWindow.Location = new System.Drawing.Point(162, 98);
            this.nudTopWindow.Name = "nudTopWindow";
            this.nudTopWindow.Size = new System.Drawing.Size(117, 20);
            this.nudTopWindow.TabIndex = 53;
            this.nudTopWindow.ValueChanged += new System.EventHandler(this.nudTopWindow_ValueChanged);
            // 
            // lWidthWindow
            // 
            this.lWidthWindow.Location = new System.Drawing.Point(6, 122);
            this.lWidthWindow.Name = "lWidthWindow";
            this.lWidthWindow.Size = new System.Drawing.Size(150, 20);
            this.lWidthWindow.TabIndex = 35;
            this.lWidthWindow.Text = "Ширина окна:";
            this.lWidthWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lNSensor
            // 
            this.lNSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNSensor.Location = new System.Drawing.Point(6, 18);
            this.lNSensor.Name = "lNSensor";
            this.lNSensor.Size = new System.Drawing.Size(156, 20);
            this.lNSensor.TabIndex = 65;
            this.lNSensor.Text = "№ датчика: ";
            this.lNSensor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbNSensor
            // 
            this.cbNSensor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNSensor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNSensor.FormatString = "N0";
            this.cbNSensor.FormattingEnabled = true;
            this.cbNSensor.Location = new System.Drawing.Point(168, 19);
            this.cbNSensor.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbNSensor.Name = "cbNSensor";
            this.cbNSensor.Size = new System.Drawing.Size(120, 21);
            this.cbNSensor.TabIndex = 63;
            this.cbNSensor.SelectedIndexChanged += new System.EventHandler(this.cbNSensor_SelectedIndexChanged);
            // 
            // tCheckState
            // 
            this.tCheckState.Enabled = true;
            this.tCheckState.Tick += new System.EventHandler(this.tCheckState_Tick);
            // 
            // nudNSensors
            // 
            this.nudNSensors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNSensors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudNSensors.Location = new System.Drawing.Point(171, 3);
            this.nudNSensors.Name = "nudNSensors";
            this.nudNSensors.Size = new System.Drawing.Size(126, 20);
            this.nudNSensors.TabIndex = 54;
            // 
            // lNSensors
            // 
            this.lNSensors.Location = new System.Drawing.Point(3, 3);
            this.lNSensors.Name = "lNSensors";
            this.lNSensors.Size = new System.Drawing.Size(162, 20);
            this.lNSensors.TabIndex = 53;
            this.lNSensors.Text = "Количество датчиков:";
            this.lNSensors.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SensorSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudNSensors);
            this.Controls.Add(this.lNSensors);
            this.Controls.Add(this.gbSensorSettings);
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(300, 430);
            this.Name = "SensorSettingsControl";
            this.Size = new System.Drawing.Size(300, 430);
            this.gbSensorSettings.ResumeLayout(false);
            this.gbConfig.ResumeLayout(false);
            this.gbConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKPixelsToMm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPgaGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdcGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExposition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeightWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidthWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNSensors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSensorSettings;
        private System.Windows.Forms.Label lNSensor;
        private System.Windows.Forms.ComboBox cbNSensor;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.ComboBox cbId;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.NumericUpDown nudPgaGain;
        private System.Windows.Forms.NumericUpDown nudAdcGain;
        private System.Windows.Forms.NumericUpDown nudExposition;
        private System.Windows.Forms.NumericUpDown nudHeightWindow;
        private System.Windows.Forms.NumericUpDown nudWidthWindow;
        private System.Windows.Forms.NumericUpDown nudTopWindow;
        private System.Windows.Forms.NumericUpDown nudLeftWindow;
        private System.Windows.Forms.Label lPgaGain;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Label lAdcGain;
        private System.Windows.Forms.Label lExposition;
        private System.Windows.Forms.Label lHeightWindow;
        private System.Windows.Forms.Label lWidthWindow;
        private System.Windows.Forms.Label lTopWindow;
        private System.Windows.Forms.Label lLeftWindow;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.Label lIp;
        private System.Windows.Forms.Timer tCheckState;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.Button bApply;
        private System.Windows.Forms.Label lKMmToPixel;
        private System.Windows.Forms.NumericUpDown nudKPixelsToMm;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Label lNSensors;
        public System.Windows.Forms.NumericUpDown nudNSensors;
    }
}
