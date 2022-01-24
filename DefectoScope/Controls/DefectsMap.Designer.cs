
namespace DefectoScope
{
    partial class DefectsMap
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
            this.gbDefectsMap = new System.Windows.Forms.GroupBox();
            this._markerLine = new Kogerent.HLine();
            this.tlpDefectsMap = new System.Windows.Forms.TableLayoutPanel();
            this.lStart = new System.Windows.Forms.Label();
            this.gbDefectsMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDefectsMap
            // 
            this.gbDefectsMap.Controls.Add(this.lStart);
            this.gbDefectsMap.Controls.Add(this._markerLine);
            this.gbDefectsMap.Controls.Add(this.tlpDefectsMap);
            this.gbDefectsMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDefectsMap.Location = new System.Drawing.Point(0, 0);
            this.gbDefectsMap.Name = "gbDefectsMap";
            this.gbDefectsMap.Size = new System.Drawing.Size(391, 300);
            this.gbDefectsMap.TabIndex = 0;
            this.gbDefectsMap.TabStop = false;
            this.gbDefectsMap.Text = "Карта дефектов";
            // 
            // _markerLine
            // 
            this._markerLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._markerLine.BackColor = System.Drawing.Color.Black;
            this._markerLine.Enabled = false;
            this._markerLine.LineColor = System.Drawing.Color.Black;
            this._markerLine.LineHeight = 10;
            this._markerLine.Location = new System.Drawing.Point(60, 25);
            this._markerLine.Name = "_markerLine";
            this._markerLine.Size = new System.Drawing.Size(324, 10);
            this._markerLine.TabIndex = 1;
            // 
            // tlpDefectsMap
            // 
            this.tlpDefectsMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpDefectsMap.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpDefectsMap.ColumnCount = 1;
            this.tlpDefectsMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDefectsMap.Location = new System.Drawing.Point(60, 25);
            this.tlpDefectsMap.Name = "tlpDefectsMap";
            this.tlpDefectsMap.RowCount = 1;
            this.tlpDefectsMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDefectsMap.Size = new System.Drawing.Size(324, 249);
            this.tlpDefectsMap.TabIndex = 0;
            this.tlpDefectsMap.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tlpDefectsMap_CellPaint);
            this.tlpDefectsMap.SizeChanged += new System.EventHandler(this.tlpDefectsMap_SizeChanged);
            this.tlpDefectsMap.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpDefectsMap_Paint);
            // 
            // lStart
            // 
            this.lStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lStart.AutoSize = true;
            this.lStart.Location = new System.Drawing.Point(9, 277);
            this.lStart.Name = "lStart";
            this.lStart.Size = new System.Drawing.Size(45, 13);
            this.lStart.TabIndex = 2;
            this.lStart.Text = "Привод";
            // 
            // DefectsMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbDefectsMap);
            this.DoubleBuffered = true;
            this.Name = "DefectsMap";
            this.Size = new System.Drawing.Size(391, 300);
            this.gbDefectsMap.ResumeLayout(false);
            this.gbDefectsMap.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDefectsMap;
        private System.Windows.Forms.TableLayoutPanel tlpDefectsMap;
        private Kogerent.HLine _markerLine;
        private System.Windows.Forms.Label lStart;
    }
}
