#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace DefectoScope
{
    /// <summary>
    ///     Контрол с настройками датчика
    /// </summary>
    public partial class DynamicSensorControl : UserControl
    {
        #region Конструкторы

        public DynamicSensorControl()
        {
            InitializeComponent();

            _s = new SensorSettings();

            _numericTag = new Dictionary<NumericUpDown, string>
            {
                {nudExposition, nameof(_s.Exposition)},
                {nudAdcGain, nameof(_s.AdcGain)},
                {nudPgaGain, nameof(_s.PgaGain)},
            };
        }

        #endregion

        #region Методы

        /// <summary>
        ///     Отображает настройки на экран
        /// </summary>
        private void ShowSettings()
        {
            nudExposition.Value = _s.Exposition;
            nudAdcGain.Value = _s.AdcGain;
            nudPgaGain.Value = _s.PgaGain;
        }

        #endregion

        #region Поля

        /// <summary>
        ///     Основной конфиг настроек
        /// </summary>
        private SensorSettings _s;
        
        #endregion

        #region Свойства

        /// <summary>
        /// Массив объектов датчиков. Необходимо для отображения данных на контроле!
        /// </summary>
        public Sensor[] Sensors { get; set; } = new Sensor[0];

        /// <summary>
        /// Производились изменения?
        /// </summary>
        public bool Changed { get; private set; }

        #endregion

        #region События

        #region Формы

        private void DynamicSensorControl_Load(object sender, EventArgs e) => Reload();

        #endregion

        #endregion

        #region Общая часть

        #region Поля

        /// <summary>
        ///     Связь числовых регуляторов и имен свойств (для считывания минимума и максимума)
        /// </summary>
        private readonly Dictionary<NumericUpDown, string> _numericTag;

        #endregion

        #region События

        #region Контролов

        private void cbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если мы тут после отмены или -1, то уходим
            if (cbId.SelectedIndex == -1) return;

            foreach (var s in Sensors)
            {
                if (s.Settings.Id == cbId.Text)
                    _s = s.Settings;
            }

            ShowSettings();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            _s.Exposition = (ushort) nudExposition.Value;
            _s.AdcGain = (byte)nudAdcGain.Value;
            _s.PgaGain = (byte)nudPgaGain.Value;

            _s.SaveSettingsInFile();

            foreach (var s in Sensors)
            {
                if (s.Settings != _s)
                    continue;

                var success =
                    s.SetExposition() &&
                    s.SetAdcGain() &&
                    s.SetPgaGain();

                if (!success)
                {
                    MessageBox.Show(@"Не удалось применить настройки, датчик не отвечает!", @"Датчик не отвечает",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Changed = false;
        }

        #endregion

        #endregion

        #region Методы

        /// <summary>
        ///     Устанавливает ограничения на элементы управления в соответствие с настройками
        /// </summary>
        private void SetLimitSettings()
        {
            //Применяем ограничения конфигурации
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
            SetLimitSettings();

            //Добавляем элементы объектов
            cbId.SelectedIndex = -1;
            cbId.Items.Clear();

            foreach (var s in Sensors)
                cbId.Items.Add(s.Settings.Id);

            if (Sensors.Length > 0)
                cbId.SelectedIndex = 0;
        }


        #endregion

        #endregion
    }
}