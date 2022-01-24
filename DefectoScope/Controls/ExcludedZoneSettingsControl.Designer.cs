namespace DefectoScope
{
    partial class ExcludedZoneSettingsControl
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
            this.gbNExcludedZoneSettings = new System.Windows.Forms.GroupBox();
            this.bApply = new System.Windows.Forms.Button();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.lFillColor = new System.Windows.Forms.Label();
            this.bFillColor = new System.Windows.Forms.Button();
            this.lRight = new System.Windows.Forms.Label();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.lLeft = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.bAdd = new System.Windows.Forms.Button();
            this.lId = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.lNExcludedZone = new System.Windows.Forms.Label();
            this.cbNExcludedZone = new System.Windows.Forms.ComboBox();
            this.nudNExcludedZones = new System.Windows.Forms.NumericUpDown();
            this.lNExcludedZones = new System.Windows.Forms.Label();
            this.gbNExcludedZoneSettings.SuspendLayout();
            this.gbConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNExcludedZones)).BeginInit();
            this.SuspendLayout();
            // 
            // gbNExcludedZoneSettings
            // 
            this.gbNExcludedZoneSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNExcludedZoneSettings.Controls.Add(this.bApply);
            this.gbNExcludedZoneSettings.Controls.Add(this.gbConfig);
            this.gbNExcludedZoneSettings.Controls.Add(this.lNExcludedZone);
            this.gbNExcludedZoneSettings.Controls.Add(this.cbNExcludedZone);
            this.gbNExcludedZoneSettings.Location = new System.Drawing.Point(3, 30);
            this.gbNExcludedZoneSettings.Name = "gbNExcludedZoneSettings";
            this.gbNExcludedZoneSettings.Size = new System.Drawing.Size(294, 250);
            this.gbNExcludedZoneSettings.TabIndex = 2;
            this.gbNExcludedZoneSettings.TabStop = false;
            this.gbNExcludedZoneSettings.Text = "Конфигурации исключаемых зон";
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
            this.gbConfig.Controls.Add(this.lFillColor);
            this.gbConfig.Controls.Add(this.bFillColor);
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
            this.gbConfig.Size = new System.Drawing.Size(282, 154);
            this.gbConfig.TabIndex = 66;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Конфигурация";
            // 
            // lFillColor
            // 
            this.lFillColor.Location = new System.Drawing.Point(9, 98);
            this.lFillColor.Name = "lFillColor";
            this.lFillColor.Size = new System.Drawing.Size(147, 20);
            this.lFillColor.TabIndex = 80;
            this.lFillColor.Text = "Цвет зоны:";
            this.lFillColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bFillColor
            // 
            this.bFillColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bFillColor.Location = new System.Drawing.Point(162, 98);
            this.bFillColor.Name = "bFillColor";
            this.bFillColor.Size = new System.Drawing.Size(117, 20);
            this.bFillColor.TabIndex = 79;
            this.bFillColor.UseVisualStyleBackColor = true;
            this.bFillColor.BackColorChanged += new System.EventHandler(this.bFillColor_BackColorChanged);
            this.bFillColor.Click += new System.EventHandler(this.bFillColor_Click);
            // 
            // lRight
            // 
            this.lRight.Location = new System.Drawing.Point(6, 70);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(150, 20);
            this.lRight.TabIndex = 71;
            this.lRight.Text = "Правая граница (мм):";
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
            this.nudRight.Size = new System.Drawing.Size(117, 20);
            this.nudRight.TabIndex = 72;
            this.nudRight.ValueChanged += new System.EventHandler(this.nudRight_ValueChanged);
            // 
            // lLeft
            // 
            this.lLeft.Location = new System.Drawing.Point(6, 44);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(150, 20);
            this.lLeft.TabIndex = 69;
            this.lLeft.Text = "Левая граница (мм):";
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
            this.nudLeft.Size = new System.Drawing.Size(117, 20);
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
            // lNExcludedZone
            // 
            this.lNExcludedZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNExcludedZone.Location = new System.Drawing.Point(6, 18);
            this.lNExcludedZone.Name = "lNExcludedZone";
            this.lNExcludedZone.Size = new System.Drawing.Size(156, 20);
            this.lNExcludedZone.TabIndex = 65;
            this.lNExcludedZone.Text = "№ исключаемой зоны: ";
            this.lNExcludedZone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbNExcludedZone
            // 
            this.cbNExcludedZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNExcludedZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNExcludedZone.FormatString = "N0";
            this.cbNExcludedZone.FormattingEnabled = true;
            this.cbNExcludedZone.Location = new System.Drawing.Point(168, 19);
            this.cbNExcludedZone.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbNExcludedZone.Name = "cbNExcludedZone";
            this.cbNExcludedZone.Size = new System.Drawing.Size(120, 21);
            this.cbNExcludedZone.TabIndex = 63;
            this.cbNExcludedZone.SelectedIndexChanged += new System.EventHandler(this.cbNExcludedZone_SelectedIndexChanged);
            // 
            // nudNExcludedZones
            // 
            this.nudNExcludedZones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNExcludedZones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudNExcludedZones.Location = new System.Drawing.Point(171, 3);
            this.nudNExcludedZones.Name = "nudNExcludedZones";
            this.nudNExcludedZones.Size = new System.Drawing.Size(126, 20);
            this.nudNExcludedZones.TabIndex = 56;
            // 
            // lNExcludedZones
            // 
            this.lNExcludedZones.Location = new System.Drawing.Point(6, 3);
            this.lNExcludedZones.Name = "lNExcludedZones";
            this.lNExcludedZones.Size = new System.Drawing.Size(159, 20);
            this.lNExcludedZones.TabIndex = 55;
            this.lNExcludedZones.Text = "Количество зон:";
            this.lNExcludedZones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ExcludedZoneSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudNExcludedZones);
            this.Controls.Add(this.lNExcludedZones);
            this.Controls.Add(this.gbNExcludedZoneSettings);
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(300, 280);
            this.Name = "ExcludedZoneSettingsControl";
            this.Size = new System.Drawing.Size(300, 280);
            this.gbNExcludedZoneSettings.ResumeLayout(false);
            this.gbConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNExcludedZones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNExcludedZoneSettings;
        private System.Windows.Forms.Label lNExcludedZone;
        private System.Windows.Forms.ComboBox cbNExcludedZone;
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
        private System.Windows.Forms.Label lFillColor;
        private System.Windows.Forms.Button bFillColor;
        private System.Windows.Forms.Label lNExcludedZones;
        public System.Windows.Forms.NumericUpDown nudNExcludedZones;
    }
}
