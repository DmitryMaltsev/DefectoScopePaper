using System;
using System.Runtime.InteropServices;

namespace DefectoScope
{
    public static class SensorE
    {
        #region Constant declarations
        /// <summary>
        /// Имя Dll
        /// </summary>
        public const string DriverDllName = "SensorE.dll";

        /// <summary>
        /// Что-то устаревшее
        /// </summary>
        public const int UniqueIdDefault = 1;

        /// <summary>
        /// Этот режим не используется
        /// </summary>
        public const bool PreCalculationDefault = false;

        // 0..3  байты - номер профиля 
        // 4..63 байты - резерв

        public const int ProfileNumberIndex = 0;

        /// <summary>
        /// Длина заголовка массива данных профиля
        /// </summary>
        public const int ProfileHeaderLength = 16;
        public const int BeginBodyIndex = 16;

        //Параметры для включения режима блинкования
        /// <summary>
        /// Номер регистра 1
        /// </summary>
        public const int BlinkRegKey1 = 130;

        /// <summary>
        /// Значение для записи в регистр 1
        /// </summary>
        public const int BlinkRegVal1 = 8;

        /// <summary>
        /// Номер регистра 2
        /// </summary>
        public const int BlinkRegKey2 = 70;

        /// <summary>
        /// Значение для записи в регистр 2
        /// </summary>
        public const int BlinkRegVal2 = 2;

        /// <summary>
        /// Регистр для записи инверсии лазера напрямую
        /// </summary>
        public const int InvLaserRegKey = 133;

        /// <summary>
        /// Значение для записи в регистр инверсии лазера напрямую
        /// </summary>
        public const int InvLaserRegVal = 4;

        /// <summary>
        /// Регистр для записи усиления напрямую
        /// </summary>
        public const int AdcRegKey = 103;

        /// <summary>
        /// Регистр для записи усиления PGA напрямую (от 0 до 3)
        /// </summary>
        public const int PgaRegKey = 102;
        #endregion

        #region Return Values
        /// <summary>
        /// Успешное выполнение = 0
        /// </summary>
        public const int Success = 0x00000000;

        /// <summary>
        /// Датчик не проинициализирован = -1
        /// </summary>
        public const int SensorNotInitialize = -0x00000001;

        /// <summary>
        /// Датчик не отвечает = -10
        /// </summary>
        public const int SensorNotRespond = -0x0000000A;
        #endregion

        #region Callback
        //Объявление функций, используемых для получения данных программой в режиме приема видео/профиля/автоэкспозиции
        public delegate void CallbackEventVideoDelegate(IntPtr sender, int mode, IntPtr data, int dataLength);
        public delegate void CallbackEventProfileDelegate(IntPtr sender, int mode, IntPtr data, int dataLength);
        public delegate void CallbackEventAutoExpDelegate(IntPtr sender, ushort exposition);
        #endregion

        #region SetRegisterValue
        /// <summary>
        /// Устанавливает инверсию включения лазера
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns></returns>
        public static int SetInvLaser(IntPtr handle) => SetRegisterValue(handle, InvLaserRegKey, InvLaserRegVal);

        /// <summary>
        /// Устанавливает цифровое усиление
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="value">Значение усиления</param>
        /// <returns></returns>
        public static int SetAdcGain(IntPtr handle, ushort value) => SetRegisterValue(handle, AdcRegKey, value);

        /// <summary>
        /// Устанавливает аналоговое усиление
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="value">Значение усиления</param>
        /// <returns></returns>
        public static int SetPgaGain(IntPtr handle, ushort value) => SetRegisterValue(handle, PgaRegKey, value);

        #endregion

        #region Others

        /// <summary>
        ///     Считывает версию библиотеки SensorE.dll
        /// </summary>
        /// <param name="version">Версия SensorE.dll</param>
        /// <returns>Код ошибки</returns>
        public static int GetVersionDll(out string version)
        {
            //G.Logger.WriteToLog("Вошли в GetVersionDll");

            //Пытаемся считать версию dll из самой dll
            try
            {
                const byte length = 22; //Длина строки с версией

                var versionPtr = Marshal.AllocHGlobal(length); //Выделение памяти и получение указателя
                VersionV2(versionPtr);
                version = Marshal.PtrToStringAnsi(versionPtr); //Преобразование в строку
                Marshal.FreeHGlobal(versionPtr); //Освобождаем память под указателем

                return 0;
            }
            catch
            {
                version = default;
                return -115;    //Код ошибки недоступности файла
            }
        }

        #endregion

        #region Dll Import

        #region Get/Set

        /// <summary>
        /// Получает математические параметры обработки данных
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="data">Математические параметры</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetGlobalMathParam(IntPtr handle, [In, Out] float[] data);

        /// <summary>
        /// Устанавливает математические параметры обработки данных
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="data">Математические параметры</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetGlobalMathParam(IntPtr handle, float[] data);

        /// <summary>
        /// Считывает права ввода/вывода
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="state"></param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetIO(IntPtr handle, out uint state);

        /// <summary>
        /// Устанавливает права ввода/вывода
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="state"></param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetIO(IntPtr handle, byte state);

        /// <summary>
        /// Получает напрямую значение любого регистра
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="reg">Номер регистра</param>
        /// <param name="value">Значение</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetRegisterValue(IntPtr handle, byte reg, out ushort value);

        /// <summary>
        /// Устанавливает напрямую значение любого регистра
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="reg">Номер регистра</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetRegisterValue(IntPtr handle, byte reg, ushort value);

        /// <summary>
        /// Получает параметры профиля
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="data">Параметры</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetSmartProfileParam(IntPtr handle, [In, Out] int[] data);

        /// <summary>
        /// Устанавливает параметры профиля
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="data">Параметры</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetSmartProfileParam(IntPtr handle, int[] data);

        #endregion

        #region Calculate

        /// <summary>
        /// Обсчитывает профиль
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="source">Профиль для обсчета. [i*2] - ширина пикселя i, [i*2+1] - дальность пикселя i</param>
        /// <param name="sLength">Размер массива профиля для обсчета [i*2]</param>
        /// <param name="result">Массив результатов. Его размер должен быть [i*3]</param>
        /// <param name="rLength">Размер массива результатов</param>
        /// <returns>Сколько точек сконвертировано реально</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Calculate(IntPtr handle, float[] source, int sLength, [In, Out] float[] result, int rLength);

        /// <summary>
        /// Обсчитывает профиль
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="source">Профиль для обсчета. [i*2] - ширина пикселя i, [i*2+1] - дальность пикселя i</param>
        /// <param name="sLength">Размер массива профиля для обсчета [i*2]</param>
        /// <param name="result">Массив результатов. Его размер должен быть [i*3]</param>
        /// <param name="rLength">Размер массива результатов</param>
        /// <returns>Сколько точек сконвертировано реально</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Calculate_VIKSA_LENGTH(IntPtr handle, float[] source, int sLength, [In, Out] float[] result, int rLength);

        /// <summary>
        /// Обсчитывает профиль
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="source">Профиль для обсчета. [i*2] - ширина пикселя i, [i*2+1] - дальность пикселя i</param>
        /// <param name="sLength">Размер массива профиля для обсчета [i*2]</param>
        /// <param name="result">Массив результатов. Его размер должен быть [i*3]</param>
        /// <param name="rLength">Размер массива результатов</param>
        /// <returns>Сколько точек сконвертировано реально</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CalculateColumn(IntPtr handle, float[] source, int sLength, [In, Out] float[] result, int rLength);

        /// <summary>
        /// Обсчитывает
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="source">Входной массив для обсчета</param>
        /// <param name="sLength">Размер входного массива для обсчета</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CalculateSimple(IntPtr handle, float[] source, int sLength);

        /// <summary>
        /// Обсчитывает профили формата Коломны
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="source">Исходный массив данных</param>
        /// <param name="sLength">Размер исходного массива данных</param>
        /// <param name="result">Выходной массив данных</param>
        /// <param name="rLength">Размер выходного массива данных</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SleepCalculate(IntPtr handle, float[] source, int sLength, [In, Out] float[] result, int rLength);

        #endregion

        #region Other

        /// <summary>
        /// Обсчитывает антиблик
        /// </summary>
        /// <param name="profile1">Первый профиль для сравнений</param>
        /// <param name="cnt1">Количество точек в первом профиле</param>
        /// <param name="profile2">Второй профиль для сравнения</param>
        /// <param name="cnt2">Количество точек во втором профиле</param>
        /// <param name="sigma">Окно поиска общих точек</param>
        /// <param name="result">Результирующий общий массив</param>
        /// <returns>Количество точек в результирующем профиле</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AntiFlashProfile(float[] profile1, int cnt1, float[] profile2, int cnt2, float sigma, [In, Out] float[] result);

        /// <summary>
        /// УСТАРЕВШАЯ ФУНКЦИЯ. Устанавливает калибровочный режим
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="isMm">Выдача профиля в мм? (иначе в пикселях)</param>
        /// <param name="preCalculation">Нужна прекалькуляция? c v1.6 это устаревший параметр, его значение ни на что не влияет</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CalibrationMode(IntPtr handle, bool isMm, bool preCalculation);

        /// <summary>
        /// Устанавливает калибровочный режим
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="isMm">Выдача профиля в мм? (иначе в пикселях)</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CalibrationModeV2(IntPtr handle, bool isMm);

        /// <summary>
        /// Создает маску коррекции
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="pl"></param>
        /// <param name="cnt">Количество ..</param>
        /// <param name="smoothRate"></param>
        /// <param name="plf"></param>
        /// <param name="filename">Имя файла ..</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateCorrectionMask(IntPtr handle, float[] pl, int cnt, int smoothRate, float[] plf, string filename);

        /// <summary>
        /// Создает быструю таблицу
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="binFileName">Имя bin-файла</param>
        /// <param name="fastTableFileName">Имя файла быстрой таблицы</param>
        /// <param name="typeMatrix">Тип матрицы</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateFastTable(IntPtr handle, string binFileName, string fastTableFileName, int typeMatrix);

        /// <summary>
        /// ФУНКЦИЯ УСТАРЕЛА. Создает объект датчика.
        /// </summary>
        /// <param name="ip">IP-адрес профилометра</param>
        /// <param name="portCmd"></param>
        /// <param name="portData"></param>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="margin">Запас в пикселях, который используется для фильтрации значений по калибровочным данным. Этот параметр признан излишним и его значение ни на что не влияет. Если точка выходит на пределы + запас, то она будет обнулена.</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateSensor(string ip, ushort portCmd, ushort portData, out IntPtr handle, float margin);

        /// <summary>
        /// Создает объект датчика. Обновленная версия функции. Без запаса (его можно задать в математических параметрах) и портов (задаются псевдо-случайным образом)
        /// </summary>
        /// <param name="ip">IP-адрес профилометра</param>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateSensorV2(string ip, out IntPtr handle);

        /// <summary>
        /// Переключает режим отладки с записью в журнал
        /// </summary>
        /// <param name="isDebugMode">Включить режим отладки с записью в журнал?</param>
        /// <returns>Код ошабки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DebugMode(bool isDebugMode);

        /// <summary>
        /// Уничтожает объект датчика и освобождает все занятые ресурсы памяти
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DisposeSensor(IntPtr handle);

        /// <summary>
        /// Получение ..
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="frameCount"></param>
        /// <param name="strobeCount"></param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FrameStrobeCount(IntPtr handle, out int frameCount, out int strobeCount);

        /// <summary>
        /// Получение данных о кадрах в ОЗУ
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="frameCount">Количество кадров</param>
        /// <param name="bufferSize">Размер буфера</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetRamValue(IntPtr handle, out ushort frameCount, out uint bufferSize);

        /// <summary>
        /// Скачивает в буфер кадры из ОЗУ
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="buffer">Буфер для получения данных кадра</param>
        /// <param name="bufferSize">Размер буфера для получения данных кадра (количество пикселей)</param>
        /// <param name="typeMatrix">Тип матрицы</param>
        /// <param name="nFrames">Количество полученных кадров</param>
        /// <param name="error">Данные, оставшиеся в ОЗУ нескаченные (погрешность)</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetRawRamData(IntPtr handle, IntPtr buffer, int bufferSize, int typeMatrix, out int nFrames, out int error);

        /// <summary>
        /// Получает температуру датчика
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="temper">Температура внутри профилометра</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTemper(IntPtr handle, out float temper);

        /// <summary>
        /// Управляет лазером. Если лазер выключен, он не будет включаться в момент экспозиции
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="state">0x1 - включить, 0x0 - выключить</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Laser(IntPtr handle, byte state);

        /// <summary>
        /// Загружает калибровочные данные в датчик
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="calibrationFn">Имя файла с калибровкой</param>
        /// <param name="uniqueId">Номер калибровки в файле, всегда равен 1</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoadCalibration(IntPtr handle, string calibrationFn, int uniqueId);

        /// <summary>
        /// Загружает калибровочные данные в датчик
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="calibrationFn">Имя файла с калибровкой</param>
        /// <param name="uniqueId">Номер калибровки в файле, всегда равен 1</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int LoadCalibrationViksa(IntPtr handle, string calibrationFn, int uniqueId);

        /// <summary>
        /// Загружает маску коррекции
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="filename">Имя файла маски коррекции</param>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LoadCorrectionMask(IntPtr handle, string filename);

        /// <summary>
        /// Считывает значения регистров 158 и 159
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="r158">Значение регистра 158</param>
        /// <param name="r159">Значение регистра 159</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int LVDS(IntPtr handle, out ushort r158, out ushort r159);

        /// <summary>
        /// Следит за контактным проводом
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="state">0x0 - слежение отключено, 0x1 - слежение включено</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int KontaktSearch(IntPtr handle, byte state);

        /// <summary>
        /// Сбрасывает маску коррекции
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetCorrectionMask(IntPtr handle);

        /// <summary>
        /// Отправляет команду профилометру на выдачу одного профиля/кадра, работает только в режиме синхронизации по команде от сервера SyncCmd
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SendSync(IntPtr handle);

        /// <summary>
        /// Устанавливает ..
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="channel">Канал</param>
        /// <param name="value">Значение</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetAnalog(IntPtr handle, uint channel, uint value);

        /// <summary>
        /// Устанавливает ..
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="value">Значение</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetDebounce(IntPtr handle, uint value);

        /// <summary>
        /// Устанавливает экспозицию датчика (24 бита)
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="exp">Время экспозиции</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetExposition(IntPtr handle, uint exp);

        /// <summary>
        /// Устанавливает параметры обработки пятен на кадрах, получаемых с матрицы профилометра
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="maxSpotsCount">Максимальное количество точек на столбце</param>
        /// <param name="threshold">Порог сигнала</param>
        /// <param name="minWidth">Минимальная ширина пятна</param>
        /// <param name="maxWidth">Максимальная ширина пятна</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetParameters(IntPtr handle, ushort maxSpotsCount, ushort threshold, ushort minWidth, ushort maxWidth);

        /// <summary>
        /// Устанавливает параметры вращения профиля
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="onOff"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetRotateParam(IntPtr handle, int onOff, float angle);

        /// <summary>
        /// Устанавливает режим видео/профиля
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="state"></param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetVideoProfileMode(IntPtr handle, byte state);

        /// <summary>
        /// Устанавливает размеры окна
        /// </summary>
        /// <param name="handle">Идентификатор объекта адаптера</param>
        /// <param name="left">Положение левого края окна</param>
        /// <param name="top">Положение верхнего края окна</param>
        /// <param name="width">Ширина окна</param>
        /// <param name="height">Высота окна</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetWindow(IntPtr handle, ushort left, ushort top, ushort width, ushort height);

        /// <summary>
        /// Устанавливает размеры окна
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="data">Данные для установки окна</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetWindowtTvema(IntPtr handle, ushort[] data);

        /// <summary>
        /// Устанавливает режим передачи замеров без подтверждения о приеме по столбцам
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SpotsNoAckColumn(IntPtr handle);

        /// <summary>
        /// Устанавливает режим передачи замеров без подтверждения о приеме по строкам
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SpotsNoAckRow(IntPtr handle);

        /// <summary>
        /// Запускает поток приема данных
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="callbackEventVideo">Возврат видео</param>
        /// <param name="callbackEventProfile">Возврат профиля</param>
        /// <param name="callbackEventAutoExp">Возврат экспозиции</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartListener(IntPtr handle, CallbackEventVideoDelegate callbackEventVideo,
            CallbackEventProfileDelegate callbackEventProfile, CallbackEventAutoExpDelegate callbackEventAutoExp);

        /// <summary>
        /// Останавливает работу профилометра и передачу данных в SensorE.dll
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Stop(IntPtr handle);

        /// <summary>
        /// Останавливает цикл прием данных
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopListener(IntPtr handle);

        /// <summary>
        /// Устанавливает режим синхронизации по команде от сервера
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SyncCmd(IntPtr handle);

        /// <summary>
        /// Устанавливает режим внешней синхронизации
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SyncExt(IntPtr handle);

        /// <summary>
        /// Устанавливает режим синхронизации от стробов
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="strobeMode">Режим строба</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SyncFromStrobe(IntPtr handle, int strobeMode);

        /// <summary>
        /// Устанавливает режим непрерывной работы с заданной частотой
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="frameRate">Частота выдачи профилей/кадров (профили = 0..250, кадры = 0..40)</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SyncNone(IntPtr handle, int frameRate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <param name="result"></param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TempDataRead(IntPtr handle, [In, Out] float[] result);

        /// <summary>
        /// Получает версию библиотеки SensorE.dll
        /// </summary>
        /// <returns>Массив символов версии dll</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern string Version();

        /// <summary>
        /// Получает версию библиотеки SensorE.dll
        /// </summary>
        /// <param name="ptr">Указатель на память, куда запишется версия библиотеки</param>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void VersionV2(IntPtr ptr);

        /// <summary>
        /// Устанавливает режим передачи видео
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Video(IntPtr handle);

        /// <summary>
        /// Устанавливает режим передачи видео через ОЗУ
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns>Код ошибки</returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VideoToRam(IntPtr handle);

        /// <summary>
        /// Направляет данные
        /// </summary>
        /// <param name="handle">Идентификатор объекта датчика</param>
        /// <returns></returns>
        [DllImport(DriverDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteToRAM(IntPtr handle);

        #endregion


        #endregion
    }
}