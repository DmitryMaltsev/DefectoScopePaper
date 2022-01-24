using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Kogerent;

namespace DefectoScope
{
    public static class Calibration
    {
        #region Константы

        /// <summary>
        /// Папка калибровок
        /// </summary>
        public const string CalibrationFolder = "\\Calibration\\";

        /// <summary>
        /// Файл калибровки по умолчанию
        /// </summary>
        public const string DefCalibrationFileName = "default";

        #endregion

        #region Поля

        /// <summary>
        /// Пороговый массив
        /// </summary>
        private static byte[] _threshold = new byte[0];

        public static EventHandler ThresholdChanged;

        #endregion

        #region Свойства

        /// <summary>
        /// Путь к калибровкам
        /// </summary>
        public static string PathToCalibration { get; } = KUtils.GetStartUpFolder(CalibrationFolder);

        /// <summary>
        /// Возвращает допуск светлого
        /// </summary>
        public static byte[] ToleranceLight { get; private set; } = new byte[0];

        /// <summary>
        /// Возвращает допуск темного
        /// </summary>
        public static byte[] ToleranceDark { get; private set; } = new byte[0];

        /// <summary>
        /// Возвращает и задает пороговый массив
        /// </summary>
        public static byte[] Threshold
        {
            get => _threshold;

            set
            {
                _threshold = value;
                ToleranceLight = GetTolerance(G.Settings.ToleranceLight);
                ToleranceDark = GetTolerance(-G.Settings.ToleranceDark);
                ThresholdChanged?.Invoke(null, null);
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Калибровка в порядке?
        /// </summary>
        /// <returns></returns>
        public static bool IsOk() => Threshold.Length == G.ProfileSize;

        /// <summary>
        /// Возвращает список доступных на данный момент файлов калибровки, только имена без пути и расширения
        /// </summary>
        /// <returns></returns>
        public static object[] GetCalibrationFileNames()
        {
            var dir = new DirectoryInfo(PathToCalibration);
            // ReSharper disable once CoVariantArrayConversion
            return dir.GetFiles().Select(file => Path.GetFileNameWithoutExtension(file.FullName)).ToArray();
        }

        /// <summary>
        /// Возвращает текущее имя файла калибровки
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentCalibrationFileName()
        {
            var result = $"{G.Settings.Id}_{G.ProfileSize.ToString()}_";
            //foreach (var sensor in G.Sensors) result += $"{sensor.Settings.Exposition}_";
            result += $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
            //result = result.TrimEnd('_');
            return result;
        }

        /// <summary>
        /// Возвращает путь к файлу калибровки
        /// </summary>
        /// <param name="name">Имя файла калибровки</param>
        /// <returns></returns>
        public static string GetPathToCalibrationFile(string name = "CurrentCalibration") => Path.Combine(PathToCalibration, $"{name}.bin");

        /// <summary>
        /// Возвращает профиль допуска
        /// </summary>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        private static byte[] GetTolerance(int tolerance)
        {
            var t = new byte[G.ProfileSize];
            for (var i = 0; i < Threshold.Length; i++)
            {
                var value = KUtils.GetRoundValue(Threshold[i] + 256 * tolerance / 100f);
                //var value = KUtils.GetRoundValue(Threshold[i] + Threshold[i] * tolerance / 100f);
                if (value < 0) value = 0;
                if (value > 255) value = 255;
                t[i] = (byte)value;
            }

            return t;
        }

        #endregion

        #region Работа с калибровкой

        /// <summary>
        /// Сохраняет текущую калибровку в файл
        /// </summary>
        /// <param name="threshold">Пороговый массив. NULL = текущий.</param>
        /// <param name="file">Имя файла калибровки. NULL = текущее имя калибровки</param>
        public static void SaveCalibration(byte[] threshold = null, string file = null)
        {
            if (threshold == null) threshold = Threshold;
            if (file == null) file = GetCurrentCalibrationFileName();

            var path = GetPathToCalibrationFile(file);

            using (var fs = new FileStream(path, FileMode.Create))
                new BinaryFormatter().Serialize(fs, threshold);
        }

        /// <summary>
        /// Пытается сохранить текущую калибровку в файл
        /// </summary>
        /// <param name="threshold">Пороговый массив. NULL = текущий.</param>
        /// <param name="file">Имя файла калибровки. NULL = текущее имя калибровки</param>
        /// <returns>Успех?</returns>
        public static bool TrySaveCalibration(byte[] threshold = null, string file = null)
        {
            try
            {
                SaveCalibration(threshold, file);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Открывает текущую калибровку из файла
        /// </summary>
        public static void OpenCalibration(string file)
        {
            var path = GetPathToCalibrationFile(file);

            using (var fs = new FileStream(path, FileMode.Open))
                Threshold = (byte[])new BinaryFormatter().Deserialize(fs);
        }

        /// <summary>
        /// Пытается открыть текущую калибровку из файла
        /// </summary>
        /// <returns>Успешное выполнение?</returns>
        public static bool TryOpenCalibration(string file)
        {
            try
            {
                OpenCalibration(file);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Пытается считать калибровку
        /// </summary>
        public static void LoadCalibration()
        {
            if (!TryOpenCalibration(G.Settings.CalibrationFileName))
            {
                MessageBox.Show(
                    @"Калибровка не была получена! Необходимо создать файл калибровки и выбрать его для текущей конфигурации.", @"Ошибка считывания калибровки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                G.Logger.Error($@"{nameof(Calibration)}: Калибровка не была получена!");

                var threshold = new byte[G.ProfileSize];
                threshold.Init((byte)(byte.MaxValue / 2));
                Threshold = threshold;
            }
            else if (Threshold.Length != G.ProfileSize)
            {
                MessageBox.Show(
                    @"Калибровка некорректна для текущей конфигурации! Выберите в настройках подходящий файл калибровки.", @"Неверная калибровка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                G.Logger.Error($@"{nameof(Calibration)}: Калибровка некорректна!");

                var threshold = new byte[G.ProfileSize];
                threshold.Init((byte)(byte.MaxValue / 2));
                Threshold = threshold;
            }
        }

        #endregion


    }
}