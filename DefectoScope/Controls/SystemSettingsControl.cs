using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DefectoScope.Properties;
using Kogerent;
using Timer = System.Timers.Timer;

namespace DefectoScope
{
    public partial class SystemSettingsControl : UserControl
    {
        #region Константы

        /// <summary>
        /// Высота заголовка вкладки
        /// </summary>
        private const int TabHeaderHeight = 20;

        #endregion

        #region Поля

        /// <summary>
        /// Таймер проверки состояний
        /// </summary>
        private readonly Timer _tCheckState;

        /// <summary>
        ///     Датчики
        /// </summary>
        private readonly SensorSettingsControl _sensor;

        /// <summary>
        ///     Исключенные зоны
        /// </summary>
        private readonly ExcludedZoneSettingsControl _excludedZone;

        /// <summary>
        ///     Дефекты
        /// </summary>
        private readonly TypeDefectSettingsControl _typeDefect;

        /// <summary>
        ///     Основной конфиг настроек
        /// </summary>
        private readonly SystemSettings _s;

        private bool _changed;

        #endregion

        #region Свойства

        /// <summary>
        /// Производились изменения (кроме изменений датчиков, для них флаг <see cref="SensorsChanged"/>)?
        /// </summary>
        public bool Changed
        {
            get => _changed || /*_sensor.Changed ||*/ _excludedZone.Changed || _typeDefect.Changed;
            private set => _changed = value;
        }

        /// <summary>
        /// Производились изменения датчиков? (изменение имени конфига означает смену и набора датчиков)
        /// </summary>
        public bool SensorsChanged
        {
            get => _sensor.Changed || _s.NSensors != G.Sensors.Length || _s.Id != G.Settings.Id;
            private set => _changed = value;
        }

        #endregion

        #region Конструкторы

        public SystemSettingsControl()
        {
            InitializeComponent();

            _tCheckState = new Timer(100) { SynchronizingObject = this };
            _tCheckState.Elapsed += tCheckState_Tick;

            _s = new SystemSettings();

            tc.ItemSize = new Size(0, TabHeaderHeight);

            _sensor = new SensorSettingsControl() { Dock = DockStyle.Fill };
            tpSensors.Width = _sensor.Bounds.Width;
            tpSensors.Height = _sensor.Bounds.Height;
            tpSensors.Controls.Add(_sensor);

            _excludedZone = new ExcludedZoneSettingsControl() { Dock = DockStyle.Fill };
            tpExcludedZones.Width = _excludedZone.Bounds.Width;
            tpExcludedZones.Height = _excludedZone.Bounds.Height;
            tpExcludedZones.Controls.Add(_excludedZone);

            _typeDefect = new TypeDefectSettingsControl() { Dock = DockStyle.Fill };
            tpTypeDefects.Width = _typeDefect.Bounds.Width;
            tpTypeDefects.Height = _typeDefect.Bounds.Height;
            tpTypeDefects.Controls.Add(_typeDefect);

            //Определяем размеры
            var x = 0;
            var y = 0;

            foreach (TabPage tabPage in tc.TabPages)
            {
                if (x < tabPage.Width) x = tabPage.Width;
                if (y < tabPage.Height) y = tabPage.Height;
            }

            //Размер окна вкладок это максимальный размер вкладки с учетом отступов (двойные для запаса)
            tc.Size = new Size(x + Margin.Horizontal * 2, y + tc.ItemSize.Height + Margin.Vertical * 2);

            //После добавления всех вкладок инициализируем словари
            _numericTag = new Dictionary<NumericUpDown, string>
            {
                {_sensor.nudNSensors, nameof(_s.NSensors) },
                {_excludedZone.nudNExcludedZones, nameof(_s.NExcludedZones)},
                {_typeDefect.nudNTypeDefects, nameof(_s.NTypeDefects)},

                {nudEncoderDivider, nameof(_s.EncoderDivider)},
                {nudStrobeEveryNMm, nameof(_s.StrobeEveryNMm)},
                {nudAlarmDuration, nameof(_s.AlarmDuration)},

                {nudToleranceDark, nameof(_s.ToleranceDark)},
                {nudToleranceLight, nameof(_s.ToleranceLight)},
                {nudNCalibrationProfiles, nameof(_s.NCalibrationProfiles)},

                {nudProfileBufferSize, nameof(_s.ProfileBufferSize)},
                {nudInputBufferSize, nameof(_s.InputBufferSize)},

                {nudMapRange, nameof(_s.MapRange)},
                {nudMarkerLinePosition, nameof(_s.MarkerLinePosition)},
                {nudMarkerLineWidth, nameof(_s.MarkerLineWidth)},
                {nudNMapRows, nameof(_s.NMapRows)},
                {nudNMapColumns, nameof(_s.NMapColumns)},
                {nudMarkerSize, nameof(_s.MarkerSize)},

                {nudNScaleLineDivisions, nameof(_s.NScaleLineDivisions)},
                {nudScaleLineWidth, nameof(_s.ScaleLineWidth)},
            };

            _bColorIsOk = new Dictionary<Button, bool>
            {
                {bDarkAreaColor, true},
                {bLightAreaColor, true},
                {bScaleLineColor, true},
                {bCalibrationDataColor, true},
                {bNoiseColor, true },
                {bToleranceDarkColor, true},
                {bToleranceLightColor, true},
                {bProfileColor, true},
                {bMarkerLineColor, true},
            };

            _controlIsOk = new Dictionary<Control, bool>
            {
                {mtbComPortName, true},
            };

            
            //Привязываемся контролам
            _sensor.SParent = _s;
            _sensor.nudNSensors.ValueChanged += NudNSensorsOnValueChanged;
            _excludedZone.SParent = _s;
            _excludedZone.nudNExcludedZones.ValueChanged += NudNExcludedZonesOnValueChanged;
            _typeDefect.SParent = _s;
            _typeDefect.nudNTypeDefects.ValueChanged += NudNTypeDefectsOnValueChanged;
        }

        #endregion

        #region Таймеры

        private void tCheckState_Tick(object sender, EventArgs e)
        {
            //_tCheckState.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tCheckState.Enabled = false;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());

                //Если таймера нет, ничего не делаем
                return;
            }

            //Тело таймера
            try
            {
                    var colorsIsOk = true;
                    foreach (var b in _bColorIsOk) colorsIsOk = colorsIsOk && b.Value;
                    _configOk = colorsIsOk;

                    var controlsIsOk = true;
                    foreach (var c in _controlIsOk) controlsIsOk = controlsIsOk && c.Value;
                    _configOk = _configOk && controlsIsOk;

                    var isGoodChanged = _configOk && ConfigChanged;

                    if (bSave.Enabled != isGoodChanged) bSave.Enabled = isGoodChanged;
                    if (bAdd.Enabled != _configOk) bAdd.Enabled = _configOk;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tCheckState.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        #endregion

        #region Методы

        /// <summary>
        ///     Отображает настройки на экран
        /// </summary>
        private void ShowSettings()
        {
            _shown = false;

            _sensor.nudNSensors.Value = _s.NSensors;
            _excludedZone.nudNExcludedZones.Value = _s.NExcludedZones;
            _typeDefect.nudNTypeDefects.Value = _s.NTypeDefects;

            mtbComPortName.Text = _s.ComPortName;
            nudEncoderDivider.Value = _s.EncoderDivider;
            nudStrobeEveryNMm.Value = (decimal)_s.StrobeEveryNMm;
            cbAlarm.Checked = _s.Alarm;
            nudAlarmDuration.Value = _s.AlarmDuration;


            var index = cbCalibrationFileName.Items.IndexOf(_s.CalibrationFileName);
            if (index == -1)
            {
                MessageBox.Show($@"Файл калибровки ""{_s.CalibrationFileName}"" не существует. Будет указан элемент по умолчанию.",
                    @"Не найден файл калибровки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                //Индекс элемента по умолчанию
                index = 0;
            }
            cbCalibrationFileName.SelectedIndex = index;


            bCalibrationDataColor.BackColor = _s.CalibrationDataColor;
            bNoiseColor.BackColor = _s.NoiseColor;
            nudToleranceDark.Value = _s.ToleranceDark;
            bToleranceDarkColor.BackColor = _s.ToleranceDarkColor;
            bDarkAreaColor.BackColor = _s.DarkAreaColor;
            nudToleranceLight.Value = _s.ToleranceLight;
            bToleranceLightColor.BackColor = _s.ToleranceLightColor;
            bLightAreaColor.BackColor = _s.LightAreaColor;
            nudNCalibrationProfiles.Value = _s.NCalibrationProfiles;
            bProfileColor.BackColor = _s.ProfileColor;

            nudMapRange.Value = (decimal)_s.MapRange;
            cbMarkerLine.Checked = _s.MarkerLine;
            nudMarkerLinePosition.Value = (decimal)_s.MarkerLinePosition;
            bMarkerLineColor.BackColor = _s.MarkerLineColor;
            nudMarkerLineWidth.Value = _s.MarkerLineWidth;
            nudNMapRows.Value = _s.NMapRows;
            nudNMapColumns.Value = _s.NMapColumns;
            nudMarkerSize.Value = _s.MarkerSize;

            nudInputBufferSize.Value = _s.InputBufferSize;
            nudProfileBufferSize.Value = _s.ProfileBufferSize;

            cbScaleLine.Checked = _s.ScaleLine;
            nudNScaleLineDivisions.Value = _s.NScaleLineDivisions;
            bScaleLineColor.BackColor = _s.ScaleLineColor;
            nudScaleLineWidth.Value = _s.ScaleLineWidth;

            clbLogger.SetItemChecked(0, _s.LogTrace);
            clbLogger.SetItemChecked(1, _s.LogDebug);
            clbLogger.SetItemChecked(2, _s.LogInfo);
            clbLogger.SetItemChecked(3, _s.LogWarn);
            clbLogger.SetItemChecked(4, _s.LogError);
            clbLogger.SetItemChecked(5, _s.LogFatal);

            //cbWorkingSensors.Checked = _s.WorkingSensors;
            cbDebugMode.Checked = _s.DebugMode;
            cbZoom.Checked = _s.Zoom;
            cbLabelsInMm.Checked = _s.LabelsInMm;

            tbSqlConnectionString.Text = _s.SqlConnectionString;
            cbWriteInDatabase.Checked = _s.WriteInDatabase;
            cbInitTestOpenDB.Checked = _s.InitTestOpenDB;
            cbAutoBorders.Checked = _s.AutoBorders;
            cbCheckAutoBorders.Checked = _s.CheckAutoBorderDefects;
            cbCalibrationWithoutDefects.Checked = _s.CalibrationWithoutDefects;
            cbAutoCreateReport.Checked = _s.AutoCreateReport;

            _shown = true;
            ConfigChanged = false;
        }

        #endregion

        #region События

        #region Формы

        private void SystemSettingsControl_Load(object sender, EventArgs e) => Reload();

        #endregion

        #region Контролов

        private void bApply_Click(object sender, EventArgs e)
        {
            G.ConfigSettings.SystemSettings = _s.Id;
            G.ConfigSettings.SaveSettingsInFile();

            Changed = true;
            //UtilsSettings.LoadConfigSettings();
            //UtilsSettings.LoadSystemSettings();
            //UtilsSettings.LoadExcludedZoneSettings();
            //UtilsSettings.LoadTypeDefectSettings();

        }

        #endregion

        #endregion

        #region Общая часть

        #region Поля

        /// <summary>
        ///     Связь числовых регуляторов и имен свойств (для считывания минимума и максимума)
        /// </summary>
        private readonly Dictionary<NumericUpDown, string> _numericTag;

        /// <summary>
        /// Связь цветовых кнопок и их состояния, в порядке ли они
        /// </summary>
        private readonly Dictionary<Button, bool> _bColorIsOk;

        /// <summary>
        /// Связь контролов и их состояния, в порядке ли они
        /// </summary>
        private readonly Dictionary<Control, bool> _controlIsOk;

        /// <summary>
        ///     Последний индекс конфигурации
        /// </summary>
        private int _lastConfigIndex;

        /// <summary>
        ///     Конфиг был показан?
        /// </summary>
        private bool _shown;

        /// <summary>
        ///     Конфиг в порядке?
        /// </summary>
        private bool _configOk;

        /// <summary>
        ///     Конфиг изменялся?
        /// </summary>
        private bool _configChanged;

        /// <summary>
        ///     Изменение было отменено?
        /// </summary>
        private bool _canceled;

        #endregion

        #region Свойства

        /// <summary>
        ///     Конфиг изменялся?
        /// </summary>
        private bool ConfigChanged
        {
            get => _configChanged;
            set
            {
                if (_shown || !value)
                    _configChanged = value;
            }
        }

        #endregion

        #region События

        #region Контролов

        private void cbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если мы тут после отмены или -1, то уходим
            if (_canceled || cbId.SelectedIndex == -1) return;

            //Если конфиг менялся, то необходимо что то делать с этим
            if (ConfigChanged)
            {
                DialogResult result;

                if (_configOk)
                {
                    result = MessageBox.Show(@"Сохранить измененную конфигурацию перед ее сменой?",
                        @"Сохранить изменения?",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                }
                else
                {
                    result = MessageBox.Show(@"Измененную конфигурацию нельзя сохранить, она будет восстановлена.",
                        @"Восстановление конфигурации?",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                }

                switch (result)
                {
                    case DialogResult.None:
                    case DialogResult.Abort:
                    case DialogResult.Retry:
                    case DialogResult.Ignore:
                        break;
                    case DialogResult.OK:
                    case DialogResult.No:
                        {
                            ConfigChanged = false;
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            _canceled = true;
                            cbId.SelectedIndex = _lastConfigIndex;
                            _canceled = false;
                            return;
                        }
                    case DialogResult.Yes:
                        {
                            var id = cbId.Items[_lastConfigIndex].ToString();
                            _s.SaveSettingsInFile(id);
                            ConfigChanged = false;
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _lastConfigIndex = cbId.SelectedIndex;

            UtilsSettings.LoadAndCheckSettings(_s, cbId.Text);
            ShowSettings();
            _sensor.Reload();
            _excludedZone.Reload();
            _typeDefect.Reload();
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Вы действительно желаете удалить текущую конфигурацию?",
                @"Удалить конфигурацию?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            var id = cbId.Text;

            for (var i = 0; i < cbId.Items.Count; i++)
            {
                if (cbId.Items[i].ToString() == id)
                {
                    cbId.Items.RemoveAt(i);
                    if (cbId.Items.Count > 0)
                        cbId.SelectedIndex = i - 1;
                    break;
                }
            }

            _s.DeleteSettingsInFile(id);

            ConfigChanged = false;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            _s.SaveSettingsInFile();
            ConfigChanged = false;
            _s.LoadSettingsFromFile(_s.Id);

            _sensor.Reload();
            _excludedZone.Reload();
            _typeDefect.Reload();

            Changed = true;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //Если конфиг менялся, то необходимо что то делать с этим
            if (ConfigChanged)
            {
                var result = MessageBox.Show(@"Сохранить измененную конфигурацию перед созданием новой?",
                    @"Сохранить изменения?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.None:
                    case DialogResult.Abort:
                    case DialogResult.Retry:
                    case DialogResult.Ignore:
                    case DialogResult.OK:
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        {
                            _s.SaveSettingsInFile();
                            ConfigChanged = false;
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var createConfigF = new CreateConfigForm(cbId.Items.Cast<string>().ToArray(), cbId.Text);
            var id = createConfigF.GetId();
            if (id != null)
            {
                Add(id);

                //_excludedZone.Reload();
                //_typeDefect.Reload();
            }
        }

        #endregion

        #region Мыши

        private void pbDeleteId_MouseEnter(object sender, EventArgs e) =>
            ((PictureBox)sender).Image = Resources.deleteHighlight;

        private void pbDeleteId_MouseLeave(object sender, EventArgs e) =>
            ((PictureBox)sender).Image = Resources.delete;

        #endregion

        #endregion

        #region Методы

        /// <summary>
        ///     Добавляет новую конфигурацию
        /// </summary>
        private void Add(string id)
        {
            _s.SaveSettingsInFile(id);
            ConfigChanged = false;

            cbId.Items.Add(id);
            cbId.SelectedIndex = cbId.Items.IndexOf(id);
        }

        /// <summary>
        ///     Устанавливает ограничения на элементы управления в соответствие с настройками
        /// </summary>
        private void SetLimitSettings()
        {
            foreach (var numeric in _numericTag)
            {
                numeric.Key.Minimum = decimal.Parse(_s.Limit[numeric.Value][0].ToString());
                numeric.Key.Maximum = decimal.Parse(_s.Limit[numeric.Value][1].ToString());
            }
        }

        /// <summary>
        /// Производит повторную настройку контрола
        /// </summary>
        public void Reload()
        {
            ConfigChanged = false;
            _shown = false;

            SetLimitSettings();

            //Файл калибровки выбирается из списка
            cbCalibrationFileName.SelectedIndex = -1;
            cbCalibrationFileName.Items.Clear();
            cbCalibrationFileName.Items.Add(Calibration.DefCalibrationFileName);
            cbCalibrationFileName.Items.AddRange(Calibration.GetCalibrationFileNames());

            //Считываем имеющиеся ID из файла настроек
            cbId.SelectedIndex = -1;
            cbId.Items.Clear();
            cbId.Items.AddRange(_s.GetNames());

            //Выбираем конфигурацию в соответствие с настройками
            var id = G.ConfigSettings.SystemSettings;
            cbId.SelectedIndex = cbId.Items.IndexOf(id);

            ////Указываем палитру хороших известных цветов
            //cd.CustomColors = OffColor.NiceColors;
        }


        #endregion

        #endregion

        #region События цвета

        private void bColor_Click(object sender, EventArgs e)
        {
            var knownColorsF = new KnownColorsForm();
            var color = knownColorsF.GetColor();
            if (color != default)
                ((Button)sender).BackColor = color;
        }

        private void bDarkAreaColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.DarkAreaKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lDarkAreaColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lDarkAreaColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bLightAreaColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.LightAreaKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lLightAreaColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lLightAreaColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bScaleLineColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.ScaleLineKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lScaleLineColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lScaleLineColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bCalibrationDataColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.CalibrationDataKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lCalibrationDataColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lCalibrationDataColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void BNoiseColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.NoiseKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lNoiseColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lNoiseColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bToleranceDarkColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.ToleranceDarkKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lToleranceDarkColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lToleranceDarkColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bToleranceLightColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.ToleranceLightKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lToleranceLightColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lToleranceLightColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bProfileColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.ProfileKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lProfileColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lProfileColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void bMarkerLineColor_BackColorChanged(object sender, EventArgs e)
        {
            var bColor = (Button)sender;

            if (bColor.BackColor.IsKnownColor)
            {
                _s.MarkerLineKnownColor = bColor.BackColor.ToKnownColor();
                _bColorIsOk[bColor] = true;
                lMarkerLineColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bColor] = false;
                lMarkerLineColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }


        #endregion

        #region Числовые регуляторы

        private void NudNSensorsOnValueChanged(object sender, EventArgs e)
        {
            _s.NSensors = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void NudNExcludedZonesOnValueChanged(object sender, EventArgs e)
        {
            _s.NExcludedZones = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void NudNTypeDefectsOnValueChanged(object sender, EventArgs e)
        {
            _s.NTypeDefects = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void NudEncoderDivider_ValueChanged(object sender, EventArgs e)
        {
            _s.EncoderDivider = (ushort)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void NudStrobeEveryNMm_ValueChanged(object sender, EventArgs e)
        {
            _s.StrobeEveryNMm = (float)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudAlarmDuration_ValueChanged(object sender, EventArgs e)
        {
            _s.AlarmDuration = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudToleranceDark_ValueChanged(object sender, EventArgs e)
        {
            _s.ToleranceDark = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudToleranceLight_ValueChanged(object sender, EventArgs e)
        {
            _s.ToleranceLight = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudNCalibrationProfiles_ValueChanged(object sender, EventArgs e)
        {
            _s.NCalibrationProfiles = (ushort)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudMapRange_ValueChanged(object sender, EventArgs e)
        {
            _s.MapRange = (float)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudMarkerLinePosition_ValueChanged(object sender, EventArgs e)
        {
            _s.MarkerLinePosition = (float)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudNMapRows_ValueChanged(object sender, EventArgs e)
        {
            _s.NMapRows = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudNMapColumns_ValueChanged(object sender, EventArgs e)
        {
            _s.NMapColumns = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudInputBufferSize_ValueChanged(object sender, EventArgs e)
        {
            _s.InputBufferSize = (int)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudProfileBufferSize_ValueChanged(object sender, EventArgs e)
        {
            _s.ProfileBufferSize = (ushort)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudNScaleLineDivisions_ValueChanged(object sender, EventArgs e)
        {
            _s.NScaleLineDivisions = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudScaleLineWidth_ValueChanged(object sender, EventArgs e)
        {
            _s.ScaleLineWidth = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudMarkerLineWidth_ValueChanged(object sender, EventArgs e)
        {
            _s.MarkerLineWidth = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        private void nudMarkerSize_ValueChanged(object sender, EventArgs e)
        {
            _s.MarkerSize = (byte)((NumericUpDown)sender).Value;
            ConfigChanged = true;
        }

        #endregion

        #region Ключи

        private void cbAlarm_CheckedChanged(object sender, EventArgs e)
        {
            _s.Alarm = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void cbMarkerLine_CheckedChanged(object sender, EventArgs e)
        {
            _s.MarkerLine = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void cbScaleLine_CheckedChanged(object sender, EventArgs e)
        {
            _s.ScaleLine = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void clbLogger_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var check = e.NewValue == CheckState.Checked;

            switch (e.Index)
            {
                case 0:
                    _s.LogTrace = check;
                    break;
                case 1:
                    _s.LogDebug = check;
                    break;
                case 2:
                    _s.LogInfo = check;
                    break;
                case 3:
                    _s.LogWarn = check;
                    break;
                case 4:
                    _s.LogError = check;
                    break;
                case 5:
                    _s.LogFatal = check;
                    break;
                default:
                    break;
            }

            ConfigChanged = true;
        }

        //private void cbWorkingSensors_CheckedChanged(object sender, EventArgs e)
        //{
        //    _s.WorkingSensors = ((CheckBox)sender).Checked;
        //    ConfigChanged = true;
        //}

        private void cbDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            _s.DebugMode = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void cbZoom_CheckedChanged(object sender, EventArgs e)
        {
            _s.Zoom = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void cbLabelsInMm_CheckedChanged(object sender, EventArgs e)
        {
            _s.LabelsInMm = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void cbWriteInDatabase_CheckedChanged(object sender, EventArgs e)
        {
            _s.WriteInDatabase = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void CbInitTestOpenDB_CheckedChanged(object sender, EventArgs e)
        {
            _s.InitTestOpenDB = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void CbAutoBorders_CheckedChanged(object sender, EventArgs e)
        {
            _s.AutoBorders = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void CbCheckAutoBorders_CheckedChanged(object sender, EventArgs e)
        {
            _s.CheckAutoBorderDefects = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void CbCalibrationWithoutDefects_CheckedChanged(object sender, EventArgs e)
        {
            _s.CalibrationWithoutDefects = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        private void CbAutoCreateReport_CheckedChanged(object sender, EventArgs e)
        {
            _s.AutoCreateReport = ((CheckBox)sender).Checked;
            ConfigChanged = true;
        }

        #endregion

        #region Прочие 

        private void mtbComPortName_TextChanged(object sender, EventArgs e)
        {
            var control = (Control)sender;

            if (UtilsD.IsValidComPortName(mtbComPortName.Text, out var newComPortName))
            {
                _s.ComPortName = newComPortName;
                _controlIsOk[control] = true;
                lComPortName.ForeColor = Color.DarkGreen;
            }
            else
            {
                _controlIsOk[control] = false;
                lComPortName.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        private void cbCalibrationFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBox) sender;

            _s.CalibrationFileName = cb.Text;
            ConfigChanged = true;
        }

        private void tbSqlConnectionString_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;

            _s.SqlConnectionString = tb.Text;
            ConfigChanged = true;
        }






        #endregion


    }
}