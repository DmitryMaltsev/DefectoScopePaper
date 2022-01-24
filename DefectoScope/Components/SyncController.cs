using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace DefectoScope
{
    /// <summary>
    /// Регистры контроллера
    /// </summary>
    public enum Reg
    {
        EncoderStatus,
        EncoderConfig,
        EncoderControl,
        EncoderDivider,
        EncoderPosition,
        EncoderSavedPosition,
        EncoderCountImpulse,
        EncoderMaxImpulse,
        SimulationCountImpulse,
        SimulationMaxImpulse,
        SimulationTics,
        SimulationPeriod,
        SwitchControl,
        PortP5,
        PortP6,
        EncoderTotalInterrupt
    };

    /// <summary>
    /// Код завершения операции
    /// </summary>
    public enum Code
    {
        /// <summary>
        /// Нет ошибок
        /// </summary>
        Success = 0,

        /// <summary>
        /// Несоответствие контрольных сумм
        /// </summary>
        BadCRC = -1,

        /// <summary>
        /// Неизвестный тип посылки
        /// </summary>
        UnknownType = -2,

        /// <summary>
        /// Ошибки операции
        /// </summary>
        ErrorOperation = -3,
    }

    /// <summary>
    /// Контроллер синхронизации для работы с энкодером
    /// </summary>
    public class SyncController: IDisposable
    {
        #region Константы

        /// <summary>
        /// Скорость передачи по умолчанию
        /// </summary>
        private const int DefBaudRate = 9600;

        /// <summary>
        /// Таймаут чтения (мс)
        /// </summary>
        private const int DefReadTimeout = 100;

        /// <summary>
        /// Адрес контроллера синхронизации
        /// </summary>
        private const byte SyncAddress = 1;
        
        #endregion

        #region Поля
        
        /// <summary>
        /// Порт соединения с контроллером
        /// </summary>
        private readonly SerialPort _port;

        #endregion

        #region Свойства

        /// <summary>
        /// Локер
        /// </summary>
        public object Locker = new object();

        /// <summary>
        /// Запущен ли поток данных с энкодера?
        /// </summary>
        public bool Launched { get; private set; }

        /// <summary>
        /// Связь с контроллером синхронизации в норме?
        /// </summary>
        public bool IsOk { get; private set; } = true;

        /// <summary>
        /// Закрывать соединение с портом после каждого обращения?
        /// </summary>
        public bool CloseMode { get; set; } = true;
        
        /// <summary>
        /// Реле 1 установлено? (Зеленая лампа)
        /// </summary>
        public bool SW1 { get; set; }

        /// <summary>
        /// Реле 2 установлено? (Красная лампа + звук)
        /// </summary>
        public bool SW2 { get; set; }

        /// <summary>
        /// На данный момент есть сигнализация?
        /// </summary>
        public bool Alarmed => SW2;

        /// <summary>
        /// Количество стробов (от энкодера).
        /// </summary>
        public ushort EncoderCountImpulse { get; private set; }

        /// <summary>
        /// Имя порта.
        /// </summary>
        public string PortName
        {
            get
            {
                lock (Locker)
                    return _port.PortName;
            }
            set
            {
                SafeClose();
                lock (Locker)
                    _port.PortName = value;
            }
        }

        #endregion

        #region Коды ошибок

        /// <summary>
        /// Заданный код об отсутствии ошибок? (успех?)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsSuccess(int value) => value == (int)Code.Success;

        #endregion

        #region Конструкторы

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="portName">Имя порта</param>
        public SyncController(string portName)
        {
            _port = new SerialPort
            {
                Parity = Parity.None,
                StopBits = StopBits.One,
                PortName = portName,
            };

            //Пытаемся открыть и закрыть соединение
            TestOpen();

            //Останавливаем работу контроллера
            StopController();

            Unknown();

            //Устанавливаем делитель
            SetEncoderDivider(G.Settings.EncoderDivider);

            //Снимаем сигнализацию
            Alarm(false);
        }

        public void Dispose()
        {
            StopController();
            _port?.Dispose();
        }

        #endregion

        #region Открытие / Закрытие порта

        /// <summary>
        /// Закрывает соединение последовательного порта если оно было открыто.
        /// </summary>
        public void SafeClose()
        {
            lock (Locker)
            {
                if (_port.IsOpen)
                    _port.Close();
            }
        }

        /// <summary>
        /// Открывает соединение последовательного порта, если оно было закрыто.
        /// </summary>
        public void SafeOpen()
        {
            lock (Locker)
            {
                if (!_port.IsOpen)
                    _port.Open();
            }
        }

        /// <summary>
        /// Пытается закрыть соединение последовательного порта если оно было открыто.
        /// </summary>
        /// <returns>Соединение закрыто успешно?</returns>
        public bool TrySafeClose()
        {
            try
            {
                SafeClose();
                return true;
            }
            catch
            {
                //G.Logger.Error(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Пытается открыть соединение последовательного порта, если оно было закрыто.
        /// </summary>
        /// <returns>Соединение открыто успешно?</returns>
        public bool TrySafeOpen()
        {
            try
            {
                SafeOpen();
                return true;
            }
            catch
            {
                //G.Logger.Error(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Тестирует открытие соединения, после чего закрывает его.
        /// </summary>
        /// <returns>Соединение успешно открывается?</returns>
        public bool TestOpen()
        {
            var result = TrySafeOpen();
            TrySafeClose();

            IsOk = result;
            return result;
        }

        #endregion

        #region Контрольная сумма

        private readonly byte[] _crcTable =
        {
            0, 94, 188, 226, 97, 63, 221, 131, 194, 156, 126, 32, 163, 253, 31, 65,
            157, 195, 33, 127, 252, 162, 64, 30, 95, 1, 227, 189, 62, 96, 130, 220,
            35, 125, 159, 193, 66, 28, 254, 160, 225, 191, 93, 3, 128, 222, 60, 98,
            190, 224, 2, 92, 223, 129, 99, 61, 124, 34, 192, 158, 29, 67, 161, 255,
            70, 24, 250, 164, 39, 121, 155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
            219, 133, 103, 57, 186, 228, 6, 88, 25, 71, 165, 251, 120, 38, 196, 154,
            101, 59, 217, 135, 4, 90, 184, 230, 167, 249, 27, 69, 198, 152, 122, 36,
            248, 166, 68, 26, 153, 199, 37, 123, 58, 100, 134, 216, 91, 5, 231, 185,
            140, 210, 48, 110, 237, 179, 81, 15, 78, 16, 242, 172, 47, 113, 147, 205,
            17, 79, 173, 243, 112, 46, 204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
            175, 241, 19, 77, 206, 144, 114, 44, 109, 51, 209, 143, 12, 82, 176, 238,
            50, 108, 142, 208, 83, 13, 239, 177, 240, 174, 76, 18, 145, 207, 45, 115,
            202, 148, 118, 40, 171, 245, 23, 73, 8, 86, 180, 234, 105, 55, 213, 139,
            87, 9, 235, 181, 54, 104, 138, 212, 149, 203, 41, 119, 244, 170, 72, 22,
            233, 183, 85, 11, 136, 214, 52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
            116, 42, 200, 150, 21, 75, 169, 247, 182, 232, 10, 84, 215, 137, 107, 53
        };

        /// <summary>
        /// Вычисляет контрольную сумму
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        private byte CalcCrc(IReadOnlyList<byte> ptr, int cnt)
        {
            byte crc = 0;
            for (var i = 0; i < cnt; i++)
                crc = _crcTable[crc ^ ptr[i]];
            return crc;
        }

        #endregion

        #region Чтение / Запись данных контроллера

        /// <summary>
        /// Считывает данные из регистра контроллера
        /// </summary>
        /// <param name="reg">Номер регистра</param>
        /// <param name="value">Считанные данные</param>
        /// <returns></returns>
        private int ReadReg(byte reg, out ushort value)
        {
            var buffer = new byte[16];
            buffer[0] = SyncAddress;
            buffer[1] = 0x03;   //Код чтения
            buffer[2] = reg;
            buffer[3] = CalcCrc(buffer, 3);

            int n;
            lock (Locker)
            {
                SafeOpen();
                _port.Write(buffer, 0, 4);
                Thread.Sleep(50);
                n = _port.Read(buffer, 0, 6);

                if (CloseMode)
                    SafeClose();
            }

            if (n != 6)
            {
                value = 0;

                return (int)Code.UnknownType;
            }

            if (buffer[5] == CalcCrc(buffer, 5))
            {
                value = (ushort)(buffer[3] << 8 | buffer[4]);

                return (int)Code.Success;
            }

            value = 0;

            return (int)Code.BadCRC;
        }

        /// <summary>
        /// Считывает данные из регистра контроллера
        /// </summary>
        /// <param name="reg">Регистр</param>
        /// <param name="value">Считанные данные</param>
        /// <returns></returns>
        private int ReadReg(Reg reg, out ushort value) => ReadReg((byte)reg, out value);

        /// <summary>
        /// Записывает данные в регистр контроллера
        /// </summary>
        /// <param name="reg">Номер регистра</param>
        /// <param name="value">Данные</param>
        /// <returns></returns>
        private int WriteReg(byte reg, ushort value)
        {
            var buffer = new byte[16];

            buffer[0] = SyncAddress;
            buffer[1] = 0x06;     //Код записи
            buffer[2] = reg;
            buffer[3] = (byte)(value >> 8);
            buffer[4] = (byte)(value & 0xff);
            buffer[5] = CalcCrc(buffer, 5);

            int n;
            lock (Locker)
            {
                SafeOpen();
                _port.Write(buffer, 0, 6);
                Thread.Sleep(50);
                n = _port.Read(buffer, 0, 6);

                if (CloseMode)
                    SafeClose();
            }

            if (n != 6) return (int)Code.UnknownType;
            if (buffer[5] == CalcCrc(buffer, 5)) return (int)Code.Success;
            return (int)Code.BadCRC;
        }

        /// <summary>
        /// Записывает данные в регистр контроллера
        /// </summary>
        /// <param name="reg">Регистр</param>
        /// <param name="value">Данные</param>
        /// <returns></returns>
        private int WriteReg(Reg reg, ushort value) => WriteReg((byte)reg, value);

        #endregion

        #region Внутренние команды контроллера

        /// <summary>
        /// Запускает работу контроллера
        /// </summary>
        /// <returns></returns>
        private int WriteStart() => WriteReg(Reg.EncoderControl, 1);

        /// <summary>
        /// Останавливает работу контроллера
        /// </summary>
        /// <returns></returns>
        private int WriteStop() => WriteReg(Reg.EncoderControl, 0);

        /// <summary>
        /// На обычном контроллере этот регистр отвечает за период импульсов синхронизации, а тут не знаю
        /// </summary>
        /// <returns></returns>
        private int WriteUnknown() => WriteReg(Reg.EncoderConfig, 0x0100);

        /// <summary>
        /// Считывает режим из контроллера
        /// </summary>
        /// <returns></returns>
        private int ReadMode()
        {
            var result = ReadReg(Reg.EncoderControl, out var value);
            Launched = value == 1;

            return result;
        }

        /// <summary>
        /// Записывает в контроллер делитель энкодера.
        /// </summary>
        /// <returns></returns>
        private int WriteEncoderDivider() => WriteReg(Reg.EncoderDivider, G.Settings.EncoderDivider);

        /// <summary>
        /// Записывает в контроллер состояния реле SW.
        /// </summary>
        /// <returns></returns>
        private int WriteSW()
        {
            ushort value = 0;
            if (SW1) value |= 1;
            if (SW2) value |= 2;
            return WriteReg(Reg.SwitchControl, value);
        }

        /// <summary>
        /// Считывает количество стробов из контроллера (от энкодера).
        /// </summary>
        /// <returns></returns>
        private int ReadEncoderCountImpulse()
        {
            var result = ReadReg(Reg.EncoderCountImpulse, out var value);
            EncoderCountImpulse = value;

            return result;
        }

        #endregion

        #region Работа с контроллером

        /// <summary>
        ///     Вызывает функцию по стандартному "протоколу"
        /// </summary>
        /// <param name="func"></param>
        /// <returns>Успех?</returns>
        private bool Call(Func<int> func)
        {
            int code;

            try
            {
                code = func.Invoke();
                IsOk = true;
            }
            catch (Exception e)
            {
                if (IsOk)
                    G.Logger.Error(e.ToString());
                code = (int)Code.ErrorOperation;
                IsOk = false;
            }
            //finally
            //{
            //    SafeClose();
            //}

            return IsSuccess(code);
        }

        /// <summary>
        /// Останавливает работу контроллера, если она запущена.
        /// </summary>
        /// <returns></returns>
        public bool StopController()
        {
            var success = Call(ReadMode);

            var count = 10;
            while (count > 0 && Launched)
            {
                success &= Call(WriteStop);
                success &= Call(ReadMode);

                count--;
            }

            if (Launched)
            {
                IsOk = false;
                G.Logger.Error($"{nameof(SyncController)}: Не удалось остановить контроллер");
            }

            return success;
        }

        /// <summary>
        /// Запускаем работу контроллера
        /// </summary>
        /// <returns></returns>
        public bool StartController()
        {
            var success = Call(ReadMode);

            var count = 10;
            while (count > 0 && !Launched)
            {
                success &= Call(WriteStart);
                success &= Call(ReadMode);

                count--;
            }

            if (!Launched)
            {
                IsOk = false;
                G.Logger.Error($"{nameof(SyncController)}: Не удалось запустить контроллер");
            }

            return success;
        }

        public bool Unknown() => Call(WriteUnknown);

        /// <summary>
        /// Записывает в контроллер состояния реле SW.
        /// </summary>
        /// <returns></returns>
        public bool SW() => Call(WriteSW);

        /// <summary>
        /// Управление сигнализацией через реле SW1-2.
        /// </summary>
        /// <returns></returns>
        public bool Alarm(bool enabled = true)
        {
            if (enabled)
            {
                SW1 = false;
                SW2 = true;
            }
            else
            {
                SW1 = true;
                SW2 = false;
            }

            return SW();
        }

        /// <summary>
        /// Задает делитель импульсов для энкодера.
        /// </summary>
        /// <returns></returns>
        public bool SetEncoderDivider(ushort value)
        {
            //Сохраняем старое значение
            var current = G.Settings.EncoderDivider;

            //Изменяем
            G.Settings.EncoderDivider = value;

            var result = Call(WriteEncoderDivider);

            //Возвращаем настройки
            G.Settings.EncoderDivider = current;

            return result;
        }

        /// <summary>
        /// Обновляет счетчик импульсов от энкодера.
        /// </summary>
        /// <returns></returns>
        public bool UpdateCount() => Call(ReadEncoderCountImpulse);

        #endregion
    }
}
