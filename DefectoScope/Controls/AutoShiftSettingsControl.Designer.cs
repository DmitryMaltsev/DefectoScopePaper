namespace DefectoScope
{
    partial class AutoShiftSettingsControl
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
            this.gbNAutoShifts = new System.Windows.Forms.GroupBox();
            this.bApply = new System.Windows.Forms.Button();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.cbSystemSettingId = new System.Windows.Forms.ComboBox();
            this.lSystemSettingId = new System.Windows.Forms.Label();
            this.lRight = new System.Windows.Forms.Label();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.lLeft = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.bAdd = new System.Windows.Forms.Button();
            this.lId = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.lNAutoShift = new System.Windows.Forms.Label();
            this.cbNAutoShift = new System.Windows.Forms.ComboBox();
            this.nudNAutoShifts = new System.Windows.Forms.NumericUpDown();
            this.lNAutoShifts = new System.Windows.Forms.Label();
            this.cbAutoShift = new System.Windows.Forms.CheckBox();
            this.bSaveG = new System.Windows.Forms.Button();
            this.gbNAutoShifts.SuspendLayout();
            this.gbConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNAutoShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // gbNAutoShifts
            // 
            this.gbNAutoShifts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNAutoShifts.Controls.Add(this.bApply);
            this.gbNAutoShifts.Controls.Add(this.gbConfig);
            this.gbNAutoShifts.Controls.Add(this.lNAutoShift);
            this.gbNAutoShifts.Controls.Add(this.cbNAutoShift);
            this.gbNAutoShifts.Location = new System.Drawing.Point(0, 57);
            this.gbNAutoShifts.Name = "gbNAutoShifts";
            this.gbNAutoShifts.Size = new System.Drawing.Size(300, 250);
            this.gbNAutoShifts.TabIndex = 2;
            this.gbNAutoShifts.TabStop = false;
            this.gbNAutoShifts.Text = "Конфигурации авто-смен";
            // 
            // bApply
            // 
            this.bApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bApply.Location = new System.Drawing.Point(6, 46);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(288, 23);
            this.bApply.TabIndex = 67;
            this.bApply.Text = "Применить текущую конфигурацию";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // gbConfig
            // 
            this.gbConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConfig.Controls.Add(this.cbSystemSettingId);
            this.gbConfig.Controls.Add(this.lSystemSettingId);
            this.gbConfig.Controls.Add(this.lRight);
            this.gbConfig.Controls.Add(this.nudRight);
            this.gbConfig.Controls.Add(this.lLeft);
            this.gbConfig.Controls.Add(this.nudLeft);
            this.gbConfig.Controls.Add(this.bAdd);
            this.gbConfig.Controls.Add(this.lId);
            this.gbConfig.Controls.Add(this.bSave);
            this.gbConfig.Controls.Add(this.pbDelete);
            this.gbConfig.Controls.Add(this.cbId);
            this.gbConfig.Location = new System.Drawing.Point(6, 90);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(288, 154);
            this.gbConfig.TabIndex = 66;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Конфигурация";
            // 
            // cbSystemSettingId
            // 
            this.cbSystemSettingId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSystemSettingId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSystemSettingId.FormatString = "N0";
            this.cbSystemSettingId.FormattingEnabled = true;
            this.cbSystemSettingId.Location = new System.Drawing.Point(162, 98);
            this.cbSystemSettingId.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbSystemSettingId.Name = "cbSystemSettingId";
            this.cbSystemSettingId.Size = new System.Drawing.Size(123, 21);
            this.cbSystemSettingId.TabIndex = 81;
            this.cbSystemSettingId.SelectedIndexChanged += new System.EventHandler(this.CbSystemSettingId_SelectedIndexChanged);
            // 
            // lSystemSettingId
            // 
            this.lSystemSettingId.Location = new System.Drawing.Point(9, 98);
            this.lSystemSettingId.Name = "lSystemSettingId";
            this.lSystemSettingId.Size = new System.Drawing.Size(147, 20);
            this.lSystemSettingId.TabIndex = 80;
            this.lSystemSettingId.Text = "Применяемый конфиг:";
            this.lSystemSettingId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lRight
            // 
            this.lRight.Location = new System.Drawing.Point(6, 70);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(150, 20);
            this.lRight.TabIndex = 71;
            this.lRight.Text = "Правая граница (г/м2):";
            this.lRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudRight
            // 
            this.nudRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudRight.DecimalPlaces = 3;
            this.nudRight.Location = new System.Drawing.Point(162, 72);
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(123, 20);
            this.nudRight.TabIndex = 72;
            this.nudRight.ValueChanged += new System.EventHandler(this.nudRight_ValueChanged);
            // 
            // lLeft
            // 
            this.lLeft.Location = new System.Drawing.Point(6, 44);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(150, 20);
            this.lLeft.TabIndex = 69;
            this.lLeft.Text = "Левая граница (г/м2):";
            this.lLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudLeft
            // 
            this.nudLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudLeft.DecimalPlaces = 3;
            this.nudLeft.Location = new System.Drawing.Point(162, 46);
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(123, 20);
            this.nudLeft.TabIndex = 70;
            this.nudLeft.ValueChanged += new System.EventHandler(this.nudLeft_ValueChanged);
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(9, 125);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(147, 23);
            this.bAdd.TabIndex = 68;
            this.bAdd.Text = "Создать";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
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
            this.bSave.Location = new System.Drawing.Point(162, 125);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(123, 23);
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
            this.pbDelete.Location = new System.Drawing.Point(269, 19);
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
            // cbId
            // 
            this.cbId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbId.Location = new System.Drawing.Point(162, 19);
            this.cbId.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(107, 21);
            this.cbId.TabIndex = 43;
            this.cbId.SelectedIndexChanged += new System.EventHandler(this.cbId_SelectedIndexChanged);
            // 
            // lNAutoShift
            // 
            this.lNAutoShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNAutoShift.Location = new System.Drawing.Point(6, 18);
            this.lNAutoShift.Name = "lNAutoShift";
            this.lNAutoShift.Size = new System.Drawing.Size(156, 20);
            this.lNAutoShift.TabIndex = 65;
            this.lNAutoShift.Text = "№ авто-смены: ";
            this.lNAutoShift.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbNAutoShift
            // 
            this.cbNAutoShift.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNAutoShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNAutoShift.FormatString = "N0";
            this.cbNAutoShift.FormattingEnabled = true;
            this.cbNAutoShift.Location = new System.Drawing.Point(168, 19);
            this.cbNAutoShift.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbNAutoShift.Name = "cbNAutoShift";
            this.cbNAutoShift.Size = new System.Drawing.Size(126, 21);
            this.cbNAutoShift.TabIndex = 63;
            this.cbNAutoShift.SelectedIndexChanged += new System.EventHandler(this.cbNAutoShift_SelectedIndexChanged);
            // 
            // nudNAutoShifts
            // 
            this.nudNAutoShifts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNAutoShifts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudNAutoShifts.Location = new System.Drawing.Point(168, 2);
            this.nudNAutoShifts.Name = "nudNAutoShifts";
            this.nudNAutoShifts.Size = new System.Drawing.Size(126, 20);
            this.nudNAutoShifts.TabIndex = 91;
            this.nudNAutoShifts.ValueChanged += new System.EventHandler(this.NudNAutoShifts_ValueChanged);
            // 
            // lNAutoShifts
            // 
            this.lNAutoShifts.Location = new System.Drawing.Point(3, 0);
            this.lNAutoShifts.Name = "lNAutoShifts";
            this.lNAutoShifts.Size = new System.Drawing.Size(159, 20);
            this.lNAutoShifts.TabIndex = 90;
            this.lNAutoShifts.Text = "Количество авто-смен:";
            this.lNAutoShifts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbAutoShift
            // 
            this.cbAutoShift.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbAutoShift.Location = new System.Drawing.Point(3, 28);
            this.cbAutoShift.Name = "cbAutoShift";
            this.cbAutoShift.Size = new System.Drawing.Size(291, 22);
            this.cbAutoShift.TabIndex = 92;
            this.cbAutoShift.Text = "Авто-смена конфигураций";
            this.cbAutoShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbAutoShift.UseVisualStyleBackColor = true;
            this.cbAutoShift.CheckedChanged += new System.EventHandler(this.CbAutoShift_CheckedChanged);
            // 
            // bSaveG
            // 
            this.bSaveG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSaveG.Location = new System.Drawing.Point(3, 313);
            this.bSaveG.Name = "bSaveG";
            this.bSaveG.Size = new System.Drawing.Size(291, 23);
            this.bSaveG.TabIndex = 93;
            this.bSaveG.Text = "Сохранить";
            this.bSaveG.UseVisualStyleBackColor = true;
            this.bSaveG.Click += new System.EventHandler(this.BSaveG_Click);
            // 
            // AutoShiftSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bSaveG);
            this.Controls.Add(this.cbAutoShift);
            this.Controls.Add(this.nudNAutoShifts);
            this.Controls.Add(this.lNAutoShifts);
            this.Controls.Add(this.gbNAutoShifts);
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(300, 280);
            this.Name = "AutoShiftSettingsControl";
            this.Size = new System.Drawing.Size(300, 339);
            this.Load += new System.EventHandler(this.AutoShiftSettingsControl_Load);
            this.gbNAutoShifts.ResumeLayout(false);
            this.gbConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNAutoShifts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNAutoShifts;
        private System.Windows.Forms.Label lNAutoShift;
        private System.Windows.Forms.ComboBox cbNAutoShift;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.ComboBox cbId;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.Button bApply;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Label lLeft;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.Label lRight;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.Label lSystemSettingId;
        private System.Windows.Forms.ComboBox cbSystemSettingId;
        public System.Windows.Forms.NumericUpDown nudNAutoShifts;
        private System.Windows.Forms.Label lNAutoShifts;
        private System.Windows.Forms.CheckBox cbAutoShift;
        private System.Windows.Forms.Button bSaveG;
    }
}
