using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Kogerent;
using Timer = System.Timers.Timer;

namespace DefectoScope
{
    /// <summary>
    /// Окно буфера профилей
    /// </summary>
    public partial class ProfileBufferForm : Form
    {
        /// <summary>
        /// Текущее изображение буфера профилей
        /// </summary>
        private Bitmap _currentImage;

        /// <summary>
        ///     Таймер по обновлению изображения буфера профилей
        /// </summary>
        private readonly Timer _tImageUpdater;

        /// <summary>
        /// Блокировщик кода
        /// </summary>
        private readonly object _locker = new object();

        /// <summary>
        /// Создает окно буфера профилей
        /// </summary>
        public ProfileBufferForm()
        {
            InitializeComponent();

            _tImageUpdater = new Timer(G.UpdatePeriodMs * 2) { SynchronizingObject = this };
            _tImageUpdater.Elapsed += ImageUpdater;

            WidthMap = _profileBufferMap.Width;
        }

        public void ImageUpdater(object sender, ElapsedEventArgs e)
        {
            //_tImageUpdater.DoEventWithTimerPause(() =>

            //Выключение таймера
            try
            {
                _tImageUpdater.Enabled = false;
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
                    lock (_locker)
                    {
                        _profileBufferMap.Image = _currentImage;
                        _profileBufferMap.Update();
                    }
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }

            //Включение таймера
            try
            {
                _tImageUpdater.Enabled = true;
            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
        }

        private void ProfileBufferForm_Load(object sender, EventArgs e)
        {
            if (G.Settings.DebugMode)
                Text += $@" (Поток №{Thread.CurrentThread.ManagedThreadId})";

            lock (_locker)
                _currentImage = Properties.Resources.background;

            _tImageUpdater.Enabled = true;


            //G.ProfileBuffer.AddedProfile += AddProfileInGraphic;
            G.ProfileBuffer.AddedProfile += ChangeCurrentImage;


        }

        /// <summary>
        /// Текущая ширина карты
        /// </summary>
        public int WidthMap { get; private set; }

        /// <summary>
        /// Добавляет профиль сверху вниз на PictureBox
        /// </summary>
        public void AddProfile(byte[] profile, ProfileBufferMap bufferMap)
        {
            Bitmap bmp = null;
            BitmapData bmpData = null;

            try
            {
                //Ширина данных критична, все данные должны быть одной ширины, иначе полное обновление
                if (bufferMap.Width != WidthMap)
                {
                    lock (_locker)
                    {
                        _currentImage = Properties.Resources.background;
                        WidthMap = bufferMap.Width;
                    }
                }

                //var width = bufferMap.Width < G.ProfileSize ? bufferMap.Width : G.ProfileSize;
                var width = WidthMap;

                lock (_locker)
                {
                    bmp = _currentImage.Clone(new Rectangle(0, 0, width, G.Settings.ProfileBufferSize),
                        PixelFormat.Format24bppRgb);
                }

                var bmpWidth = bmp.Width;
                var bmpHeight = bmp.Height;

                //ЭТА ФИГНЯ ЗАНИМАЕТ МНОГО ВРЕМЕНИ
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmpWidth, bmpHeight),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb);

                var stride = bmpData.Stride;
                var size = stride * bmpHeight;

                //Последняя строка изображения, в которую кладется текущий профиль
                var line = new byte[stride];

                var j = 0; //Счетчик строки изображения

                for (var cI = 0; cI < bmpWidth; cI++)
                {
                    var i = KUtils.GetTruncateValue(cI / bufferMap.KWidth);

                    //var i = cI;

                    if (i >= profile.Length)
                        i = profile.Length - 1;

                    var point = profile[i];

                    var color = Color.Empty;
                    var isNeed = true;

                    //Возможно точка принадлежит шуму
                    if (0 <= i && i <= G.Left || G.Right <= i && i < G.ProfileSize)
                    {
                        color = G.Settings.NoiseColor;
                        isNeed = false;
                    }

                    //Возможно точка принадлежит исключаемой зоне
                    foreach (var excludedZone in G.ExcludedZones)
                    {
                        if (excludedZone.LeftX <= i && i <= excludedZone.RightX)
                        {
                            color = excludedZone.FillColor;
                            isNeed = false;

                            break;
                        }
                    }

                    //Если не принадлежит исключаемой зоне или шуму
                    if (isNeed)
                    {
                        color = KUtils.GetColorFromValue(point,
                            0,
                            255,
                            G.Settings.DarkAreaColor,
                            G.Settings.LightAreaColor);
                    }

                    line[j] = color.B;
                    line[j + 1] = color.G;
                    line[j + 2] = color.R;

                    j += 3;
                }


                lock (_locker)
                {
                    //Сверху вниз - нормально
                    if (true)
                    {
                        KUtils.CopyMemory(IntPtr.Add(bmpData.Scan0, stride), bmpData.Scan0, (uint)(size - stride));
                        Marshal.Copy(line, 0, bmpData.Scan0, stride);
                    }
                    ////Снизу вверх - имеет артефакты при изменении размера области, решается сбросом данных при смене размера
                    //else
                    //{
                    //    Win32.CopyMemory(bmpData.Scan0, IntPtr.Add(bmpData.Scan0, stride), (uint)(size - stride));
                    //    Marshal.Copy(line, 0, IntPtr.Add(bmpData.Scan0, size - stride), stride);
                    //}
                }

            }
            catch (Exception exc)
            {
                G.Logger.Error(exc.ToString());
            }
            finally
            {
                //Освобождаем картинку от блокировки в памяти если это требуется
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                lock (_locker)
                    _currentImage = bmp;
            }
        }

        private async void ChangeCurrentImage(object sender, AddedProfileEventArgs e) => 
            //await Task.Run(() => AddProfile(e.Profile, _profileBufferMap));
            AddProfile(e.Profile, _profileBufferMap);

        //private void AddProfileInGraphic(byte[] profile)
        //{
        //    var sw = new Stopwatch();
        //    if (Time.IsNeed) sw.Start();

        //    this.TryInvokeIfRequired(() =>
        //    {
        //            //Блокируем смену размера пока строчка рисуется
        //            //FormBorderStyle = FormBorderStyle.FixedToolWindow;
        //            _profileBufferMap.DrawProfile(profile);
        //            //FormBorderStyle = FormBorderStyle.SizableToolWindow;
        //        });

        //    sw.Stop();
        //    if (Time.IsNeed) Time.AddRowsBufferForm = sw.Elapsed.TotalMilliseconds;
        //}

        //private async void AddProfileInGraphic(object sender, AddedProfileEventArgs addedProfileEventArgs) =>
        //    await Task.Run(() => AddProfileInGraphic(addedProfileEventArgs.Profile));
        //    //AddProfileInGraphic(addedProfileEventArgs.Profile);

        private void profileBufferMap_SizeChanged(object sender, EventArgs e)
        {
            //_profileBufferMap.UpdateK();

            //lock (_locker)
            //    _currentImage = Properties.Resources.background;

            ////При смене размера нужно обновить картинку фона, чтобы не было ошибок
            //_profileBufferMap.Image = Properties.Resources.background;
        }

        private void RowsBufferForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //G.ProfileBuffer.AddedProfile -= AddProfileInGraphic;
            G.ProfileBuffer.AddedProfile -= ChangeCurrentImage;
            Enabled = false;
            Thread.Sleep(500);

            G.Logger.Info($"{nameof(ProfileBufferForm)}: Пользователь закрыл окно");
        }

        #region Смена размера окна

        private void ProfileBufferForm_ResizeBegin(object sender, EventArgs e) => KUtils.SuspendPainting(this);

        private void ProfileBufferForm_ResizeEnd(object sender, EventArgs e) => KUtils.ResumePainting(this);

        #endregion
    }
}
