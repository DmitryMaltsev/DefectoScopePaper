using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DefectoScope.Properties;

namespace DefectoScope
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private IContainer components = null;

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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ms = new System.Windows.Forms.MenuStrip();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAutoShifts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCalibration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiManualMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatistic = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiController = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.bErrorForm = new System.Windows.Forms.Button();
            this.lSystemOk = new System.Windows.Forms.Label();
            this.bStop = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.dgvDefects = new System.Windows.Forms.DataGridView();
            this.bTambourChange = new System.Windows.Forms.Button();
            this.bCreateReport = new System.Windows.Forms.Button();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.bResetAlarm = new System.Windows.Forms.Button();
            this.bClearLog = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.defectsMap = new DefectoScope.DefectsMap();
            this.defectZoom = new DefectoScope.DefectZoom();
            this.ms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defectZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // ms
            // 
            this.ms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings,
            this.tsmiService,
            this.tsmiAboutProgram});
            this.ms.Location = new System.Drawing.Point(0, 0);
            this.ms.Name = "ms";
            this.ms.Size = new System.Drawing.Size(784, 24);
            this.ms.TabIndex = 23;
            this.ms.Text = "Меню";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystemSettings,
            this.tsmiAutoShifts,
            this.tsmiCalibration});
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(73, 20);
            this.tsmiSettings.Text = "Настройки";
            // 
            // tsmiSystemSettings
            // 
            this.tsmiSystemSettings.Name = "tsmiSystemSettings";
            this.tsmiSystemSettings.Size = new System.Drawing.Size(135, 22);
            this.tsmiSystemSettings.Text = "Программы";
            this.tsmiSystemSettings.Click += new System.EventHandler(this.tsmiSystemSettings_Click);
            // 
            // tsmiAutoShifts
            // 
            this.tsmiAutoShifts.Name = "tsmiAutoShifts";
            this.tsmiAutoShifts.Size = new System.Drawing.Size(135, 22);
            this.tsmiAutoShifts.Text = "Авто-смен";
            this.tsmiAutoShifts.Click += new System.EventHandler(this.TsmiAutoShifts_Click);
            // 
            // tsmiCalibration
            // 
            this.tsmiCalibration.Name = "tsmiCalibration";
            this.tsmiCalibration.Size = new System.Drawing.Size(135, 22);
            this.tsmiCalibration.Text = "Калибровка";
            this.tsmiCalibration.Click += new System.EventHandler(this.tsmiCalibration_Click);
            // 
            // tsmiService
            // 
            this.tsmiService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiManualMode,
            this.tsmiStatistic,
            this.tsmiTime,
            this.tsmiController,
            this.tsmiBuffer,
            this.tsmiTest,
            this.tsmiLoadFrame});
            this.tsmiService.Name = "tsmiService";
            this.tsmiService.Size = new System.Drawing.Size(55, 20);
            this.tsmiService.Text = "Сервис";
            // 
            // tsmiManualMode
            // 
            this.tsmiManualMode.Name = "tsmiManualMode";
            this.tsmiManualMode.Size = new System.Drawing.Size(154, 22);
            this.tsmiManualMode.Text = "Ручная работа";
            this.tsmiManualMode.Click += new System.EventHandler(this.tsmiManualMode_Click);
            // 
            // tsmiStatistic
            // 
            this.tsmiStatistic.Name = "tsmiStatistic";
            this.tsmiStatistic.Size = new System.Drawing.Size(154, 22);
            this.tsmiStatistic.Text = "Статистика";
            this.tsmiStatistic.Click += new System.EventHandler(this.tsmiStatistic_Click);
            // 
            // tsmiTime
            // 
            this.tsmiTime.Name = "tsmiTime";
            this.tsmiTime.Size = new System.Drawing.Size(154, 22);
            this.tsmiTime.Text = "Время";
            this.tsmiTime.Click += new System.EventHandler(this.tsmiTime_Click);
            // 
            // tsmiController
            // 
            this.tsmiController.Name = "tsmiController";
            this.tsmiController.Size = new System.Drawing.Size(154, 22);
            this.tsmiController.Text = "Контроллер";
            this.tsmiController.Click += new System.EventHandler(this.tsmiController_Click);
            // 
            // tsmiBuffer
            // 
            this.tsmiBuffer.Name = "tsmiBuffer";
            this.tsmiBuffer.Size = new System.Drawing.Size(154, 22);
            this.tsmiBuffer.Text = "Буфер строк";
            this.tsmiBuffer.Click += new System.EventHandler(this.tsmiBuffer_Click);
            // 
            // tsmiTest
            // 
            this.tsmiTest.Name = "tsmiTest";
            this.tsmiTest.Size = new System.Drawing.Size(154, 22);
            this.tsmiTest.Text = "Тест";
            this.tsmiTest.Visible = false;
            this.tsmiTest.Click += new System.EventHandler(this.tsmiTest_Click);
            // 
            // tsmiLoadFrame
            // 
            this.tsmiLoadFrame.Name = "tsmiLoadFrame";
            this.tsmiLoadFrame.Size = new System.Drawing.Size(154, 22);
            this.tsmiLoadFrame.Text = "Загрузка кадра";
            this.tsmiLoadFrame.Click += new System.EventHandler(this.tsmiLoadFrame_Click);
            // 
            // tsmiAboutProgram
            // 
            this.tsmiAboutProgram.Name = "tsmiAboutProgram";
            this.tsmiAboutProgram.Size = new System.Drawing.Size(83, 20);
            this.tsmiAboutProgram.Text = "О программе";
            this.tsmiAboutProgram.Click += new System.EventHandler(this.tsmiAboutProgram_Click);
            // 
            // bErrorForm
            // 
            this.bErrorForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bErrorForm.Enabled = false;
            this.bErrorForm.Location = new System.Drawing.Point(622, 474);
            this.bErrorForm.Name = "bErrorForm";
            this.bErrorForm.Size = new System.Drawing.Size(150, 23);
            this.bErrorForm.TabIndex = 25;
            this.bErrorForm.Text = "Ошибки системы";
            this.bErrorForm.UseVisualStyleBackColor = true;
            this.bErrorForm.Click += new System.EventHandler(this.bErrorForm_Click);
            // 
            // lSystemOk
            // 
            this.lSystemOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lSystemOk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lSystemOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lSystemOk.Location = new System.Drawing.Point(622, 428);
            this.lSystemOk.Margin = new System.Windows.Forms.Padding(3);
            this.lSystemOk.Name = "lSystemOk";
            this.lSystemOk.Size = new System.Drawing.Size(150, 40);
            this.lSystemOk.TabIndex = 26;
            this.lSystemOk.Text = "Исправность системы не проверялась";
            this.lSystemOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bStop.BackColor = System.Drawing.Color.OrangeRed;
            this.bStop.Enabled = false;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStop.ForeColor = System.Drawing.Color.Black;
            this.bStop.Location = new System.Drawing.Point(622, 530);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(150, 23);
            this.bStop.TabIndex = 27;
            this.bStop.Text = "Останов работы";
            this.bStop.UseVisualStyleBackColor = false;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bStart
            // 
            this.bStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStart.ForeColor = System.Drawing.Color.Green;
            this.bStart.Location = new System.Drawing.Point(466, 530);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(150, 23);
            this.bStart.TabIndex = 28;
            this.bStart.Text = "Запуск работы";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // dgvDefects
            // 
            this.dgvDefects.AllowUserToAddRows = false;
            this.dgvDefects.AllowUserToDeleteRows = false;
            this.dgvDefects.AllowUserToResizeColumns = false;
            this.dgvDefects.AllowUserToResizeRows = false;
            this.dgvDefects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDefects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDefects.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDefects.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDefects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefects.Location = new System.Drawing.Point(12, 428);
            this.dgvDefects.MultiSelect = false;
            this.dgvDefects.Name = "dgvDefects";
            this.dgvDefects.ReadOnly = true;
            this.dgvDefects.Size = new System.Drawing.Size(448, 125);
            this.dgvDefects.StandardTab = true;
            this.dgvDefects.TabIndex = 33;
            this.dgvDefects.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefects_CellClick);
            this.dgvDefects.CurrentCellChanged += new System.EventHandler(this.dgvDefects_CurrentCellChanged);
            this.dgvDefects.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDefects_RowPostPaint);
            // 
            // bTambourChange
            // 
            this.bTambourChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bTambourChange.Location = new System.Drawing.Point(466, 501);
            this.bTambourChange.Name = "bTambourChange";
            this.bTambourChange.Size = new System.Drawing.Size(150, 23);
            this.bTambourChange.TabIndex = 34;
            this.bTambourChange.Text = "Смена тамбура";
            this.bTambourChange.UseVisualStyleBackColor = true;
            this.bTambourChange.Click += new System.EventHandler(this.bTambourChange_Click);
            // 
            // bCreateReport
            // 
            this.bCreateReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCreateReport.Location = new System.Drawing.Point(622, 501);
            this.bCreateReport.Name = "bCreateReport";
            this.bCreateReport.Size = new System.Drawing.Size(150, 23);
            this.bCreateReport.TabIndex = 34;
            this.bCreateReport.Text = "Создание отчета";
            this.bCreateReport.UseVisualStyleBackColor = true;
            this.bCreateReport.Click += new System.EventHandler(this.bCreateReport_Click);
            // 
            // cbId
            // 
            this.cbId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbId.Location = new System.Drawing.Point(466, 474);
            this.cbId.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(150, 21);
            this.cbId.TabIndex = 79;
            // 
            // bResetAlarm
            // 
            this.bResetAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bResetAlarm.Location = new System.Drawing.Point(622, 399);
            this.bResetAlarm.Name = "bResetAlarm";
            this.bResetAlarm.Size = new System.Drawing.Size(150, 23);
            this.bResetAlarm.TabIndex = 80;
            this.bResetAlarm.Text = "Сброс тревоги";
            this.bResetAlarm.UseVisualStyleBackColor = true;
            this.bResetAlarm.Click += new System.EventHandler(this.BResetAlarm_Click);
            // 
            // bClearLog
            // 
            this.bClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bClearLog.Location = new System.Drawing.Point(466, 399);
            this.bClearLog.Name = "bClearLog";
            this.bClearLog.Size = new System.Drawing.Size(150, 23);
            this.bClearLog.TabIndex = 81;
            this.bClearLog.Text = "Очистка истории";
            this.bClearLog.UseVisualStyleBackColor = true;
            this.bClearLog.Click += new System.EventHandler(this.BClearLog_Click);
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(466, 27);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(306, 368);
            this.lbLog.TabIndex = 82;
            // 
            // defectsMap
            // 
            this.defectsMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defectsMap.Location = new System.Drawing.Point(12, 27);
            this.defectsMap.Name = "defectsMap";
            this.defectsMap.Size = new System.Drawing.Size(448, 395);
            this.defectsMap.TabIndex = 32;
            // 
            // defectZoom
            // 
            this.defectZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.defectZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.defectZoom.Location = new System.Drawing.Point(466, 428);
            this.defectZoom.Name = "defectZoom";
            this.defectZoom.Size = new System.Drawing.Size(150, 40);
            this.defectZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.defectZoom.TabIndex = 31;
            this.defectZoom.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.bResetAlarm);
            this.Controls.Add(this.bClearLog);
            this.Controls.Add(this.cbId);
            this.Controls.Add(this.bCreateReport);
            this.Controls.Add(this.bTambourChange);
            this.Controls.Add(this.dgvDefects);
            this.Controls.Add(this.defectsMap);
            this.Controls.Add(this.defectZoom);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.lSystemOk);
            this.Controls.Add(this.bErrorForm);
            this.Controls.Add(this.ms);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "DefectoScope";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.ms.ResumeLayout(false);
            this.ms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defectZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip ms;
        private ToolStripMenuItem tsmiSettings;
        private ToolStripMenuItem tsmiSystemSettings;
        private Button bErrorForm;
        private Label lSystemOk;
        private ToolStripMenuItem tsmiAboutProgram;
        private Button bStop;
        private Button bStart;
        private ToolStripMenuItem tsmiService;
        private ToolStripMenuItem tsmiStatistic;
        private ToolStripMenuItem tsmiController;
        private ToolStripMenuItem tsmiManualMode;
        private ToolStripMenuItem tsmiBuffer;
        private ToolStripMenuItem tsmiCalibration;
        private DefectZoom defectZoom;
        private ToolStripMenuItem tsmiTest;
        private ToolStripMenuItem tsmiTime;
        private DefectsMap defectsMap;
        private ToolStripMenuItem tsmiLoadFrame;
        private DataGridView dgvDefects;
        private Button bTambourChange;
        private Button bCreateReport;
        private ComboBox cbId;
        private ToolStripMenuItem tsmiAutoShifts;
        private Button bResetAlarm;
        private Button bClearLog;
        private ListBox lbLog;
    }
}

