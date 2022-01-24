namespace DefectoScope
{
    partial class SystemSettingsControl
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
            this.pSystemSettings = new System.Windows.Forms.Panel();
            this.tc = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudMarkerLinePosition = new System.Windows.Forms.NumericUpDown();
            this.cbMarkerLine = new System.Windows.Forms.CheckBox();
            this.lNMapColumns = new System.Windows.Forms.Label();
            this.nudNMapColumns = new System.Windows.Forms.NumericUpDown();
            this.lNMapRows = new System.Windows.Forms.Label();
            this.nudNMapRows = new System.Windows.Forms.NumericUpDown();
            this.lMapRange = new System.Windows.Forms.Label();
            this.nudMapRange = new System.Windows.Forms.NumericUpDown();
            this.gbCalibration = new System.Windows.Forms.GroupBox();
            this.lNCalibrationProfiles = new System.Windows.Forms.Label();
            this.nudNCalibrationProfiles = new System.Windows.Forms.NumericUpDown();
            this.cbCalibrationFileName = new System.Windows.Forms.ComboBox();
            this.lCalibrationFileName = new System.Windows.Forms.Label();
            this.lToleranceLight = new System.Windows.Forms.Label();
            this.nudToleranceLight = new System.Windows.Forms.NumericUpDown();
            this.lToleranceDark = new System.Windows.Forms.Label();
            this.nudToleranceDark = new System.Windows.Forms.NumericUpDown();
            this.gbController = new System.Windows.Forms.GroupBox();
            this.nudStrobeEveryNMm = new System.Windows.Forms.NumericUpDown();
            this.lStrobeEveryNMm = new System.Windows.Forms.Label();
            this.lEncoderDivider = new System.Windows.Forms.Label();
            this.nudEncoderDivider = new System.Windows.Forms.NumericUpDown();
            this.mtbComPortName = new System.Windows.Forms.MaskedTextBox();
            this.lComPortName = new System.Windows.Forms.Label();
            this.nudAlarmDuration = new System.Windows.Forms.NumericUpDown();
            this.cbAlarm = new System.Windows.Forms.CheckBox();
            this.tpOther = new System.Windows.Forms.TabPage();
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.cbInitTestOpenDB = new System.Windows.Forms.CheckBox();
            this.tbSqlConnectionString = new System.Windows.Forms.TextBox();
            this.lSqlConnectionString = new System.Windows.Forms.Label();
            this.cbWriteInDatabase = new System.Windows.Forms.CheckBox();
            this.gbOther = new System.Windows.Forms.GroupBox();
            this.cbCheckAutoBorders = new System.Windows.Forms.CheckBox();
            this.cbCalibrationWithoutDefects = new System.Windows.Forms.CheckBox();
            this.cbAutoBorders = new System.Windows.Forms.CheckBox();
            this.cbLabelsInMm = new System.Windows.Forms.CheckBox();
            this.cbZoom = new System.Windows.Forms.CheckBox();
            this.cbDebugMode = new System.Windows.Forms.CheckBox();
            this.gbProfileBufferF = new System.Windows.Forms.GroupBox();
            this.nudNScaleLineDivisions = new System.Windows.Forms.NumericUpDown();
            this.cbScaleLine = new System.Windows.Forms.CheckBox();
            this.gbBufferSize = new System.Windows.Forms.GroupBox();
            this.lProfileBufferSize = new System.Windows.Forms.Label();
            this.nudProfileBufferSize = new System.Windows.Forms.NumericUpDown();
            this.lInputBufferSize = new System.Windows.Forms.Label();
            this.nudInputBufferSize = new System.Windows.Forms.NumericUpDown();
            this.gbLogging = new System.Windows.Forms.GroupBox();
            this.clbLogger = new System.Windows.Forms.CheckedListBox();
            this.tpVisual = new System.Windows.Forms.TabPage();
            this.gbDefectsMap = new System.Windows.Forms.GroupBox();
            this.lMarkerSize = new System.Windows.Forms.Label();
            this.nudMarkerSize = new System.Windows.Forms.NumericUpDown();
            this.lMarkerLineWidth = new System.Windows.Forms.Label();
            this.nudMarkerLineWidth = new System.Windows.Forms.NumericUpDown();
            this.lMarkerLineColor = new System.Windows.Forms.Label();
            this.bMarkerLineColor = new System.Windows.Forms.Button();
            this.gbCalibrationF = new System.Windows.Forms.GroupBox();
            this.lProfileColor = new System.Windows.Forms.Label();
            this.bProfileColor = new System.Windows.Forms.Button();
            this.lToleranceLightColor = new System.Windows.Forms.Label();
            this.bToleranceLightColor = new System.Windows.Forms.Button();
            this.lToleranceDarkColor = new System.Windows.Forms.Label();
            this.bToleranceDarkColor = new System.Windows.Forms.Button();
            this.lCalibrationDataColor = new System.Windows.Forms.Label();
            this.bCalibrationDataColor = new System.Windows.Forms.Button();
            this.gbVProfileBufferF = new System.Windows.Forms.GroupBox();
            this.lNoiseColor = new System.Windows.Forms.Label();
            this.bNoiseColor = new System.Windows.Forms.Button();
            this.lScaleLineWidth = new System.Windows.Forms.Label();
            this.nudScaleLineWidth = new System.Windows.Forms.NumericUpDown();
            this.lScaleLineColor = new System.Windows.Forms.Label();
            this.bScaleLineColor = new System.Windows.Forms.Button();
            this.lLightAreaColor = new System.Windows.Forms.Label();
            this.bLightAreaColor = new System.Windows.Forms.Button();
            this.lDarkAreaColor = new System.Windows.Forms.Label();
            this.bDarkAreaColor = new System.Windows.Forms.Button();
            this.tpSensors = new System.Windows.Forms.TabPage();
            this.tpExcludedZones = new System.Windows.Forms.TabPage();
            this.tpTypeDefects = new System.Windows.Forms.TabPage();
            this.bAdd = new System.Windows.Forms.Button();
            this.lId = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.bApply = new System.Windows.Forms.Button();
            this.pbDelete = new System.Windows.Forms.PictureBox();
            this.cbAutoCreateReport = new System.Windows.Forms.CheckBox();
            this.pSystemSettings.SuspendLayout();
            this.tc.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkerLinePosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNMapColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNMapRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapRange)).BeginInit();
            this.gbCalibration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNCalibrationProfiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToleranceLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToleranceDark)).BeginInit();
            this.gbController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrobeEveryNMm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEncoderDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmDuration)).BeginInit();
            this.tpOther.SuspendLayout();
            this.gbDatabase.SuspendLayout();
            this.gbOther.SuspendLayout();
            this.gbProfileBufferF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNScaleLineDivisions)).BeginInit();
            this.gbBufferSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProfileBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputBufferSize)).BeginInit();
            this.gbLogging.SuspendLayout();
            this.tpVisual.SuspendLayout();
            this.gbDefectsMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkerSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkerLineWidth)).BeginInit();
            this.gbCalibrationF.SuspendLayout();
            this.gbVProfileBufferF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleLineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // pSystemSettings
            // 
            this.pSystemSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pSystemSettings.AutoSize = true;
            this.pSystemSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pSystemSettings.Controls.Add(this.tc);
            this.pSystemSettings.Location = new System.Drawing.Point(3, 59);
            this.pSystemSettings.Name = "pSystemSettings";
            this.pSystemSettings.Size = new System.Drawing.Size(392, 426);
            this.pSystemSettings.TabIndex = 82;
            // 
            // tc
            // 
            this.tc.Controls.Add(this.tpMain);
            this.tc.Controls.Add(this.tpOther);
            this.tc.Controls.Add(this.tpVisual);
            this.tc.Controls.Add(this.tpSensors);
            this.tc.Controls.Add(this.tpExcludedZones);
            this.tc.Controls.Add(this.tpTypeDefects);
            this.tc.Location = new System.Drawing.Point(3, 3);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(384, 418);
            this.tc.TabIndex = 69;
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.groupBox1);
            this.tpMain.Controls.Add(this.gbCalibration);
            this.tpMain.Controls.Add(this.gbController);
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(376, 392);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Основное";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudMarkerLinePosition);
            this.groupBox1.Controls.Add(this.cbMarkerLine);
            this.groupBox1.Controls.Add(this.lNMapColumns);
            this.groupBox1.Controls.Add(this.nudNMapColumns);
            this.groupBox1.Controls.Add(this.lNMapRows);
            this.groupBox1.Controls.Add(this.nudNMapRows);
            this.groupBox1.Controls.Add(this.lMapRange);
            this.groupBox1.Controls.Add(this.nudMapRange);
            this.groupBox1.Location = new System.Drawing.Point(6, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 124);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Карта дефектов";
            // 
            // nudMarkerLinePosition
            // 
            this.nudMarkerLinePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMarkerLinePosition.DecimalPlaces = 3;
            this.nudMarkerLinePosition.Location = new System.Drawing.Point(172, 45);
            this.nudMarkerLinePosition.Name = "nudMarkerLinePosition";
            this.nudMarkerLinePosition.Size = new System.Drawing.Size(187, 20);
            this.nudMarkerLinePosition.TabIndex = 94;
            this.nudMarkerLinePosition.ValueChanged += new System.EventHandler(this.nudMarkerLinePosition_ValueChanged);
            // 
            // cbMarkerLine
            // 
            this.cbMarkerLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbMarkerLine.Location = new System.Drawing.Point(6, 42);
            this.cbMarkerLine.Name = "cbMarkerLine";
            this.cbMarkerLine.Size = new System.Drawing.Size(160, 23);
            this.cbMarkerLine.TabIndex = 93;
            this.cbMarkerLine.Text = "Сигнальная линия (мм):";
            this.cbMarkerLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbMarkerLine.UseVisualStyleBackColor = true;
            this.cbMarkerLine.CheckedChanged += new System.EventHandler(this.cbMarkerLine_CheckedChanged);
            // 
            // lNMapColumns
            // 
            this.lNMapColumns.Location = new System.Drawing.Point(6, 95);
            this.lNMapColumns.Name = "lNMapColumns";
            this.lNMapColumns.Size = new System.Drawing.Size(160, 20);
            this.lNMapColumns.TabIndex = 92;
            this.lNMapColumns.Text = "Количество столбцов:";
            this.lNMapColumns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudNMapColumns
            // 
            this.nudNMapColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNMapColumns.Location = new System.Drawing.Point(172, 97);
            this.nudNMapColumns.Name = "nudNMapColumns";
            this.nudNMapColumns.Size = new System.Drawing.Size(186, 20);
            this.nudNMapColumns.TabIndex = 91;
            this.nudNMapColumns.ValueChanged += new System.EventHandler(this.nudNMapColumns_ValueChanged);
            // 
            // lNMapRows
            // 
            this.lNMapRows.Location = new System.Drawing.Point(6, 69);
            this.lNMapRows.Name = "lNMapRows";
            this.lNMapRows.Size = new System.Drawing.Size(160, 20);
            this.lNMapRows.TabIndex = 88;
            this.lNMapRows.Text = "Количество строк:";
            this.lNMapRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudNMapRows
            // 
            this.nudNMapRows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNMapRows.Location = new System.Drawing.Point(172, 71);
            this.nudNMapRows.Name = "nudNMapRows";
            this.nudNMapRows.Size = new System.Drawing.Size(186, 20);
            this.nudNMapRows.TabIndex = 87;
            this.nudNMapRows.ValueChanged += new System.EventHandler(this.nudNMapRows_ValueChanged);
            // 
            // lMapRange
            // 
            this.lMapRange.Location = new System.Drawing.Point(6, 17);
            this.lMapRange.Name = "lMapRange";
            this.lMapRange.Size = new System.Drawing.Size(160, 20);
            this.lMapRange.TabIndex = 86;
            this.lMapRange.Text = "Дальность карты (мм)";
            this.lMapRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudMapRange
            // 
            this.nudMapRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMapRange.DecimalPlaces = 3;
            this.nudMapRange.Location = new System.Drawing.Point(172, 19);
            this.nudMapRange.Name = "nudMapRange";
            this.nudMapRange.Size = new System.Drawing.Size(186, 20);
            this.nudMapRange.TabIndex = 85;
            this.nudMapRange.ValueChanged += new System.EventHandler(this.nudMapRange_ValueChanged);
            // 
            // gbCalibration
            // 
            this.gbCalibration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalibration.Controls.Add(this.lNCalibrationProfiles);
            this.gbCalibration.Controls.Add(this.nudNCalibrationProfiles);
            this.gbCalibration.Controls.Add(this.cbCalibrationFileName);
            this.gbCalibration.Controls.Add(this.lCalibrationFileName);
            this.gbCalibration.Controls.Add(this.lToleranceLight);
            this.gbCalibration.Controls.Add(this.nudToleranceLight);
            this.gbCalibration.Controls.Add(this.lToleranceDark);
            this.gbCalibration.Controls.Add(this.nudToleranceDark);
            this.gbCalibration.Location = new System.Drawing.Point(6, 135);
            this.gbCalibration.Name = "gbCalibration";
            this.gbCalibration.Size = new System.Drawing.Size(364, 121);
            this.gbCalibration.TabIndex = 4;
            this.gbCalibration.TabStop = false;
            this.gbCalibration.Text = "Калибровка";
            // 
            // lNCalibrationProfiles
            // 
            this.lNCalibrationProfiles.Location = new System.Drawing.Point(6, 94);
            this.lNCalibrationProfiles.Name = "lNCalibrationProfiles";
            this.lNCalibrationProfiles.Size = new System.Drawing.Size(160, 20);
            this.lNCalibrationProfiles.TabIndex = 92;
            this.lNCalibrationProfiles.Text = "Калибровочных профилей:";
            this.lNCalibrationProfiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudNCalibrationProfiles
            // 
            this.nudNCalibrationProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNCalibrationProfiles.Location = new System.Drawing.Point(172, 96);
            this.nudNCalibrationProfiles.Name = "nudNCalibrationProfiles";
            this.nudNCalibrationProfiles.Size = new System.Drawing.Size(186, 20);
            this.nudNCalibrationProfiles.TabIndex = 91;
            this.nudNCalibrationProfiles.ValueChanged += new System.EventHandler(this.nudNCalibrationProfiles_ValueChanged);
            // 
            // cbCalibrationFileName
            // 
            this.cbCalibrationFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCalibrationFileName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalibrationFileName.Location = new System.Drawing.Point(172, 17);
            this.cbCalibrationFileName.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbCalibrationFileName.Name = "cbCalibrationFileName";
            this.cbCalibrationFileName.Size = new System.Drawing.Size(186, 21);
            this.cbCalibrationFileName.TabIndex = 90;
            this.cbCalibrationFileName.SelectedIndexChanged += new System.EventHandler(this.cbCalibrationFileName_SelectedIndexChanged);
            // 
            // lCalibrationFileName
            // 
            this.lCalibrationFileName.Location = new System.Drawing.Point(6, 16);
            this.lCalibrationFileName.Name = "lCalibrationFileName";
            this.lCalibrationFileName.Size = new System.Drawing.Size(160, 20);
            this.lCalibrationFileName.TabIndex = 89;
            this.lCalibrationFileName.Text = "Калибровочный файл:";
            this.lCalibrationFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lToleranceLight
            // 
            this.lToleranceLight.Location = new System.Drawing.Point(6, 68);
            this.lToleranceLight.Name = "lToleranceLight";
            this.lToleranceLight.Size = new System.Drawing.Size(160, 20);
            this.lToleranceLight.TabIndex = 88;
            this.lToleranceLight.Text = "Допуск светлого (%):";
            this.lToleranceLight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudToleranceLight
            // 
            this.nudToleranceLight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudToleranceLight.Location = new System.Drawing.Point(172, 70);
            this.nudToleranceLight.Name = "nudToleranceLight";
            this.nudToleranceLight.Size = new System.Drawing.Size(186, 20);
            this.nudToleranceLight.TabIndex = 87;
            this.nudToleranceLight.ValueChanged += new System.EventHandler(this.nudToleranceLight_ValueChanged);
            // 
            // lToleranceDark
            // 
            this.lToleranceDark.Location = new System.Drawing.Point(6, 42);
            this.lToleranceDark.Name = "lToleranceDark";
            this.lToleranceDark.Size = new System.Drawing.Size(160, 20);
            this.lToleranceDark.TabIndex = 86;
            this.lToleranceDark.Text = "Допуск темного (%):";
            this.lToleranceDark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudToleranceDark
            // 
            this.nudToleranceDark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudToleranceDark.Location = new System.Drawing.Point(172, 44);
            this.nudToleranceDark.Name = "nudToleranceDark";
            this.nudToleranceDark.Size = new System.Drawing.Size(186, 20);
            this.nudToleranceDark.TabIndex = 85;
            this.nudToleranceDark.ValueChanged += new System.EventHandler(this.nudToleranceDark_ValueChanged);
            // 
            // gbController
            // 
            this.gbController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbController.Controls.Add(this.nudStrobeEveryNMm);
            this.gbController.Controls.Add(this.lStrobeEveryNMm);
            this.gbController.Controls.Add(this.lEncoderDivider);
            this.gbController.Controls.Add(this.nudEncoderDivider);
            this.gbController.Controls.Add(this.mtbComPortName);
            this.gbController.Controls.Add(this.lComPortName);
            this.gbController.Controls.Add(this.nudAlarmDuration);
            this.gbController.Controls.Add(this.cbAlarm);
            this.gbController.Location = new System.Drawing.Point(6, 6);
            this.gbController.Name = "gbController";
            this.gbController.Size = new System.Drawing.Size(364, 123);
            this.gbController.TabIndex = 2;
            this.gbController.TabStop = false;
            this.gbController.Text = "Контроллер";
            // 
            // nudStrobeEveryNMm
            // 
            this.nudStrobeEveryNMm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudStrobeEveryNMm.DecimalPlaces = 6;
            this.nudStrobeEveryNMm.Location = new System.Drawing.Point(172, 99);
            this.nudStrobeEveryNMm.Name = "nudStrobeEveryNMm";
            this.nudStrobeEveryNMm.Size = new System.Drawing.Size(186, 20);
            this.nudStrobeEveryNMm.TabIndex = 93;
            this.nudStrobeEveryNMm.ValueChanged += new System.EventHandler(this.NudStrobeEveryNMm_ValueChanged);
            // 
            // lStrobeEveryNMm
            // 
            this.lStrobeEveryNMm.Location = new System.Drawing.Point(6, 97);
            this.lStrobeEveryNMm.Name = "lStrobeEveryNMm";
            this.lStrobeEveryNMm.Size = new System.Drawing.Size(160, 20);
            this.lStrobeEveryNMm.TabIndex = 92;
            this.lStrobeEveryNMm.Text = "Стробов на миллиметр:";
            this.lStrobeEveryNMm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lEncoderDivider
            // 
            this.lEncoderDivider.Location = new System.Drawing.Point(6, 71);
            this.lEncoderDivider.Name = "lEncoderDivider";
            this.lEncoderDivider.Size = new System.Drawing.Size(160, 20);
            this.lEncoderDivider.TabIndex = 90;
            this.lEncoderDivider.Text = "Делитель энкодера:";
            this.lEncoderDivider.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudEncoderDivider
            // 
            this.nudEncoderDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudEncoderDivider.Location = new System.Drawing.Point(172, 73);
            this.nudEncoderDivider.Name = "nudEncoderDivider";
            this.nudEncoderDivider.Size = new System.Drawing.Size(186, 20);
            this.nudEncoderDivider.TabIndex = 89;
            this.nudEncoderDivider.ValueChanged += new System.EventHandler(this.NudEncoderDivider_ValueChanged);
            // 
            // mtbComPortName
            // 
            this.mtbComPortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mtbComPortName.Location = new System.Drawing.Point(172, 18);
            this.mtbComPortName.Mask = "\\C\\OM000";
            this.mtbComPortName.Name = "mtbComPortName";
            this.mtbComPortName.PromptChar = ' ';
            this.mtbComPortName.Size = new System.Drawing.Size(186, 20);
            this.mtbComPortName.TabIndex = 86;
            this.mtbComPortName.TextChanged += new System.EventHandler(this.mtbComPortName_TextChanged);
            // 
            // lComPortName
            // 
            this.lComPortName.Location = new System.Drawing.Point(6, 17);
            this.lComPortName.Name = "lComPortName";
            this.lComPortName.Size = new System.Drawing.Size(160, 20);
            this.lComPortName.TabIndex = 85;
            this.lComPortName.Text = "Имя COM-порта:";
            this.lComPortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudAlarmDuration
            // 
            this.nudAlarmDuration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudAlarmDuration.Location = new System.Drawing.Point(172, 47);
            this.nudAlarmDuration.Name = "nudAlarmDuration";
            this.nudAlarmDuration.Size = new System.Drawing.Size(186, 20);
            this.nudAlarmDuration.TabIndex = 2;
            this.nudAlarmDuration.ValueChanged += new System.EventHandler(this.nudAlarmDuration_ValueChanged);
            // 
            // cbAlarm
            // 
            this.cbAlarm.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbAlarm.Location = new System.Drawing.Point(9, 44);
            this.cbAlarm.Name = "cbAlarm";
            this.cbAlarm.Size = new System.Drawing.Size(160, 23);
            this.cbAlarm.TabIndex = 0;
            this.cbAlarm.Text = "Сигнализация (сек):";
            this.cbAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbAlarm.UseVisualStyleBackColor = true;
            this.cbAlarm.CheckedChanged += new System.EventHandler(this.cbAlarm_CheckedChanged);
            // 
            // tpOther
            // 
            this.tpOther.Controls.Add(this.gbDatabase);
            this.tpOther.Controls.Add(this.gbOther);
            this.tpOther.Controls.Add(this.gbProfileBufferF);
            this.tpOther.Controls.Add(this.gbBufferSize);
            this.tpOther.Controls.Add(this.gbLogging);
            this.tpOther.Location = new System.Drawing.Point(4, 22);
            this.tpOther.Name = "tpOther";
            this.tpOther.Size = new System.Drawing.Size(376, 392);
            this.tpOther.TabIndex = 4;
            this.tpOther.Text = "Прочее";
            this.tpOther.UseVisualStyleBackColor = true;
            // 
            // gbDatabase
            // 
            this.gbDatabase.Controls.Add(this.cbInitTestOpenDB);
            this.gbDatabase.Controls.Add(this.tbSqlConnectionString);
            this.gbDatabase.Controls.Add(this.lSqlConnectionString);
            this.gbDatabase.Controls.Add(this.cbWriteInDatabase);
            this.gbDatabase.Location = new System.Drawing.Point(6, 280);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.Size = new System.Drawing.Size(367, 69);
            this.gbDatabase.TabIndex = 9;
            this.gbDatabase.TabStop = false;
            this.gbDatabase.Text = "База данных";
            // 
            // cbInitTestOpenDB
            // 
            this.cbInitTestOpenDB.AutoSize = true;
            this.cbInitTestOpenDB.Location = new System.Drawing.Point(6, 48);
            this.cbInitTestOpenDB.Name = "cbInitTestOpenDB";
            this.cbInitTestOpenDB.Size = new System.Drawing.Size(134, 17);
            this.cbInitTestOpenDB.TabIndex = 98;
            this.cbInitTestOpenDB.Text = "Проверка при старте";
            this.cbInitTestOpenDB.UseVisualStyleBackColor = true;
            this.cbInitTestOpenDB.CheckedChanged += new System.EventHandler(this.CbInitTestOpenDB_CheckedChanged);
            // 
            // tbSqlConnectionString
            // 
            this.tbSqlConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSqlConnectionString.Location = new System.Drawing.Point(172, 16);
            this.tbSqlConnectionString.Name = "tbSqlConnectionString";
            this.tbSqlConnectionString.Size = new System.Drawing.Size(190, 20);
            this.tbSqlConnectionString.TabIndex = 97;
            this.tbSqlConnectionString.TextChanged += new System.EventHandler(this.tbSqlConnectionString_TextChanged);
            // 
            // lSqlConnectionString
            // 
            this.lSqlConnectionString.Location = new System.Drawing.Point(6, 16);
            this.lSqlConnectionString.Name = "lSqlConnectionString";
            this.lSqlConnectionString.Size = new System.Drawing.Size(160, 20);
            this.lSqlConnectionString.TabIndex = 96;
            this.lSqlConnectionString.Text = "Строка соединения:";
            this.lSqlConnectionString.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbWriteInDatabase
            // 
            this.cbWriteInDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWriteInDatabase.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbWriteInDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbWriteInDatabase.Location = new System.Drawing.Point(172, 42);
            this.cbWriteInDatabase.Name = "cbWriteInDatabase";
            this.cbWriteInDatabase.Size = new System.Drawing.Size(190, 23);
            this.cbWriteInDatabase.TabIndex = 94;
            this.cbWriteInDatabase.Text = "Запись в базу данных";
            this.cbWriteInDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbWriteInDatabase.UseVisualStyleBackColor = true;
            this.cbWriteInDatabase.CheckedChanged += new System.EventHandler(this.cbWriteInDatabase_CheckedChanged);
            // 
            // gbOther
            // 
            this.gbOther.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOther.Controls.Add(this.cbAutoCreateReport);
            this.gbOther.Controls.Add(this.cbCheckAutoBorders);
            this.gbOther.Controls.Add(this.cbCalibrationWithoutDefects);
            this.gbOther.Controls.Add(this.cbAutoBorders);
            this.gbOther.Controls.Add(this.cbLabelsInMm);
            this.gbOther.Controls.Add(this.cbZoom);
            this.gbOther.Controls.Add(this.cbDebugMode);
            this.gbOther.Location = new System.Drawing.Point(178, 131);
            this.gbOther.Name = "gbOther";
            this.gbOther.Size = new System.Drawing.Size(195, 143);
            this.gbOther.TabIndex = 8;
            this.gbOther.TabStop = false;
            this.gbOther.Text = "Прочее";
            // 
            // cbCheckAutoBorders
            // 
            this.cbCheckAutoBorders.AutoSize = true;
            this.cbCheckAutoBorders.Location = new System.Drawing.Point(6, 84);
            this.cbCheckAutoBorders.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbCheckAutoBorders.Name = "cbCheckAutoBorders";
            this.cbCheckAutoBorders.Size = new System.Drawing.Size(140, 17);
            this.cbCheckAutoBorders.TabIndex = 5;
            this.cbCheckAutoBorders.Text = "Проверка авто-границ";
            this.cbCheckAutoBorders.UseVisualStyleBackColor = true;
            this.cbCheckAutoBorders.CheckedChanged += new System.EventHandler(this.CbCheckAutoBorders_CheckedChanged);
            // 
            // cbCalibrationWithoutDefects
            // 
            this.cbCalibrationWithoutDefects.AutoSize = true;
            this.cbCalibrationWithoutDefects.Location = new System.Drawing.Point(6, 101);
            this.cbCalibrationWithoutDefects.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbCalibrationWithoutDefects.Name = "cbCalibrationWithoutDefects";
            this.cbCalibrationWithoutDefects.Size = new System.Drawing.Size(181, 17);
            this.cbCalibrationWithoutDefects.TabIndex = 4;
            this.cbCalibrationWithoutDefects.Text = "Нет дефектов при калибровке";
            this.cbCalibrationWithoutDefects.UseVisualStyleBackColor = true;
            this.cbCalibrationWithoutDefects.CheckedChanged += new System.EventHandler(this.CbCalibrationWithoutDefects_CheckedChanged);
            // 
            // cbAutoBorders
            // 
            this.cbAutoBorders.AutoSize = true;
            this.cbAutoBorders.Location = new System.Drawing.Point(6, 67);
            this.cbAutoBorders.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbAutoBorders.Name = "cbAutoBorders";
            this.cbAutoBorders.Size = new System.Drawing.Size(96, 17);
            this.cbAutoBorders.TabIndex = 3;
            this.cbAutoBorders.Text = "Авто-границы";
            this.cbAutoBorders.UseVisualStyleBackColor = true;
            this.cbAutoBorders.CheckedChanged += new System.EventHandler(this.CbAutoBorders_CheckedChanged);
            // 
            // cbLabelsInMm
            // 
            this.cbLabelsInMm.AutoSize = true;
            this.cbLabelsInMm.Location = new System.Drawing.Point(6, 50);
            this.cbLabelsInMm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbLabelsInMm.Name = "cbLabelsInMm";
            this.cbLabelsInMm.Size = new System.Drawing.Size(86, 17);
            this.cbLabelsInMm.TabIndex = 2;
            this.cbLabelsInMm.Text = "Метки в мм";
            this.cbLabelsInMm.UseVisualStyleBackColor = true;
            this.cbLabelsInMm.CheckedChanged += new System.EventHandler(this.cbLabelsInMm_CheckedChanged);
            // 
            // cbZoom
            // 
            this.cbZoom.AutoSize = true;
            this.cbZoom.Location = new System.Drawing.Point(6, 33);
            this.cbZoom.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Size = new System.Drawing.Size(98, 17);
            this.cbZoom.TabIndex = 1;
            this.cbZoom.Text = "Зум дефектов";
            this.cbZoom.UseVisualStyleBackColor = true;
            this.cbZoom.CheckedChanged += new System.EventHandler(this.cbZoom_CheckedChanged);
            // 
            // cbDebugMode
            // 
            this.cbDebugMode.AutoSize = true;
            this.cbDebugMode.Location = new System.Drawing.Point(6, 16);
            this.cbDebugMode.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbDebugMode.Name = "cbDebugMode";
            this.cbDebugMode.Size = new System.Drawing.Size(125, 17);
            this.cbDebugMode.TabIndex = 0;
            this.cbDebugMode.Text = "Отладочный режим";
            this.cbDebugMode.UseVisualStyleBackColor = true;
            this.cbDebugMode.CheckedChanged += new System.EventHandler(this.cbDebugMode_CheckedChanged);
            // 
            // gbProfileBufferF
            // 
            this.gbProfileBufferF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProfileBufferF.Controls.Add(this.nudNScaleLineDivisions);
            this.gbProfileBufferF.Controls.Add(this.cbScaleLine);
            this.gbProfileBufferF.Location = new System.Drawing.Point(6, 80);
            this.gbProfileBufferF.Name = "gbProfileBufferF";
            this.gbProfileBufferF.Size = new System.Drawing.Size(367, 45);
            this.gbProfileBufferF.TabIndex = 7;
            this.gbProfileBufferF.TabStop = false;
            this.gbProfileBufferF.Text = "Буфер профилей";
            // 
            // nudNScaleLineDivisions
            // 
            this.nudNScaleLineDivisions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudNScaleLineDivisions.Location = new System.Drawing.Point(172, 19);
            this.nudNScaleLineDivisions.Name = "nudNScaleLineDivisions";
            this.nudNScaleLineDivisions.Size = new System.Drawing.Size(190, 20);
            this.nudNScaleLineDivisions.TabIndex = 94;
            this.nudNScaleLineDivisions.ValueChanged += new System.EventHandler(this.nudNScaleLineDivisions_ValueChanged);
            // 
            // cbScaleLine
            // 
            this.cbScaleLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbScaleLine.Location = new System.Drawing.Point(6, 16);
            this.cbScaleLine.Name = "cbScaleLine";
            this.cbScaleLine.Size = new System.Drawing.Size(160, 23);
            this.cbScaleLine.TabIndex = 93;
            this.cbScaleLine.Text = "Шкала, число делений:";
            this.cbScaleLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbScaleLine.UseVisualStyleBackColor = true;
            this.cbScaleLine.CheckedChanged += new System.EventHandler(this.cbScaleLine_CheckedChanged);
            // 
            // gbBufferSize
            // 
            this.gbBufferSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBufferSize.Controls.Add(this.lProfileBufferSize);
            this.gbBufferSize.Controls.Add(this.nudProfileBufferSize);
            this.gbBufferSize.Controls.Add(this.lInputBufferSize);
            this.gbBufferSize.Controls.Add(this.nudInputBufferSize);
            this.gbBufferSize.Location = new System.Drawing.Point(6, 3);
            this.gbBufferSize.Name = "gbBufferSize";
            this.gbBufferSize.Size = new System.Drawing.Size(367, 71);
            this.gbBufferSize.TabIndex = 4;
            this.gbBufferSize.TabStop = false;
            this.gbBufferSize.Text = "Размеры буферов";
            // 
            // lProfileBufferSize
            // 
            this.lProfileBufferSize.Location = new System.Drawing.Point(6, 43);
            this.lProfileBufferSize.Name = "lProfileBufferSize";
            this.lProfileBufferSize.Size = new System.Drawing.Size(160, 20);
            this.lProfileBufferSize.TabIndex = 88;
            this.lProfileBufferSize.Text = "Буфер профилей:";
            this.lProfileBufferSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudProfileBufferSize
            // 
            this.nudProfileBufferSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudProfileBufferSize.Location = new System.Drawing.Point(172, 45);
            this.nudProfileBufferSize.Name = "nudProfileBufferSize";
            this.nudProfileBufferSize.Size = new System.Drawing.Size(190, 20);
            this.nudProfileBufferSize.TabIndex = 87;
            this.nudProfileBufferSize.ValueChanged += new System.EventHandler(this.nudProfileBufferSize_ValueChanged);
            // 
            // lInputBufferSize
            // 
            this.lInputBufferSize.Location = new System.Drawing.Point(6, 17);
            this.lInputBufferSize.Name = "lInputBufferSize";
            this.lInputBufferSize.Size = new System.Drawing.Size(160, 20);
            this.lInputBufferSize.TabIndex = 86;
            this.lInputBufferSize.Text = "Входной буфер:";
            this.lInputBufferSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudInputBufferSize
            // 
            this.nudInputBufferSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudInputBufferSize.Location = new System.Drawing.Point(172, 19);
            this.nudInputBufferSize.Name = "nudInputBufferSize";
            this.nudInputBufferSize.Size = new System.Drawing.Size(190, 20);
            this.nudInputBufferSize.TabIndex = 85;
            this.nudInputBufferSize.ValueChanged += new System.EventHandler(this.nudInputBufferSize_ValueChanged);
            // 
            // gbLogging
            // 
            this.gbLogging.Controls.Add(this.clbLogger);
            this.gbLogging.Location = new System.Drawing.Point(6, 131);
            this.gbLogging.Name = "gbLogging";
            this.gbLogging.Size = new System.Drawing.Size(166, 143);
            this.gbLogging.TabIndex = 0;
            this.gbLogging.TabStop = false;
            this.gbLogging.Text = "Логирование";
            // 
            // clbLogger
            // 
            this.clbLogger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbLogger.CheckOnClick = true;
            this.clbLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbLogger.FormattingEnabled = true;
            this.clbLogger.Items.AddRange(new object[] {
            "Слежение",
            "Отладка",
            "Информация",
            "Предупреждения",
            "Ошибки",
            "Аварии"});
            this.clbLogger.Location = new System.Drawing.Point(3, 16);
            this.clbLogger.Name = "clbLogger";
            this.clbLogger.Size = new System.Drawing.Size(160, 124);
            this.clbLogger.TabIndex = 0;
            this.clbLogger.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbLogger_ItemCheck);
            // 
            // tpVisual
            // 
            this.tpVisual.Controls.Add(this.gbDefectsMap);
            this.tpVisual.Controls.Add(this.gbCalibrationF);
            this.tpVisual.Controls.Add(this.gbVProfileBufferF);
            this.tpVisual.Location = new System.Drawing.Point(4, 22);
            this.tpVisual.Name = "tpVisual";
            this.tpVisual.Padding = new System.Windows.Forms.Padding(3);
            this.tpVisual.Size = new System.Drawing.Size(376, 392);
            this.tpVisual.TabIndex = 1;
            this.tpVisual.Text = "Внешний вид";
            this.tpVisual.UseVisualStyleBackColor = true;
            // 
            // gbDefectsMap
            // 
            this.gbDefectsMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDefectsMap.Controls.Add(this.lMarkerSize);
            this.gbDefectsMap.Controls.Add(this.nudMarkerSize);
            this.gbDefectsMap.Controls.Add(this.lMarkerLineWidth);
            this.gbDefectsMap.Controls.Add(this.nudMarkerLineWidth);
            this.gbDefectsMap.Controls.Add(this.lMarkerLineColor);
            this.gbDefectsMap.Controls.Add(this.bMarkerLineColor);
            this.gbDefectsMap.Location = new System.Drawing.Point(6, 290);
            this.gbDefectsMap.Name = "gbDefectsMap";
            this.gbDefectsMap.Size = new System.Drawing.Size(367, 96);
            this.gbDefectsMap.TabIndex = 2;
            this.gbDefectsMap.TabStop = false;
            this.gbDefectsMap.Text = "Карта дефектов";
            // 
            // lMarkerSize
            // 
            this.lMarkerSize.Location = new System.Drawing.Point(6, 69);
            this.lMarkerSize.Name = "lMarkerSize";
            this.lMarkerSize.Size = new System.Drawing.Size(160, 20);
            this.lMarkerSize.TabIndex = 90;
            this.lMarkerSize.Text = "Размер маркеров:";
            this.lMarkerSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudMarkerSize
            // 
            this.nudMarkerSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMarkerSize.Location = new System.Drawing.Point(172, 71);
            this.nudMarkerSize.Name = "nudMarkerSize";
            this.nudMarkerSize.Size = new System.Drawing.Size(189, 20);
            this.nudMarkerSize.TabIndex = 89;
            this.nudMarkerSize.ValueChanged += new System.EventHandler(this.nudMarkerSize_ValueChanged);
            // 
            // lMarkerLineWidth
            // 
            this.lMarkerLineWidth.Location = new System.Drawing.Point(6, 43);
            this.lMarkerLineWidth.Name = "lMarkerLineWidth";
            this.lMarkerLineWidth.Size = new System.Drawing.Size(160, 20);
            this.lMarkerLineWidth.TabIndex = 88;
            this.lMarkerLineWidth.Text = "Ширина сигнальной линии:";
            this.lMarkerLineWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudMarkerLineWidth
            // 
            this.nudMarkerLineWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMarkerLineWidth.Location = new System.Drawing.Point(172, 45);
            this.nudMarkerLineWidth.Name = "nudMarkerLineWidth";
            this.nudMarkerLineWidth.Size = new System.Drawing.Size(189, 20);
            this.nudMarkerLineWidth.TabIndex = 87;
            this.nudMarkerLineWidth.ValueChanged += new System.EventHandler(this.nudMarkerLineWidth_ValueChanged);
            // 
            // lMarkerLineColor
            // 
            this.lMarkerLineColor.Location = new System.Drawing.Point(6, 19);
            this.lMarkerLineColor.Name = "lMarkerLineColor";
            this.lMarkerLineColor.Size = new System.Drawing.Size(160, 20);
            this.lMarkerLineColor.TabIndex = 82;
            this.lMarkerLineColor.Text = "Цвет сигнальной линии:";
            this.lMarkerLineColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bMarkerLineColor
            // 
            this.bMarkerLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bMarkerLineColor.Location = new System.Drawing.Point(172, 19);
            this.bMarkerLineColor.Name = "bMarkerLineColor";
            this.bMarkerLineColor.Size = new System.Drawing.Size(189, 20);
            this.bMarkerLineColor.TabIndex = 81;
            this.bMarkerLineColor.UseVisualStyleBackColor = true;
            this.bMarkerLineColor.BackColorChanged += new System.EventHandler(this.bMarkerLineColor_BackColorChanged);
            this.bMarkerLineColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // gbCalibrationF
            // 
            this.gbCalibrationF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalibrationF.Controls.Add(this.lProfileColor);
            this.gbCalibrationF.Controls.Add(this.bProfileColor);
            this.gbCalibrationF.Controls.Add(this.lToleranceLightColor);
            this.gbCalibrationF.Controls.Add(this.bToleranceLightColor);
            this.gbCalibrationF.Controls.Add(this.lToleranceDarkColor);
            this.gbCalibrationF.Controls.Add(this.bToleranceDarkColor);
            this.gbCalibrationF.Controls.Add(this.lCalibrationDataColor);
            this.gbCalibrationF.Controls.Add(this.bCalibrationDataColor);
            this.gbCalibrationF.Location = new System.Drawing.Point(9, 162);
            this.gbCalibrationF.Name = "gbCalibrationF";
            this.gbCalibrationF.Size = new System.Drawing.Size(367, 122);
            this.gbCalibrationF.TabIndex = 1;
            this.gbCalibrationF.TabStop = false;
            this.gbCalibrationF.Text = "Окно калибровки";
            // 
            // lProfileColor
            // 
            this.lProfileColor.Location = new System.Drawing.Point(6, 97);
            this.lProfileColor.Name = "lProfileColor";
            this.lProfileColor.Size = new System.Drawing.Size(160, 20);
            this.lProfileColor.TabIndex = 88;
            this.lProfileColor.Text = "Цвет линии профиля:";
            this.lProfileColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bProfileColor
            // 
            this.bProfileColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bProfileColor.Location = new System.Drawing.Point(172, 97);
            this.bProfileColor.Name = "bProfileColor";
            this.bProfileColor.Size = new System.Drawing.Size(189, 20);
            this.bProfileColor.TabIndex = 87;
            this.bProfileColor.UseVisualStyleBackColor = true;
            this.bProfileColor.BackColorChanged += new System.EventHandler(this.bProfileColor_BackColorChanged);
            this.bProfileColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // lToleranceLightColor
            // 
            this.lToleranceLightColor.Location = new System.Drawing.Point(6, 71);
            this.lToleranceLightColor.Name = "lToleranceLightColor";
            this.lToleranceLightColor.Size = new System.Drawing.Size(160, 20);
            this.lToleranceLightColor.TabIndex = 86;
            this.lToleranceLightColor.Text = "Цвет светлого допуска:";
            this.lToleranceLightColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bToleranceLightColor
            // 
            this.bToleranceLightColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bToleranceLightColor.Location = new System.Drawing.Point(172, 71);
            this.bToleranceLightColor.Name = "bToleranceLightColor";
            this.bToleranceLightColor.Size = new System.Drawing.Size(189, 20);
            this.bToleranceLightColor.TabIndex = 85;
            this.bToleranceLightColor.UseVisualStyleBackColor = true;
            this.bToleranceLightColor.BackColorChanged += new System.EventHandler(this.bToleranceLightColor_BackColorChanged);
            this.bToleranceLightColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // lToleranceDarkColor
            // 
            this.lToleranceDarkColor.Location = new System.Drawing.Point(6, 45);
            this.lToleranceDarkColor.Name = "lToleranceDarkColor";
            this.lToleranceDarkColor.Size = new System.Drawing.Size(160, 20);
            this.lToleranceDarkColor.TabIndex = 84;
            this.lToleranceDarkColor.Text = "Цвет темного допуска:";
            this.lToleranceDarkColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bToleranceDarkColor
            // 
            this.bToleranceDarkColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bToleranceDarkColor.Location = new System.Drawing.Point(172, 45);
            this.bToleranceDarkColor.Name = "bToleranceDarkColor";
            this.bToleranceDarkColor.Size = new System.Drawing.Size(189, 20);
            this.bToleranceDarkColor.TabIndex = 83;
            this.bToleranceDarkColor.UseVisualStyleBackColor = true;
            this.bToleranceDarkColor.BackColorChanged += new System.EventHandler(this.bToleranceDarkColor_BackColorChanged);
            this.bToleranceDarkColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // lCalibrationDataColor
            // 
            this.lCalibrationDataColor.Location = new System.Drawing.Point(6, 19);
            this.lCalibrationDataColor.Name = "lCalibrationDataColor";
            this.lCalibrationDataColor.Size = new System.Drawing.Size(160, 20);
            this.lCalibrationDataColor.TabIndex = 82;
            this.lCalibrationDataColor.Text = "Цвет калибровочной линии:";
            this.lCalibrationDataColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bCalibrationDataColor
            // 
            this.bCalibrationDataColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCalibrationDataColor.Location = new System.Drawing.Point(172, 19);
            this.bCalibrationDataColor.Name = "bCalibrationDataColor";
            this.bCalibrationDataColor.Size = new System.Drawing.Size(189, 20);
            this.bCalibrationDataColor.TabIndex = 81;
            this.bCalibrationDataColor.UseVisualStyleBackColor = true;
            this.bCalibrationDataColor.BackColorChanged += new System.EventHandler(this.bCalibrationDataColor_BackColorChanged);
            this.bCalibrationDataColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // gbVProfileBufferF
            // 
            this.gbVProfileBufferF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbVProfileBufferF.Controls.Add(this.lNoiseColor);
            this.gbVProfileBufferF.Controls.Add(this.bNoiseColor);
            this.gbVProfileBufferF.Controls.Add(this.lScaleLineWidth);
            this.gbVProfileBufferF.Controls.Add(this.nudScaleLineWidth);
            this.gbVProfileBufferF.Controls.Add(this.lScaleLineColor);
            this.gbVProfileBufferF.Controls.Add(this.bScaleLineColor);
            this.gbVProfileBufferF.Controls.Add(this.lLightAreaColor);
            this.gbVProfileBufferF.Controls.Add(this.bLightAreaColor);
            this.gbVProfileBufferF.Controls.Add(this.lDarkAreaColor);
            this.gbVProfileBufferF.Controls.Add(this.bDarkAreaColor);
            this.gbVProfileBufferF.Location = new System.Drawing.Point(6, 7);
            this.gbVProfileBufferF.Name = "gbVProfileBufferF";
            this.gbVProfileBufferF.Size = new System.Drawing.Size(367, 149);
            this.gbVProfileBufferF.TabIndex = 0;
            this.gbVProfileBufferF.TabStop = false;
            this.gbVProfileBufferF.Text = "Окно буфера профилей";
            // 
            // lNoiseColor
            // 
            this.lNoiseColor.Location = new System.Drawing.Point(6, 123);
            this.lNoiseColor.Name = "lNoiseColor";
            this.lNoiseColor.Size = new System.Drawing.Size(160, 20);
            this.lNoiseColor.TabIndex = 92;
            this.lNoiseColor.Text = "Цвет шума:";
            this.lNoiseColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bNoiseColor
            // 
            this.bNoiseColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bNoiseColor.Location = new System.Drawing.Point(172, 123);
            this.bNoiseColor.Name = "bNoiseColor";
            this.bNoiseColor.Size = new System.Drawing.Size(189, 20);
            this.bNoiseColor.TabIndex = 91;
            this.bNoiseColor.UseVisualStyleBackColor = true;
            this.bNoiseColor.BackColorChanged += new System.EventHandler(this.BNoiseColor_BackColorChanged);
            this.bNoiseColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // lScaleLineWidth
            // 
            this.lScaleLineWidth.Location = new System.Drawing.Point(6, 95);
            this.lScaleLineWidth.Name = "lScaleLineWidth";
            this.lScaleLineWidth.Size = new System.Drawing.Size(160, 20);
            this.lScaleLineWidth.TabIndex = 90;
            this.lScaleLineWidth.Text = "Ширина линии шкалы:";
            this.lScaleLineWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudScaleLineWidth
            // 
            this.nudScaleLineWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudScaleLineWidth.Location = new System.Drawing.Point(172, 97);
            this.nudScaleLineWidth.Name = "nudScaleLineWidth";
            this.nudScaleLineWidth.Size = new System.Drawing.Size(189, 20);
            this.nudScaleLineWidth.TabIndex = 89;
            this.nudScaleLineWidth.ValueChanged += new System.EventHandler(this.nudScaleLineWidth_ValueChanged);
            // 
            // lScaleLineColor
            // 
            this.lScaleLineColor.Location = new System.Drawing.Point(6, 71);
            this.lScaleLineColor.Name = "lScaleLineColor";
            this.lScaleLineColor.Size = new System.Drawing.Size(160, 20);
            this.lScaleLineColor.TabIndex = 86;
            this.lScaleLineColor.Text = "Цвет шкалы:";
            this.lScaleLineColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bScaleLineColor
            // 
            this.bScaleLineColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bScaleLineColor.Location = new System.Drawing.Point(172, 71);
            this.bScaleLineColor.Name = "bScaleLineColor";
            this.bScaleLineColor.Size = new System.Drawing.Size(189, 20);
            this.bScaleLineColor.TabIndex = 85;
            this.bScaleLineColor.UseVisualStyleBackColor = true;
            this.bScaleLineColor.BackColorChanged += new System.EventHandler(this.bScaleLineColor_BackColorChanged);
            this.bScaleLineColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // lLightAreaColor
            // 
            this.lLightAreaColor.Location = new System.Drawing.Point(6, 45);
            this.lLightAreaColor.Name = "lLightAreaColor";
            this.lLightAreaColor.Size = new System.Drawing.Size(160, 20);
            this.lLightAreaColor.TabIndex = 84;
            this.lLightAreaColor.Text = "Цвет светлых областей:";
            this.lLightAreaColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bLightAreaColor
            // 
            this.bLightAreaColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bLightAreaColor.Location = new System.Drawing.Point(172, 45);
            this.bLightAreaColor.Name = "bLightAreaColor";
            this.bLightAreaColor.Size = new System.Drawing.Size(189, 20);
            this.bLightAreaColor.TabIndex = 83;
            this.bLightAreaColor.UseVisualStyleBackColor = true;
            this.bLightAreaColor.BackColorChanged += new System.EventHandler(this.bLightAreaColor_BackColorChanged);
            this.bLightAreaColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // lDarkAreaColor
            // 
            this.lDarkAreaColor.Location = new System.Drawing.Point(6, 19);
            this.lDarkAreaColor.Name = "lDarkAreaColor";
            this.lDarkAreaColor.Size = new System.Drawing.Size(160, 20);
            this.lDarkAreaColor.TabIndex = 82;
            this.lDarkAreaColor.Text = "Цвет темных областей:";
            this.lDarkAreaColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bDarkAreaColor
            // 
            this.bDarkAreaColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bDarkAreaColor.Location = new System.Drawing.Point(172, 19);
            this.bDarkAreaColor.Name = "bDarkAreaColor";
            this.bDarkAreaColor.Size = new System.Drawing.Size(189, 20);
            this.bDarkAreaColor.TabIndex = 81;
            this.bDarkAreaColor.UseVisualStyleBackColor = true;
            this.bDarkAreaColor.BackColorChanged += new System.EventHandler(this.bDarkAreaColor_BackColorChanged);
            this.bDarkAreaColor.Click += new System.EventHandler(this.bColor_Click);
            // 
            // tpSensors
            // 
            this.tpSensors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpSensors.Location = new System.Drawing.Point(4, 22);
            this.tpSensors.Name = "tpSensors";
            this.tpSensors.Size = new System.Drawing.Size(376, 392);
            this.tpSensors.TabIndex = 5;
            this.tpSensors.Text = "Датчики";
            this.tpSensors.UseVisualStyleBackColor = true;
            // 
            // tpExcludedZones
            // 
            this.tpExcludedZones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpExcludedZones.Location = new System.Drawing.Point(4, 22);
            this.tpExcludedZones.Name = "tpExcludedZones";
            this.tpExcludedZones.Size = new System.Drawing.Size(376, 392);
            this.tpExcludedZones.TabIndex = 2;
            this.tpExcludedZones.Text = "Зоны";
            this.tpExcludedZones.UseVisualStyleBackColor = true;
            // 
            // tpTypeDefects
            // 
            this.tpTypeDefects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpTypeDefects.Location = new System.Drawing.Point(4, 22);
            this.tpTypeDefects.Name = "tpTypeDefects";
            this.tpTypeDefects.Size = new System.Drawing.Size(376, 392);
            this.tpTypeDefects.TabIndex = 3;
            this.tpTypeDefects.Text = "Типы дефектов";
            this.tpTypeDefects.UseVisualStyleBackColor = true;
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(3, 491);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(147, 23);
            this.bAdd.TabIndex = 81;
            this.bAdd.Text = "Создать";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lId
            // 
            this.lId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lId.Location = new System.Drawing.Point(3, 31);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(147, 20);
            this.lId.TabIndex = 77;
            this.lId.Text = "Имя конфигурации:";
            this.lId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(156, 491);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(239, 23);
            this.bSave.TabIndex = 79;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // cbId
            // 
            this.cbId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbId.Location = new System.Drawing.Point(156, 32);
            this.cbId.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(223, 21);
            this.cbId.TabIndex = 78;
            this.cbId.SelectedIndexChanged += new System.EventHandler(this.cbId_SelectedIndexChanged);
            // 
            // bApply
            // 
            this.bApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bApply.Location = new System.Drawing.Point(3, 3);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(392, 23);
            this.bApply.TabIndex = 76;
            this.bApply.Text = "Применить текущую конфигурацию";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // pbDelete
            // 
            this.pbDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDelete.BackColor = System.Drawing.SystemColors.Window;
            this.pbDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDelete.Image = global::DefectoScope.Properties.Resources.delete;
            this.pbDelete.Location = new System.Drawing.Point(379, 32);
            this.pbDelete.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pbDelete.Name = "pbDelete";
            this.pbDelete.Size = new System.Drawing.Size(16, 21);
            this.pbDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDelete.TabIndex = 80;
            this.pbDelete.TabStop = false;
            this.pbDelete.Click += new System.EventHandler(this.pbDelete_Click);
            this.pbDelete.MouseEnter += new System.EventHandler(this.pbDeleteId_MouseEnter);
            this.pbDelete.MouseLeave += new System.EventHandler(this.pbDeleteId_MouseLeave);
            // 
            // cbAutoCreateReport
            // 
            this.cbAutoCreateReport.AutoSize = true;
            this.cbAutoCreateReport.Location = new System.Drawing.Point(6, 118);
            this.cbAutoCreateReport.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbAutoCreateReport.Name = "cbAutoCreateReport";
            this.cbAutoCreateReport.Size = new System.Drawing.Size(88, 17);
            this.cbAutoCreateReport.TabIndex = 6;
            this.cbAutoCreateReport.Text = "Авто-отчеты";
            this.cbAutoCreateReport.UseVisualStyleBackColor = true;
            this.cbAutoCreateReport.CheckedChanged += new System.EventHandler(this.CbAutoCreateReport_CheckedChanged);
            // 
            // SystemSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pSystemSettings);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.lId);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.pbDelete);
            this.Controls.Add(this.cbId);
            this.Controls.Add(this.bApply);
            this.MaximumSize = new System.Drawing.Size(800, 1000);
            this.MinimumSize = new System.Drawing.Size(300, 250);
            this.Name = "SystemSettingsControl";
            this.Size = new System.Drawing.Size(398, 517);
            this.Load += new System.EventHandler(this.SystemSettingsControl_Load);
            this.pSystemSettings.ResumeLayout(false);
            this.tc.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkerLinePosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNMapColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNMapRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapRange)).EndInit();
            this.gbCalibration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudNCalibrationProfiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToleranceLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToleranceDark)).EndInit();
            this.gbController.ResumeLayout(false);
            this.gbController.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrobeEveryNMm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEncoderDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmDuration)).EndInit();
            this.tpOther.ResumeLayout(false);
            this.gbDatabase.ResumeLayout(false);
            this.gbDatabase.PerformLayout();
            this.gbOther.ResumeLayout(false);
            this.gbOther.PerformLayout();
            this.gbProfileBufferF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudNScaleLineDivisions)).EndInit();
            this.gbBufferSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudProfileBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputBufferSize)).EndInit();
            this.gbLogging.ResumeLayout(false);
            this.tpVisual.ResumeLayout(false);
            this.gbDefectsMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkerSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMarkerLineWidth)).EndInit();
            this.gbCalibrationF.ResumeLayout(false);
            this.gbVProfileBufferF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleLineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pSystemSettings;
        private System.Windows.Forms.TabControl tc;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.TabPage tpVisual;
        private System.Windows.Forms.TabPage tpExcludedZones;
        private System.Windows.Forms.TabPage tpTypeDefects;
        private System.Windows.Forms.Button bAdd;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.PictureBox pbDelete;
        private System.Windows.Forms.ComboBox cbId;
        private System.Windows.Forms.Button bApply;
        private System.Windows.Forms.GroupBox gbController;
        private System.Windows.Forms.NumericUpDown nudAlarmDuration;
        private System.Windows.Forms.CheckBox cbAlarm;
        private System.Windows.Forms.TabPage tpOther;
        private System.Windows.Forms.GroupBox gbLogging;
        private System.Windows.Forms.CheckedListBox clbLogger;
        private System.Windows.Forms.Label lComPortName;
        private System.Windows.Forms.MaskedTextBox mtbComPortName;
        private System.Windows.Forms.GroupBox gbCalibration;
        private System.Windows.Forms.Label lToleranceLight;
        private System.Windows.Forms.NumericUpDown nudToleranceLight;
        private System.Windows.Forms.Label lToleranceDark;
        private System.Windows.Forms.NumericUpDown nudToleranceDark;
        private System.Windows.Forms.Label lCalibrationFileName;
        private System.Windows.Forms.ComboBox cbCalibrationFileName;
        private System.Windows.Forms.GroupBox gbBufferSize;
        private System.Windows.Forms.Label lProfileBufferSize;
        private System.Windows.Forms.NumericUpDown nudProfileBufferSize;
        private System.Windows.Forms.Label lInputBufferSize;
        private System.Windows.Forms.NumericUpDown nudInputBufferSize;
        private System.Windows.Forms.Label lNCalibrationProfiles;
        private System.Windows.Forms.NumericUpDown nudNCalibrationProfiles;
        private System.Windows.Forms.GroupBox gbProfileBufferF;
        private System.Windows.Forms.NumericUpDown nudNScaleLineDivisions;
        private System.Windows.Forms.CheckBox cbScaleLine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudMarkerLinePosition;
        private System.Windows.Forms.CheckBox cbMarkerLine;
        private System.Windows.Forms.Label lNMapColumns;
        private System.Windows.Forms.NumericUpDown nudNMapColumns;
        private System.Windows.Forms.Label lNMapRows;
        private System.Windows.Forms.NumericUpDown nudNMapRows;
        private System.Windows.Forms.Label lMapRange;
        private System.Windows.Forms.NumericUpDown nudMapRange;
        private System.Windows.Forms.GroupBox gbOther;
        private System.Windows.Forms.CheckBox cbZoom;
        private System.Windows.Forms.CheckBox cbDebugMode;
        private System.Windows.Forms.CheckBox cbLabelsInMm;
        private System.Windows.Forms.GroupBox gbVProfileBufferF;
        private System.Windows.Forms.Label lLightAreaColor;
        private System.Windows.Forms.Button bLightAreaColor;
        private System.Windows.Forms.Label lDarkAreaColor;
        private System.Windows.Forms.Button bDarkAreaColor;
        private System.Windows.Forms.Label lScaleLineColor;
        private System.Windows.Forms.Button bScaleLineColor;
        private System.Windows.Forms.GroupBox gbCalibrationF;
        private System.Windows.Forms.Label lToleranceLightColor;
        private System.Windows.Forms.Button bToleranceLightColor;
        private System.Windows.Forms.Label lToleranceDarkColor;
        private System.Windows.Forms.Button bToleranceDarkColor;
        private System.Windows.Forms.Label lCalibrationDataColor;
        private System.Windows.Forms.Button bCalibrationDataColor;
        private System.Windows.Forms.GroupBox gbDefectsMap;
        private System.Windows.Forms.Label lMarkerLineColor;
        private System.Windows.Forms.Button bMarkerLineColor;
        private System.Windows.Forms.Label lProfileColor;
        private System.Windows.Forms.Button bProfileColor;
        private System.Windows.Forms.Label lMarkerLineWidth;
        private System.Windows.Forms.NumericUpDown nudMarkerLineWidth;
        private System.Windows.Forms.Label lMarkerSize;
        private System.Windows.Forms.NumericUpDown nudMarkerSize;
        private System.Windows.Forms.Label lScaleLineWidth;
        private System.Windows.Forms.NumericUpDown nudScaleLineWidth;
        private System.Windows.Forms.TabPage tpSensors;
        private System.Windows.Forms.GroupBox gbDatabase;
        private System.Windows.Forms.Label lSqlConnectionString;
        private System.Windows.Forms.CheckBox cbWriteInDatabase;
        private System.Windows.Forms.TextBox tbSqlConnectionString;
        private System.Windows.Forms.Label lNoiseColor;
        private System.Windows.Forms.Button bNoiseColor;
        private System.Windows.Forms.Label lStrobeEveryNMm;
        private System.Windows.Forms.Label lEncoderDivider;
        private System.Windows.Forms.NumericUpDown nudEncoderDivider;
        private System.Windows.Forms.NumericUpDown nudStrobeEveryNMm;
        private System.Windows.Forms.CheckBox cbInitTestOpenDB;
        private System.Windows.Forms.CheckBox cbAutoBorders;
        private System.Windows.Forms.CheckBox cbCalibrationWithoutDefects;
        private System.Windows.Forms.CheckBox cbCheckAutoBorders;
        private System.Windows.Forms.CheckBox cbAutoCreateReport;
    }
}
