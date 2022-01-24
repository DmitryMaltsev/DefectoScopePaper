namespace DefectoScope
{
    partial class TypeDefectSettingsControl
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
            this.gbNTypeDefectSettings = new System.Windows.Forms.GroupBox();
            this.bApply = new System.Windows.Forms.Button();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.lMarker = new System.Windows.Forms.Label();
            this.cbMarker = new System.Windows.Forms.ComboBox();
            this.cbLevel = new System.Windows.Forms.CheckBox();
            this.lLevel = new System.Windows.Forms.Label();
            this.lLength = new System.Windows.Forms.Label();
            this.lWidth = new System.Windows.Forms.Label();
            this.lSquare = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.lMarkerColor = new System.Windows.Forms.Label();
            this.bMarkerColor = new System.Windows.Forms.Button();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.nudSquare = new System.Windows.Forms.NumericUpDown();
            this.bAdd = new System.Windows.Forms.Button();
            this.lId = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.lNTypeDefect = new System.Windows.Forms.Label();
            this.cbNTypeDefect = new System.Windows.Forms.ComboBox();
            this.nudNTypeDefects = new System.Windows.Forms.NumericUpDown();
            this.lNTypeDefects = new System.Windows.Forms.Label();
            this.gbNTypeDefectSettings.SuspendLayout();
            this.gbConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSquare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNTypeDefects)).BeginInit();
            this.SuspendLayout();
            // 
            // gbNTypeDefectSettings
            // 
            this.gbNTypeDefectSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNTypeDefectSettings.Controls.Add(this.bApply);
            this.gbNTypeDefectSettings.Controls.Add(this.gbConfig);
            this.gbNTypeDefectSettings.Controls.Add(this.lNTypeDefect);
            this.gbNTypeDefectSettings.Controls.Add(this.cbNTypeDefect);
            this.gbNTypeDefectSettings.Location = new System.Drawing.Point(3, 30);
            this.gbNTypeDefectSettings.Name = "gbNTypeDefectSettings";
            this.gbNTypeDefectSettings.Size = new System.Drawing.Size(294, 320);
            this.gbNTypeDefectSettings.TabIndex = 2;
            this.gbNTypeDefectSettings.TabStop = false;
            this.gbNTypeDefectSettings.Text = "Конфигурации типов дефектов";
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
            this.gbConfig.Controls.Add(this.lMarker);
            this.gbConfig.Controls.Add(this.cbMarker);
            this.gbConfig.Controls.Add(this.cbLevel);
            this.gbConfig.Controls.Add(this.lLevel);
            this.gbConfig.Controls.Add(this.lLength);
            this.gbConfig.Controls.Add(this.lWidth);
            this.gbConfig.Controls.Add(this.lSquare);
            this.gbConfig.Controls.Add(this.nudWidth);
            this.gbConfig.Controls.Add(this.lMarkerColor);
            this.gbConfig.Controls.Add(this.bMarkerColor);
            this.gbConfig.Controls.Add(this.nudLength);
            this.gbConfig.Controls.Add(this.nudSquare);
            this.gbConfig.Controls.Add(this.bAdd);
            this.gbConfig.Controls.Add(this.lId);
            this.gbConfig.Controls.Add(this.bSave);
            this.gbConfig.Controls.Add(this.pbDelete);
            this.gbConfig.Controls.Add(this.cbId);
            this.gbConfig.Location = new System.Drawing.Point(6, 81);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(282, 233);
            this.gbConfig.TabIndex = 66;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Конфигурация";
            // 
            // lMarker
            // 
            this.lMarker.Location = new System.Drawing.Point(6, 149);
            this.lMarker.Name = "lMarker";
            this.lMarker.Size = new System.Drawing.Size(150, 20);
            this.lMarker.TabIndex = 89;
            this.lMarker.Text = "Тип маркера:";
            this.lMarker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbMarker
            // 
            this.cbMarker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMarker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMarker.FormatString = "N0";
            this.cbMarker.FormattingEnabled = true;
            this.cbMarker.Location = new System.Drawing.Point(162, 150);
            this.cbMarker.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbMarker.Name = "cbMarker";
            this.cbMarker.Size = new System.Drawing.Size(117, 21);
            this.cbMarker.TabIndex = 88;
            this.cbMarker.SelectedIndexChanged += new System.EventHandler(this.cbMarker_SelectedIndexChanged);
            // 
            // cbLevel
            // 
            this.cbLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLevel.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbLevel.Checked = true;
            this.cbLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLevel.Location = new System.Drawing.Point(162, 46);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(117, 20);
            this.cbLevel.TabIndex = 87;
            this.cbLevel.Text = "Светлый";
            this.cbLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbLevel.UseVisualStyleBackColor = true;
            this.cbLevel.CheckedChanged += new System.EventHandler(this.cbLevel_CheckedChanged);
            // 
            // lLevel
            // 
            this.lLevel.Location = new System.Drawing.Point(6, 46);
            this.lLevel.Name = "lLevel";
            this.lLevel.Size = new System.Drawing.Size(150, 20);
            this.lLevel.TabIndex = 86;
            this.lLevel.Text = "Уровень:";
            this.lLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lLength
            // 
            this.lLength.Location = new System.Drawing.Point(6, 96);
            this.lLength.Name = "lLength";
            this.lLength.Size = new System.Drawing.Size(150, 20);
            this.lLength.TabIndex = 85;
            this.lLength.Text = "Длина (мм):";
            this.lLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lWidth
            // 
            this.lWidth.Location = new System.Drawing.Point(6, 122);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(150, 20);
            this.lWidth.TabIndex = 84;
            this.lWidth.Text = "Ширина (мм):";
            this.lWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lSquare
            // 
            this.lSquare.Location = new System.Drawing.Point(6, 70);
            this.lSquare.Name = "lSquare";
            this.lSquare.Size = new System.Drawing.Size(150, 20);
            this.lSquare.TabIndex = 83;
            this.lSquare.Text = "Площадь (мм2):";
            this.lSquare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudWidth
            // 
            this.nudWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudWidth.DecimalPlaces = 3;
            this.nudWidth.Location = new System.Drawing.Point(162, 124);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(117, 20);
            this.nudWidth.TabIndex = 81;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // lMarkerColor
            // 
            this.lMarkerColor.Location = new System.Drawing.Point(6, 177);
            this.lMarkerColor.Name = "lMarkerColor";
            this.lMarkerColor.Size = new System.Drawing.Size(150, 20);
            this.lMarkerColor.TabIndex = 80;
            this.lMarkerColor.Text = "Цвет маркера:";
            this.lMarkerColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bMarkerColor
            // 
            this.bMarkerColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bMarkerColor.Location = new System.Drawing.Point(162, 177);
            this.bMarkerColor.Name = "bMarkerColor";
            this.bMarkerColor.Size = new System.Drawing.Size(117, 20);
            this.bMarkerColor.TabIndex = 79;
            this.bMarkerColor.UseVisualStyleBackColor = true;
            this.bMarkerColor.BackColorChanged += new System.EventHandler(this.bFillColor_BackColorChanged);
            this.bMarkerColor.Click += new System.EventHandler(this.bMarkerColor_Click);
            // 
            // nudLength
            // 
            this.nudLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudLength.DecimalPlaces = 3;
            this.nudLength.Location = new System.Drawing.Point(162, 98);
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(117, 20);
            this.nudLength.TabIndex = 72;
            this.nudLength.ValueChanged += new System.EventHandler(this.nudLength_ValueChanged);
            // 
            // nudSquare
            // 
            this.nudSquare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSquare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudSquare.DecimalPlaces = 3;
            this.nudSquare.Location = new System.Drawing.Point(162, 72);
            this.nudSquare.Name = "nudSquare";
            this.nudSquare.Size = new System.Drawing.Size(117, 20);
            this.nudSquare.TabIndex = 70;
            this.nudSquare.ValueChanged += new System.EventHandler(this.nudSquare_ValueChanged);
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(9, 204);
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
            this.bSave.Location = new System.Drawing.Point(162, 204);
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
            // lNTypeDefect
            // 
            this.lNTypeDefect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNTypeDefect.Location = new System.Drawing.Point(6, 18);
            this.lNTypeDefect.Name = "lNTypeDefect";
            this.lNTypeDefect.Size = new System.Drawing.Size(156, 20);
            this.lNTypeDefect.TabIndex = 65;
            this.lNTypeDefect.Text = "№ типа дефекта: ";
            this.lNTypeDefect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbNTypeDefect
            // 
            this.cbNTypeDefect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNTypeDefect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNTypeDefect.FormatString = "N0";
            this.cbNTypeDefect.FormattingEnabled = true;
            this.cbNTypeDefect.Location = new System.Drawing.Point(168, 19);
            this.cbNTypeDefect.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbNTypeDefect.Name = "cbNTypeDefect";
            this.cbNTypeDefect.Size = new System.Drawing.Size(120, 21);
            this.cbNTypeDefect.TabIndex = 63;
            this.cbNTypeDefect.SelectedIndexChanged += new System.EventHandler(this.cbNTypeDefect_SelectedIndexChanged);
            // 
            // nudNTypeDefects
            // 
            this.nudNTypeDefects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNTypeDefects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudNTypeDefects.Location = new System.Drawing.Point(171, 3);
            this.nudNTypeDefects.Name = "nudNTypeDefects";
            this.nudNTypeDefects.Size = new System.Drawing.Size(126, 20);
            this.nudNTypeDefects.TabIndex = 58;
            // 
            // lNTypeDefects
            // 
            this.lNTypeDefects.Location = new System.Drawing.Point(3, 3);
            this.lNTypeDefects.Name = "lNTypeDefects";
            this.lNTypeDefects.Size = new System.Drawing.Size(162, 20);
            this.lNTypeDefects.TabIndex = 57;
            this.lNTypeDefects.Text = "Количество зон:";
            this.lNTypeDefects.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TypeDefectSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudNTypeDefects);
            this.Controls.Add(this.lNTypeDefects);
            this.Controls.Add(this.gbNTypeDefectSettings);
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "TypeDefectSettingsControl";
            this.Size = new System.Drawing.Size(300, 350);
            this.gbNTypeDefectSettings.ResumeLayout(false);
            this.gbConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSquare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNTypeDefects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNTypeDefectSettings;
        private System.Windows.Forms.Label lNTypeDefect;
        private System.Windows.Forms.ComboBox cbNTypeDefect;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.ComboBox cbId;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.Button bApply;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.NumericUpDown nudSquare;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label lMarkerColor;
        private System.Windows.Forms.Button bMarkerColor;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label lLength;
        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.Label lSquare;
        private System.Windows.Forms.CheckBox cbLevel;
        private System.Windows.Forms.Label lLevel;
        private System.Windows.Forms.Label lMarker;
        private System.Windows.Forms.ComboBox cbMarker;
        public System.Windows.Forms.NumericUpDown nudNTypeDefects;
        private System.Windows.Forms.Label lNTypeDefects;
    }
}

