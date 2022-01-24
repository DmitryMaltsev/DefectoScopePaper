using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

using Timer = System.Timers.Timer;

namespace DefectoScope
{
    /// <summary>
    ///     Клиент для связи с OPC сервером
    /// </summary>
    public class OpcClient: IDisposable
    {
        /// <summary>
        ///     ProgId OPC сервера
        /// </summary>
        public const string OpcProgId = "KEPware.KEPServerEx.V4";

        public OpcDaServer Server;

        public OpcClient()
        {
            //Адрес сервера неизменен
            Url = UrlBuilder.Build(OpcProgId);

            try
            {
                //Устанавливаем соединение лишь один раз
                Server = new OpcDaServer(Url);
                Server.Connect();

                //Пробное подключение
                ReadDataFromServer();

                //Инициализируем и запускаем таймер обновления
                _tUpdater = new Timer(100);
                _tUpdater.Elapsed += ReadDataFromServer;
                _tUpdater.Start();
            }
            catch (Exception e)
            {
                //Записываем в лог только по первой ошибке
                if (IsOk) G.Logger.Error(e.ToString());

                IsOk = false;
            }
        }

        private async void ReadDataFromServer(object sender, ElapsedEventArgs e) => 
            await _tUpdater.DoEventWithTimerPauseAsync(ReadDataFromServer);

        public void Dispose()
        {
            _tUpdater.Enabled = false;
            _tUpdater?.Dispose();
            Server.Dispose();
        }

        #region Поля

        /// <summary>
        /// Таймер обновления (считывания) данных с сервера
        /// </summary>
        private readonly Timer _tUpdater;

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
        public bool IsOk { get; private set; }

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
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.break", //bool
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.break2", //bool
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.smena_tambura", //bool
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.speed", //float
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.weight", //float
                "opc.Device1.BDM2.Metso_OPC_Kep.Channel1.MetsoOPC.width" //float
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

                G.Logger.Error($@"Ошибка добавления элементов: {result.Error}");
                return false;
            }

            //Аназируем элементы в группе
            foreach (var item in group.Items)
            {
                if (item.CanonicalDataType != null) continue;

                G.Logger.Error($@"В группе находится несуществующий элемент: {item.ItemId}");
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
            if (Break1 != break1) Break1 = break1;

            var break2 = (bool)values[1].Value;
            if (Break2 != break2) Break2 = break2;

            var tambourChange = (bool)values[2].Value;
            if (TambourChange != tambourChange)
            {
                TambourChange = tambourChange;

                if (!tambourChange) G.NProfiles = 0;
                else
                {
                    //Во время смены тамбура обновляем карту полным рабочим циклом из опорных данных
                    var range = UtilsD.GetGyFromGd(G.Settings.MapRange) + G.Settings.ProfileBufferSize * 5 + G.Settings.InputBufferSize;
                    for (var i = 0; i < range; i++)
                    {
                        if (G.InputBuffer.Length == G.InputBuffer.Capacity)
                            Thread.Sleep(1);

                        G.InputBuffer.Write(Calibration.Threshold);
                    }

                    //Ждем опустошения буфера
                    while (G.InputBuffer.Length != 0)
                        Thread.Sleep(1);
                }
            }

            var speed = (float)values[3].Value;
            if (Math.Abs(Speed - speed) > 0.001) Speed = speed;

            var weight = (float)values[4].Value;
            if (Math.Abs(Weight - weight) > 0.001) Weight = weight;

            var width = (float)values[5].Value;
            if (Math.Abs(Width - width) > 0.001) Width = width;
        }

        /// <summary>
        ///     Считывает данные на сервере
        /// </summary>
        public void ReadDataFromServer()
        {
            try
            {
                //Создаем и анализируем группу элементов с сервера
                if (CreateGroupWithItems(Server, out var group))
                {
                    //Считываем текущие значения элементов с сервера
                    var values = group.Read(group.Items, OpcDaDataSource.Device);

                    //Обновляем значения переменных считанными данными
                    UpdateValues(values);

                    NRequests++;
                    IsOk = true;
                }
                //else ошибка
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