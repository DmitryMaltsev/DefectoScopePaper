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
    public partial class TypeDefectSettingsControl : UserControl
    {
        #region Конструкторы

        public TypeDefectSettingsControl()
        {
            InitializeComponent();

            _tCheckState = new Timer(100) { SynchronizingObject = this };
            _tCheckState.Elapsed += tCheckState_Tick;

            _s = new TypeDefectSettings();

            _numericTag = new Dictionary<NumericUpDown, string>
            {
                {nudSquare, nameof(_s.Square)},
                {nudLength, nameof(_s.Length)},
                {nudWidth, nameof(_s.Width)},
            };

            _bColorIsOk = new Dictionary<Button, bool>
            {
                {bMarkerColor, true},
            };
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

            cbLevel.Checked = _s.Level;
            nudSquare.Value = (decimal)_s.Square;
            nudLength.Value = (decimal)_s.Length;
            nudWidth.Value = (decimal) _s.Width;
            cbMarker.SelectedIndex = (int)_s.Marker;
            bMarkerColor.BackColor = _s.MarkerColor;

            _shown = true;
            ConfigChanged = false;
        }

        #endregion

        #region Поля

        /// <summary>
        /// Таймер проверки состояний
        /// </summary>
        private readonly Timer _tCheckState;

        /// <summary>
        ///     Основной конфиг настроек
        /// </summary>
        private readonly TypeDefectSettings _s;

        private SystemSettings _sParent = G.Settings;

        /// <summary>
        /// Массив конфигов
        /// </summary>
        private TypeDefectSettings[] _sArray = new TypeDefectSettings[0];

        #endregion

        #region Свойства

        /// <summary>
        /// Родительский конфиг настроек. Если устанавливается в null, то берется основная системная конфигурация.
        /// </summary>
        public SystemSettings SParent
        {
            get => _sParent;
            set => _sParent = value ?? G.Settings;
        }

        /// <summary>
        /// Производились изменения?
        /// </summary>
        public bool Changed { get; private set; }

        #endregion

        #region События

        #region Формы

        private void TypeDefectSettingsControl_Load(object sender, EventArgs e) => Reload();

        #endregion

        #region Контролов

        private void cbNTypeDefect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если мы тут после отмены или после -1, то уходим
            if (_canceled || cbNTypeDefect.SelectedIndex == -1) return;

            //Если конфиг менялся, то необходимо что то делать с этим
            if (ConfigChanged)
            {
                DialogResult result;

                if (_configOk)
                {
                    result = MessageBox.Show(@"Сохранить измененную конфигурацию исключаемой зоны?",
                        @"Сохранить изменения?",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                }
                else
                {
                    result = MessageBox.Show(@"Измененную конфигурацию исключаемой зоны нельзя сохранить, она будет восстановлена.",
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
                            cbNTypeDefect.SelectedIndex = _lastIndex;
                            _canceled = false;
                            return;
                        }
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

            //Запоминаем индекс
            _lastIndex = cbNTypeDefect.SelectedIndex;

            //Изменяем конфиг на текущий для следующего объекта датчика
            var id = _sArray[_lastIndex].Id;
            cbId.SelectedIndex = cbId.Items.IndexOf(id);
        }

        private void bApply_Click(object sender, EventArgs e)
        {
            var index = cbNTypeDefect.SelectedIndex;

            if (index >= 0)
            {
                SParent.TypeDefects[index] = _s.Id;
                SParent.SaveSettingsInFile();

                UtilsSettings.LoadTypeDefectSettings(SParent, out _sArray);
            }

            Changed = true;
        }

        private void bMarkerColor_Click(object sender, EventArgs e)
        {
            var knownColorsF = new KnownColorsForm();
            var color = knownColorsF.GetColor();
            if (color != default)
                bMarkerColor.BackColor = color;
        }

        #region Изменение конфигурации

        private void cbLevel_CheckedChanged(object sender, EventArgs e)
        {
            cbLevel.Text = cbLevel.Checked ? "Светлый" : "Темный";
            _s.Level = cbLevel.Checked;
            ConfigChanged = true;
        }

        private void nudSquare_ValueChanged(object sender, EventArgs e)
        {
            _s.Square = (float)nudSquare.Value;
            ConfigChanged = true;
        }

        private void nudLength_ValueChanged(object sender, EventArgs e)
        {
            _s.Length = (float)nudLength.Value;
            ConfigChanged = true;
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            _s.Width = (float)nudWidth.Value;
            ConfigChanged = true;
        }

        private void cbMarker_SelectedIndexChanged(object sender, EventArgs e)
        {
            _s.Marker = (Marker)cbMarker.SelectedIndex;
            ConfigChanged = true;
        }

        private void bFillColor_BackColorChanged(object sender, EventArgs e)
        {
            if (bMarkerColor.BackColor.IsKnownColor)
            {
                _s.MarkerKnownColor = bMarkerColor.BackColor.ToKnownColor();
                _bColorIsOk[bMarkerColor] = true;
                lMarkerColor.ForeColor = Color.DarkGreen;
            }
            else
            {
                _bColorIsOk[bMarkerColor] = false;
                lMarkerColor.ForeColor = Color.DarkRed;
            }

            ConfigChanged = true;
        }

        #endregion

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
        ///     Последний индекс объекта
        /// </summary>
        private int _lastIndex;

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
            if (id != null) Add(id);
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

            //Считываем типы маркеров
            cbMarker.DataSource = Enum.GetValues(typeof(Marker));

            //Загружаем настройки
            UtilsSettings.LoadTypeDefectSettings(SParent, out _sArray);

            //Количество объектов
            var length = _sArray.Length;

            //Добавляем элементы объектов
            cbNTypeDefect.SelectedIndex = -1;
            cbNTypeDefect.Items.Clear();
            for (var j = 0; j < length; j++) cbNTypeDefect.Items.Add(j + 1);

            //Считываем имеющиеся ID из файла настроек
            cbId.SelectedIndex = -1;
            cbId.Items.Clear();
            cbId.Items.AddRange(_s.GetNames());

            //Стартовый индекс
            const int startIndex = 0;

            if (length > startIndex)
            {
                //Выбираем объект по индексу
                cbNTypeDefect.SelectedIndex = startIndex;

                //Выбираем конфигурацию выбранного объекта
                var id = _sArray[startIndex].Id;
                cbId.SelectedIndex = cbId.Items.IndexOf(id);
            }
            else if (cbId.Items.Count > 0)
                cbId.SelectedIndex = 0;

            ////Указываем палитру хороших известных цветов
            //cd.CustomColors = OffColor.NiceColors;
        }

        #endregion

        #endregion


    }
}