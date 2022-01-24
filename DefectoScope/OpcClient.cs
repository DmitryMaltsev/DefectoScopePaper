using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

using Timer = System.Timers.Timer;

namespace DefectoScope
{
    /// <summary>
    ///     Клиент для связи с OPC сервером
    /// </summary>
    public class OpcClient : IDisposable
    {

        /// <summary>
        ///     ProgId OPC сервера
        /// </summary>
        public const string OpcProgId = "KEPware.KEPServerEx.V4";

        /// <summary>
        /// Количество плохих считываний значений подряд
        /// </summary>
        public const int BadLimit = 60;

        public OpcClient()
        {
            //Адрес сервера неизменен
            Url = UrlBuilder.Build(OpcProgId);

            ////Пробное подключение
            //ReadDataFromServer();

            //Инициализируем и запускаем таймер обновления
            _tUpdater = new Timer(250);
            _tUpdater.Elapsed += ReadDataFromServer;
            _tUpdater.Start();
        }

        private async void ReadDataFromServer(object sender, ElapsedEventArgs e)
        {
            //await _tUpdater.DoEventWithTimerPauseAsync(() =>

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
                await Task.Run(ReadDataFromServer);
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

        public void Dispose()
        {
            _tUpdater.Elapsed -= ReadDataFromServer;
            _tUpdater.Enabled = false;
            Thread.Sleep(300);
            _tUpdater?.Dispose();
        }

        #region Поля

        /// <summary>
        /// Таймер обновления (считывания) данных с сервера
        /// </summary>
        private readonly Timer _tUpdater;

        /// <summary>
        /// Счетчик плохих считываний
        /// </summary>
        private int _badCount;

        #endregion

        #region Свойства

        /// <summary>
        ///     Адрес сервера
        /// </summary>
        public Uri Url { get; }

        /// <summary>
        /// Количество обращений к серверу
        /// </summary>
        public int NRequests { get; private set; }

        /// <summary>
        ///     Связь с сервером в норме?
        /// </summary>
        public bool IsOk { get; private set; } = true;

        /// <summary>
        /// Данные в порядке?
        /// </summary>
        public bool GoodData => _badCount < BadLimit;

        /// <summary>
        ///     Обрыв бумаги? ("break" с сервера)
        /// </summary>
        public bool Break1 { get; private set; }

        /// <summary>
        ///     Обрыв бумаги? ("break2" с сервера)
        /// </summary>
        public bool Break2 { get; private set; }

        /// <summary>
        ///     Происходит смена тамбура? ("smena_tambura" с сервера)
        /// </summary>
        public bool TambourChange { get; private set; }

        /// <summary>
        /// Не работать?
        /// </summary>
        public bool DontWork => Break1 || Break2 || TambourChange;

        /// <summary>
        ///     Скорость движения полотна, м/мин ("speed" с сервера)
        /// </summary>
        public float Speed { get; private set; }

        /// <summary>
        ///     Плотность полотна, г/м2 ("weight" с сервера)
        /// </summary>
        public float Weight { get; private set; }

        /// <summary>
        ///     Ширина полотна, м ("width" с сервера)
        /// </summary>
        public float Width { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        ///     Создает группу с элементами на сервере
        /// </summary>
        /// <param name="server">Сервер</param>
        /// <param name="group">Группа с элементами на сервере</param>
        /// <returns>Успех?</returns>
        private static bool CreateGroupWithItems(IOpcDaServer server, out OpcDaGroup group)
        {
            group = server.AddGroup("MetsoOPC");
            group.IsActive = true;

            //ID элементов на сервере
            var itemsId = new[]
            {
                "opc.Device1.BDM2.Metso_OPC_KeP.Channel1.MetsoOPC.break", //bool
                "opc.Device1.BDM2.Metso_OPC_KeP.Channel1.MetsoOPC.break2", //bool
                "opc.Device1.BDM2.Metso_OPC_KeP.Channel1.MetsoOPC.smena_tambura", //bool
                "opc.Device1.BDM2.Metso_OPC_KeP.Channel1.MetsoOPC.speed", //float
                "opc.Device1.BDM2.Metso_OPC_KeP.Channel1.MetsoOPC.weight", //float
                "opc.Device1.BDM2.Metso_OPC_KeP.Channel1.MetsoOPC.width" //float
            };

            //Определения элементов на сервере
            var definitions = new OpcDaItemDefinition[itemsId.Length];
            for (var i = 0; i < definitions.Length; i++)
                definitions[i] = new OpcDaItemDefinition { ItemId = itemsId[i], IsActive = true };

            //Добавляем в группу и анализируем результаты добавления
            var results = group.AddItems(definitions);
            foreach (var result in results)
            {
                if (!result.Error.Failed) continue;

                G.Logger.Error($@"{nameof(OpcClient)}: Ошибка добавления элементов {result.Error}");
                return false;
            }

            //Аназируем элементы в группе
            foreach (var item in group.Items)
            {
                if (item.CanonicalDataType != null) continue;

                G.Logger.Error($@"{nameof(OpcClient)}: В группе находится несуществующий элемент {item.ItemId}");
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Обновляет значения переменных заданными (не обновляет если не изменились)
        /// </summary>
        /// <param name="values"></param>
        private void UpdateValues(IReadOnlyList<OpcDaItemValue> values)
        {
            var break1 = (bool)values[0].Value;
            if (values[0].Quality.Master == OpcDaQualityMaster.Good && Break1 != break1)
                Break1 = break1;

            var break2 = (bool)values[1].Value;
            if (values[1].Quality.Master == OpcDaQualityMaster.Good && Break2 != break2)
                Break2 = break2;

            var tambourChange = (bool)values[2].Value;
            if (values[2].Quality.Master == OpcDaQualityMaster.Good && TambourChange != tambourChange)
            {
                TambourChange = tambourChange;

                if (!tambourChange)
                {
                    G.ResetNProfiles();
                    G.SqlClient.NTambour++;
                }
                //OFF: Неадекватное поведение при большой карте...
                //else
                //{
                //    //Во время смены тамбура обновляем карту полным рабочим циклом из опорных данных
                //    var range = UtilsD.GetGyFromGd(G.Settings.MapRange) + G.Settings.ProfileBufferSize * 5 + G.Settings.InputBufferSize;
                //    for (var i = 0; i < range; i++)
                //    {
                //        if (G.InputBuffer.Length == G.InputBuffer.Capacity)
                //            Thread.Sleep(1);

                //        G.InputBuffer.Write(Calibration.Threshold);
                //    }

                //    //Ждем опустошения буфера
                //    while (G.InputBuffer.Length != 0)
                //        Thread.Sleep(1);
                //}
            }

            var speed = (float)values[3].Value;
            if (values[3].Quality.Master == OpcDaQualityMaster.Good && Math.Abs(Speed - speed) > 0.001)
                Speed = speed;

            var weight = (float)values[4].Value;
            if (values[4].Quality.Master == OpcDaQualityMaster.Good && Math.Abs(Weight - weight) > 0.001)
                Weight = weight;

            var width = (float)values[5].Value;
            if (values[5].Quality.Master == OpcDaQualityMaster.Good && Math.Abs(Width - width) > 0.001)
                Width = width;
        }

        /// <summary>
        ///     Считывает данные на сервере
        /// </summary>
        public void ReadDataFromServer()
        {
            try
            {
                using (var server = new OpcDaServer(Url))
                {
                    Thread.Sleep(50);

                    //Пробуем подключиться
                    server.Connect();

                    Thread.Sleep(50);

                    //Создаем и анализируем группу элементов с сервера
                    if (CreateGroupWithItems(server, out var group))
                    {
                        Thread.Sleep(50);

                        //Считываем текущие значения элементов с сервера
                        var values = group.Read(group.Items, OpcDaDataSource.Device);

                        //Обновляем значения переменных считанными данными
                        UpdateValues(values);

                        //Есть плохо считанные данные?
                        var hasBad = values.Any(value => value.Quality.Master != OpcDaQualityMaster.Good);
                        if (hasBad)
                        {
                            _badCount++;

                            if (_badCount == BadLimit)
                                G.Logger.Warn($"{nameof(OpcClient)}: Ошибка чтения, поле плотности имеет статус {values[4].Quality.Status.ToString()}");
                        }
                        else
                            _badCount = 0;

                        IsOk = true;
                    }
                    else
                    {
                        if (IsOk) G.Logger.Warn($"{nameof(OpcClient)}: Ошибка создания группы данных!");
                        IsOk = false;
                    }

                    NRequests++;
                }

            }
            catch (Exception e)
            {
                //Записываем в лог только по первой ошибке
                if (IsOk) G.Logger.Error(e.ToString());

                IsOk = false;
            }
        }

        #endregion
    }
}