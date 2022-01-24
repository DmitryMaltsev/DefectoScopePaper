namespace DefectoScope
{
    partial class ProfileBufferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileBufferForm));
            this._rowBufferMap = new DefectoScope.RowBufferMap();
            ((System.ComponentModel.ISupportInitialize)(this._rowBufferMap)).BeginInit();
            this.SuspendLayout();
            // 
            // _rowBufferMap
            // 
            this._rowBufferMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rowBufferMap.Image = global::DefectoScope.Properties.Resources.background;
            this._rowBufferMap.Location = new System.Drawing.Point(0, 0);
            this._rowBufferMap.Name = "_rowBufferMap";
            this._rowBufferMap.Size = new System.Drawing.Size(584, 261);
            this._rowBufferMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._rowBufferMap.TabIndex = 0;
            this._rowBufferMap.TabStop = false;
            this._rowBufferMap.SizeChanged += new System.EventHandler(this.rowsBufferMap_SizeChanged);
            // 
            // ProfileBufferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this._rowBufferMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileBufferForm";
            this.Text = "Окно буфера строк";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RowsBufferForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._rowBufferMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RowBufferMap _rowBufferMap;
    }
}