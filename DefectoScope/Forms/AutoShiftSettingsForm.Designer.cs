namespace DefectoScope
{
    sealed partial class AutoShiftSettingsForm
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
            this.autoShiftSettingsControl1 = new DefectoScope.AutoShiftSettingsControl();
            this.SuspendLayout();
            // 
            // autoShiftSettingsControl1
            // 
            this.autoShiftSettingsControl1.Location = new System.Drawing.Point(12, 12);
            this.autoShiftSettingsControl1.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.autoShiftSettingsControl1.MinimumSize = new System.Drawing.Size(300, 280);
            this.autoShiftSettingsControl1.Name = "autoShiftSettingsControl1";
            this.autoShiftSettingsControl1.Size = new System.Drawing.Size(310, 338);
            this.autoShiftSettingsControl1.TabIndex = 0;
            // 
            // AutoShiftSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(334, 362);
            this.Controls.Add(this.autoShiftSettingsControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoShiftSettingsForm";
            this.Text = "Настройки программы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoShiftSettingsForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoShiftSettingsForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private AutoShiftSettingsControl autoShiftSettingsControl1;
    }
}