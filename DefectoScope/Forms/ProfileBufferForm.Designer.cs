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
            this._profileBufferMap = new DefectoScope.ProfileBufferMap();
            ((System.ComponentModel.ISupportInitialize)(this._profileBufferMap)).BeginInit();
            this.SuspendLayout();
            // 
            // _profileBufferMap
            // 
            this._profileBufferMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this._profileBufferMap.Location = new System.Drawing.Point(0, 0);
            this._profileBufferMap.Name = "_profileBufferMap";
            this._profileBufferMap.Size = new System.Drawing.Size(584, 261);
            this._profileBufferMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._profileBufferMap.TabIndex = 0;
            this._profileBufferMap.TabStop = false;
            this._profileBufferMap.SizeChanged += new System.EventHandler(this.profileBufferMap_SizeChanged);
            // 
            // ProfileBufferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this._profileBufferMap);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileBufferForm";
            this.ShowIcon = false;
            this.Text = "Окно буфера профилей";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RowsBufferForm_FormClosing);
            this.Load += new System.EventHandler(this.ProfileBufferForm_Load);
            this.ResizeBegin += new System.EventHandler(this.ProfileBufferForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ProfileBufferForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this._profileBufferMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ProfileBufferMap _profileBufferMap;
    }
}