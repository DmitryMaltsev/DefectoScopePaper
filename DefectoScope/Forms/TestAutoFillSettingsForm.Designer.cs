namespace DefectoScope
{
    partial class TestAutoFillSettingsForm
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
            this.bOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bOpen
            // 
            this.bOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOpen.Location = new System.Drawing.Point(0, 0);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(268, 107);
            this.bOpen.TabIndex = 0;
            this.bOpen.Text = "Открыть файл";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.BOpen_Click);
            // 
            // TestAutoFillSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 107);
            this.Controls.Add(this.bOpen);
            this.Name = "TestAutoFillSettingsForm";
            this.Text = "TestAutoFillSettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOpen;
    }
}