namespace DefectoScope
{
    partial class DynamicSensorControl
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
            this.lId = new System.Windows.Forms.Label();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.bSave = new System.Windows.Forms.Button();
            this.lPgaGain = new System.Windows.Forms.Label();
            this.nudPgaGain = new System.Windows.Forms.NumericUpDown();
            this.nudAdcGain = new System.Windows.Forms.NumericUpDown();
            this.lAdcGain = new System.Windows.Forms.Label();
            this.nudExposition = new System.Windows.Forms.NumericUpDown();
            this.lExposition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPgaGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdcGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExposition)).BeginInit();
            this.SuspendLayout();
            // 
            // lId
            // 
            this.lId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lId.Location = new System.Drawing.Point(-3, 0);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(150, 20);
            this.lId.TabIndex = 77;
            this.lId.Text = "Имя конфигурации:";
            this.lId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbId
            // 
            this.cbId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbId.Location = new System.Drawing.Point(153, 1);
            this.cbId.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(97, 21);
            this.cbId.TabIndex = 78;
            this.cbId.SelectedIndexChanged += new System.EventHandler(this.cbId_SelectedIndexChanged);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(0, 107);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(250, 23);
            this.bSave.TabIndex = 76;
            this.bSave.Text = "Сохранить и применить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // lPgaGain
            // 
            this.lPgaGain.Location = new System.Drawing.Point(0, 78);
            this.lPgaGain.Name = "lPgaGain";
            this.lPgaGain.Size = new System.Drawing.Size(147, 20);
            this.lPgaGain.TabIndex = 70;
            this.lPgaGain.Text = "Аналоговое усиление:";
            this.lPgaGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudPgaGain
            // 
            this.nudPgaGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPgaGain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPgaGain.Location = new System.Drawing.Point(153, 80);
            this.nudPgaGain.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudPgaGain.Name = "nudPgaGain";
            this.nudPgaGain.Size = new System.Drawing.Size(97, 20);
            this.nudPgaGain.TabIndex = 74;
            // 
            // nudAdcGain
            // 
            this.nudAdcGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudAdcGain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudAdcGain.Location = new System.Drawing.Point(153, 54);
            this.nudAdcGain.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAdcGain.Name = "nudAdcGain";
            this.nudAdcGain.Size = new System.Drawing.Size(97, 20);
            this.nudAdcGain.TabIndex = 75;
            // 
            // lAdcGain
            // 
            this.lAdcGain.Location = new System.Drawing.Point(0, 52);
            this.lAdcGain.Name = "lAdcGain";
            this.lAdcGain.Size = new System.Drawing.Size(147, 20);
            this.lAdcGain.TabIndex = 71;
            this.lAdcGain.Text = "Цифровое усиление:";
            this.lAdcGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudExposition
            // 
            this.nudExposition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudExposition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudExposition.Location = new System.Drawing.Point(153, 28);
            this.nudExposition.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudExposition.Name = "nudExposition";
            this.nudExposition.Size = new System.Drawing.Size(97, 20);
            this.nudExposition.TabIndex = 73;
            // 
            // lExposition
            // 
            this.lExposition.Location = new System.Drawing.Point(3, 26);
            this.lExposition.Name = "lExposition";
            this.lExposition.Size = new System.Drawing.Size(147, 20);
            this.lExposition.TabIndex = 72;
            this.lExposition.Text = "Время экспозиции:";
            this.lExposition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DynamicSensorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lId);
            this.Controls.Add(this.cbId);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.lPgaGain);
            this.Controls.Add(this.nudPgaGain);
            this.Controls.Add(this.nudAdcGain);
            this.Controls.Add(this.lAdcGain);
            this.Controls.Add(this.nudExposition);
            this.Controls.Add(this.lExposition);
            this.MinimumSize = new System.Drawing.Size(250, 130);
            this.Name = "DynamicSensorControl";
            this.Size = new System.Drawing.Size(250, 130);
            this.Load += new System.EventHandler(this.DynamicSensorControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPgaGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdcGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExposition)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.ComboBox cbId;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label lPgaGain;
        private System.Windows.Forms.NumericUpDown nudPgaGain;
        private System.Windows.Forms.NumericUpDown nudAdcGain;
        private System.Windows.Forms.Label lAdcGain;
        private System.Windows.Forms.NumericUpDown nudExposition;
        private System.Windows.Forms.Label lExposition;
    }
}
