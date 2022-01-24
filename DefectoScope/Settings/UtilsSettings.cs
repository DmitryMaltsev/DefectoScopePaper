using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using Kogerent;

namespace DefectoScope
{
    /// <summary>
    ///     Утилиты настроек
    /// </summary>
    public static class UtilsSettings
    {
        /// <summary>
        ///     Возвращает список публичных нестатических свойств (для настроек)
        /// </summary>
        /// <param name="type">Считываемый тип</param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type) => type.GetProperties(
            BindingFlags.Default |
            BindingFlags.IgnoreCase |
            BindingFlags.DeclaredOnly |
            BindingFlags.Instance |
            //BindingFlags.Static |
            BindingFlags.Public |
            //BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy |
            BindingFlags.InvokeMethod |
            BindingFlags.CreateInstance |
            BindingFlags.GetField |
            BindingFlags.SetField |
            BindingFlags.GetProperty |
            BindingFlags.SetProperty |
            BindingFlags.PutDispProperty |
            BindingFlags.PutRefDispProperty |
            BindingFlags.ExactBinding |
            BindingFlags.SuppressChangeType |
            BindingFlags.OptionalParamBinding |
            BindingFlags.IgnoreReturn
        );

        /// <summary>
        ///     Возвращает путь к файлу с настройками
        /// </summary>
        /// <param name="name">Имя файла настроек</param>
        /// <returns></returns>
        public static string GetPathToSettings(string name) => Path.Combine(G.PathToSettings, $"{name}.xml");


        /// <summary>
        ///     Пытается создать <see cref="XDocument" /> из файла XML по указанному пути
        /// </summary>
        /// <param name="path">Путь к файлу XML</param>
        /// <returns></returns>
        public static XDocument TryLoadXDoc(string path)
        {
            XDocument result = null;

            if (File.Exists(path))
            {
                try { result = XDocument.Load(path); }
                catch (Exception e) { G.Logger.Error(e.ToString()); }
            }

            return result;
        }

        /// <summary>
        ///     Ищет дочерний элемент с заданным именем
        /// </summary>
        /// <param name="xElement">Родительский элемент</param>
        /// <param name="name">Имя дочернего элемента</param>
        /// <returns>Элемент или null, если элемент не найден</returns>
        public static XElement FindElement(XElement xElement, string name) =>
            xElement?.Elements().FirstOrDefault(element => element.Name == name);

        /// <summary>
        ///     Получает дочерний элемент с заданным именем. Если не был найден, то будет создан с пустым значением
        /// </summary>
        /// <param name="xElement">Родительский элемент</param>
        /// <param name="name">Имя дочернего элемента</param>
        /// <returns>Элемент был найден?</returns>
        public static XElement GetElement(XElement xElement, string name)
        {
            var result = FindElement(xElement, name);

            if (result == null)
            {
                WriteValueElement(xElement, "", name);
                result = FindElement(xElement, name);
            }

            return result;
        }

        /// <summary>
        ///     Считывает значение дочернего элемента, и если его нет, то создает его
        /// </summary>
        /// <param name="xElement">Элемент-родитель</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="value">Значение элемента</param>
        /// <param name="type">Тип выходных данных</param>
        /// <param name="name">Имя элемента</param>
        /// <param name="defValue">Значение элемента по умолчанию</param>
        /// <returns>Успех?</returns>
        public static bool ReadValueElement(
            XElement xElement,
            string path,
            out object value,
            Type type,
            string name,
            object defValue
        )
        {
            if (xElement != null)
            {
                var element = FindElement(xElement, name);

                if (element != null)
                {
                    //Пытаемся преобразовать написанный в файле текст в нужный нам тип данных
                    return KUtils.ChangeType(element.Value, type, out value);
                }

                G.Logger.Debug($"{nameof(ReadValueElement)}: Создаем параметр {name} по значением по умолчанию.");

                xElement.Add(new XElement(name, defValue.ToString()));
                xElement.Document?.Save(path);
            }

            //Пытаемся преобразовать написанный в файле текст в нужный нам тип данных
            return KUtils.ChangeType(defValue, type, out value);
        }

        /// <summary>
        ///     Записывает значение дочернего элемента. Если такого элемента нет, то будет создан новый со значением
        /// </summary>
        /// <param name="xElement">Элемент-родитель</param>
        /// <param name="value">Значение элемента</param>
        /// <param name="name">Имя элемента</param>
        /// <returns>Файл настроек изменен? (нужно сохранить файл?)</returns>
        public static bool WriteValueElement(XElement xElement, object value, string name)
        {
            //if (typeof(T).IsClass && typeof(T) != typeof(string))
            //    throw new ArgumentException($@"Нельзя прочитать данные с типом {typeof(T)}, разрешены лишь базовые типы!");

            var valueString = value.ToString();

            if (xElement != null)
            {
                var element = FindElement(xElement, name);

                if (element == null)
                {
                    //Если элемент так и не был найден, то создаем элемент и заполняем его значением
                    xElement.Add(new XElement(name, valueString));
                }
                else
                {
                    if (element.Value == valueString) return false;

                    element.Value = valueString;
                    return true;
                }
            }

            return true;
        }

        /// <summary>
        ///     Загружает настройки с указанным идентификатором
        /// </summary>
        /// <param name="s">Загружаемые настройки</param>
        /// <param name="id">Идентификатор, если null, то по умолчанию</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadSettings(ISettings s, string id = null, bool exitOnError = true)
        {
            if (!s.LoadSettingsFromFile(id))
            {
                if (exitOnError)
                {
                    MessageBox.Show(
                        $@"Настройки {s.TypeSettingsMessage} не были получены! Проверьте правильность параметров и перезапустите программу."
                    );
                    G.Logger.Fatal($"{nameof(LoadSettings)}: Настройки {s.TypeSettingsMessage} не были получены!");

                    Program.Close();
                }

                return false;
            }

            return true;
        }

        /// <summary>
        ///     Настройки проходят проверку на логичность?
        /// </summary>
        /// <param name="s">Проверяемые настройки</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool CheckSettings(ISettings s, bool exitOnError = true)
        {
            if (!s.AllIsOk())
            {
                if (exitOnError)
                {
                    MessageBox.Show(
                        $@"Настройки {s.TypeSettingsMessage} не в порядке! Проверьте правильность параметров и перезапустите программу."
                    );
                    G.Logger.Fatal($"{nameof(CheckSettings)}: Настройки {s.TypeSettingsMessage} не в порядке!");

                    Program.Close();
                }

                return false;
            }

            return true;
        }

        /// <summary>
        ///     Загружает настройки с указанным идентификатором и проверяет их на логичность
        /// </summary>
        /// <param name="s">Загружаемые настройки</param>
        /// <param name="id">Идентификатор, если null, то по умолчанию</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadAndCheckSettings(ISettings s, string id = null, bool exitOnError = true) =>
            LoadSettings(s, id, exitOnError) && CheckSettings(s, exitOnError);

        /// <summary>
        ///     Загружает настройки конфигураций
        /// </summary>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadConfigSettings(bool exitOnError = true) =>
            LoadAndCheckSettings(G.ConfigSettings, null, exitOnError);

        /// <summary>
        ///     Загружает настройки системы
        /// </summary>
        /// <param name="s">Загруженные настройки</param>
        /// <param name="id">Идентификатор конфигурации</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadSystemSettings(SystemSettings s, string id, bool exitOnError = true) =>
            LoadAndCheckSettings(s, id, exitOnError);

        /// <summary>
        ///     Загружает основные настройки системы
        /// </summary>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadSystemSettings(bool exitOnError = true) =>
            LoadSystemSettings(G.Settings, G.ConfigSettings.SystemSettings, exitOnError);

        /// <summary>
        ///     Обновляет значение размера профиля
        /// </summary>
        public static void UpdateProfileSize() =>
            G.ProfileSize = G.Sensors.Aggregate(
                0,
                (current, sensor) =>
                    current + sensor.Settings.WidthWindow
            );


        /// <summary>
        ///     Проверяет уникальность IP адресов загруженных настроек
        ///     <param name="sArray">Загруженные настройки, если null, то берутся текущие</param>
        /// </summary>
        /// <returns></returns>
        public static bool CheckUniqueIpLoadedSensors(SensorSettings[] sArray = null)
        {
            var ipArray = sArray?.Select(s => s.Ip).ToArray() ??
                          G.Sensors.Select(sensor => sensor.Settings.Ip).ToArray();
            return ipArray.Length == ipArray.Distinct().ToArray().Length;
        }

        /// <summary>
        ///     Загружает настройки датчиков
        /// </summary>
        /// <param name="sParent">Родительский конфиг настроек</param>
        /// <param name="sArray">Загруженные настройки</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadSensorSettings(
            SystemSettings sParent,
            out SensorSettings[] sArray,
            bool exitOnError = true
        )
        {
            var success = true;

            sArray = new SensorSettings[sParent.NSensors];
            for (byte i = 0; i < sParent.NSensors; i++)
            {
                sArray[i] = new SensorSettings();
                success = success &&
                          LoadAndCheckSettings(sArray[i], sParent.SensorSettings[i], exitOnError);
            }

            return success;
        }

        /// <summary>
        ///     Загружает основные настройки датчиков
        /// </summary>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadSensorSettings(bool exitOnError = true)
        {
            var success = LoadSensorSettings(G.Settings, out var sArray, exitOnError);

            G.Sensors = new Sensor[G.Settings.NSensors];
            for (byte i = 0; i < G.Settings.NSensors; i++)
                G.Sensors[i] = new Sensor((byte)(i + 1), Sensor.SensorMode, sArray[i]);

            //Помимо прочего, необходима проверка на уникальность IP адресов (не фатально)
            var ipIsOk = CheckUniqueIpLoadedSensors(sArray);
            if (!ipIsOk)
            {
                MessageBox.Show(
                    @"IP адреса загружаемых настроек датчиков совпадают! Измените настройки, чтобы добиться уникальности IP адресов."
                );
                G.Logger.Error($"{nameof(LoadSensorSettings)}: IP адреса загружаемых настроек датчиков совпадают!");
            }

            success = success && ipIsOk;

            UpdateProfileSize();

            return success;
        }

        /// <summary>
        ///     Загружает настройки исключаемых зон
        /// </summary>
        /// <param name="sParent">Родительский конфиг настроек</param>
        /// <param name="sArray">Загруженные настройки</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadExcludedZoneSettings(
            SystemSettings sParent,
            out ExcludedZoneSettings[] sArray,
            bool exitOnError = true
        )
        {
            var success = true;

            sArray = new ExcludedZoneSettings[sParent.NExcludedZones];
            for (byte i = 0; i < sParent.NExcludedZones; i++)
            {
                sArray[i] = new ExcludedZoneSettings();
                success = success &&
                          LoadAndCheckSettings(sArray[i], sParent.ExcludedZones[i], exitOnError);
            }

            return success;
        }

        /// <summary>
        ///     Загружает основные настройки исключаемых зон
        /// </summary>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadExcludedZoneSettings(bool exitOnError = true) =>
            LoadExcludedZoneSettings(G.Settings, out G.ExcludedZones, exitOnError);

        /// <summary>
        ///     Загружает настройки типов дефектов
        /// </summary>
        /// <param name="sParent">Родительский конфиг настроек</param>
        /// <param name="sArray">Загруженные настройки</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadTypeDefectSettings(
            SystemSettings sParent,
            out TypeDefectSettings[] sArray,
            bool exitOnError = true
        )
        {
            var success = true;

            sArray = new TypeDefectSettings[sParent.NTypeDefects];
            for (byte i = 0; i < sParent.NTypeDefects; i++)
            {
                sArray[i] = new TypeDefectSettings();
                success = success &&
                          LoadAndCheckSettings(sArray[i], sParent.TypeDefects[i], exitOnError);
            }

            return success;
        }

        /// <summary>
        ///     Загружает основные настройки типов дефектов
        /// </summary>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadTypeDefectSettings(bool exitOnError = true) =>
            LoadTypeDefectSettings(G.Settings, out G.TypeDefects, exitOnError);

        /// <summary>
        ///     Загружает настройки авто-смены
        /// </summary>
        /// <param name="sParent">Родительский конфиг настроек</param>
        /// <param name="sArray">Загруженные настройки</param>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadAutoShiftSettings(
            ConfigSettings sParent,
            out AutoShiftSettings[] sArray,
            bool exitOnError = true
        )
        {
            var success = true;

            sArray = new AutoShiftSettings[sParent.NAutoShifts];
            for (byte i = 0; i < sParent.NAutoShifts; i++)
            {
                sArray[i] = new AutoShiftSettings();
                success = success &&
                          LoadAndCheckSettings(sArray[i], sParent.AutoShiftSettings[i], exitOnError);
            }

            return success;
        }

        /// <summary>
        ///     Загружает основные настройки типов дефектов
        /// </summary>
        /// <param name="exitOnError">Выход из программы по получении ошибок?</param>
        /// <returns>Успех?</returns>
        public static bool LoadAutoShiftSettings(bool exitOnError = true) =>
            LoadAutoShiftSettings(G.ConfigSettings, out G.AutoShifts, exitOnError);
    }
}