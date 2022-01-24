#region

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DefectoScope.Properties;
using Timer = System.Timers.Timer;

#endregion

namespace DefectoScope
{
    /// <summary>
    /// Окно контроллера стробов
    /// </summary>
    public partial class ControllerForm : Form
    {
        /// <summary>
        ///     Временный объект COM-порта для проверок
        /// </summary>
        private SyncController _sync;

        /// <summary>
        ///     Считывать стробы?
        /// </summary>
        private bool _readStrobes;

        /// <summary>
        /// Таймер обновления
        /// </summary>
        private readonly Timer _tUpdater;

        /// <summary>
        /// Создает окно контроллера стробов
        /// </summary>
        public ControllerForm()
        {
            InitializeComponent();

            _tUpdater = new Timer(100) { SynchronizingObject = this };
            _tUpdater.Elapsed += tUpdater_Tick;
        }

        private void ControllerForm_Load(object sender, EventArgs e)
        {
            tbComPortName.Text = G.Settings.ComPortName;
            nudDivider.Value = G.Settings.EncoderDivider;

            _sync = new SyncController(tbComPortName.Text);

            _tUpdater.Enabled = true;
        }

        /// <summary>
        ///     Отображает результат на экране
        /// </summary>
        /// <param name="text">Текст параметра результирования</param>
        /// <param name="isGood">Результат положительный?</param>
        private void ShowResult(string text, bool isGood)
        {
            lStatusValue.Text = $@"{text}: {(isGood ? "Успешно" : "Ошибка")}";
            lStatusValue.ForeColor = isGood ? Color.Green : Color.Red;
            lStatusValue.Update();
        }

        private void bSetName_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь нажал на кнопку установки имени");

            cbReading.Checked = false;

            _sync?.Dispose();
            _sync = new SyncController(tbComPortName.Text);

            var res = _sync.TestOpen();

            pbTestOpen.Image = res ? Resources.ok : Resources.delete;
            ShowResult($"Соединение с {_sync.PortName}", res);
        }

        private void bWriteConfig_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь нажал на кнопку записи конфигурации");

            if (!_sync.StopController())
            {
                ShowResult("Стоп", false);
                return;
            }

            //На обычном контроллере этот регистр отвечает за период импульсов синхронизации, а тут не знаю
            if (!_sync.Unknown())
            {
                ShowResult($@"Регистр {Reg.EncoderConfig}", false);
                return;
            }

            if (!_sync.SetEncoderDivider((ushort)nudDivider.Value))
            {
                ShowResult(@"Делитель", false);
                return;
            }

            ShowResult(@"Запись конфигурации", true);
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь нажал на кнопку старта");

            ShowResult(@"Старт", _sync.StartController());
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь нажал на кнопку остановки");

            ShowResult(@"Стоп", _sync.StopController());
        }

        private void bWriteKeys_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь нажал на кнопку записи ключей");

            _sync.SW1 = cbKey16.Checked;
            _sync.SW2 = cbKey17.Checked;

            ShowResult(@"Реле SW", _sync.SW());
        }

        private void tUpdater_Tick(object sender, EventArgs e)
        {
            //_tUpdater.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tUpdater.Enabled = false;
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
                //Ничего не делаем, если читать не требуется
                if (_readStrobes)
                {
                    ShowResult(@"Счетчик импульсов", _sync.UpdateCount());

                    lCountStrobeValue.Text = _sync.EncoderCountImpulse.ToString();
                    lCountStrobeValue.Update();
                }
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tUpdater.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        #region События контрола

        private void cbReading_CheckedChanged(object sender, EventArgs e) =>
            _readStrobes = cbReading.Checked;

        #endregion

        private void ControllerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tUpdater.Enabled = false;
            _tUpdater.Elapsed -= tUpdater_Tick;
            Enabled = false;
            Thread.Sleep(500);

            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь закрыл окно");
        }

        private void BCurrentPortName_Click(object sender, EventArgs e)
        {
            G.Logger.Info($"{nameof(ControllerForm)}: Пользователь нажал на Текущего имя");

            if (_sync == null)
            {
                ShowResult(@"Текущее имя", false);
                return;
            }

            tbComPortName.Text = _sync.PortName;
            ShowResult(@"Текущее имя", true);
        }
    }
}