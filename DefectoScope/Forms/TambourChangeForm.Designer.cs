namespace DefectoScope
{
    partial class TambourChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TambourChangeForm));
            this.lNTambour = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.cbNShift = new System.Windows.Forms.CheckBox();
            this.nudNTambour = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudNTambour)).BeginInit();
            this.SuspendLayout();
            // 
            // lNTambour
            // 
            this.lNTambour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lNTambour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNTambour.Location = new System.Drawing.Point(12, 9);
            this.lNTambour.Name = "lNTambour";
            this.lNTambour.Size = new System.Drawing.Size(160, 23);
            this.lNTambour.TabIndex = 3;
            this.lNTambour.Text = "Номер тамбура";
            this.lNTambour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(97, 92);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 6;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.BCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Location = new System.Drawing.Point(12, 92);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 5;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.BOK_Click);
            // 
            // cbNShift
            // 
            this.cbNShift.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNShift.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbNShift.Location = new System.Drawing.Point(12, 61);
            this.cbNShift.Name = "cbNShift";
            this.cbNShift.Size = new System.Drawing.Size(160, 24);
            this.cbNShift.TabIndex = 7;
            this.cbNShift.Text = "Пусковой";
            this.cbNShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbNShift.UseVisualStyleBackColor = true;
            this.cbNShift.CheckedChanged += new System.EventHandler(this.cbNShift_CheckedChanged);
            // 
            // nudNTambour
            // 
            this.nudNTambour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNTambour.Location = new System.Drawing.Point(15, 35);
            this.nudNTambour.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudNTambour.Name = "nudNTambour";
            this.nudNTambour.Size = new System.Drawing.Size(157, 20);
            this.nudNTambour.TabIndex = 8;
            this.nudNTambour.ValueChanged += new System.EventHandler(this.nudNTambour_ValueChanged);
            // 
            // TambourChangeForm
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(184, 127);
            this.Controls.Add(this.nudNTambour);
            this.Controls.Add(this.cbNShift);
            this.Controls.Add(this.lNTambour);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "TambourChangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Смена тамбура";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TambourChangeForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudNTambour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lNTambour;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.CheckBox cbNShift;
        private System.Windows.Forms.NumericUpDown nudNTambour;
    }
}