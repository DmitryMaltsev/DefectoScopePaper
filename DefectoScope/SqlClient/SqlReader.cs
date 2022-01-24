using System;
using System.Data.SqlClient;
using System.Threading;
using Kogerent;
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace DefectoScope
{
    /// <summary>
    /// Считыватель из базы данных
    /// </summary>
    public class SqlReader : IDisposable
    {
        #region Свойства

        /// <summary>
        ///     Строка подключения к базе данных
        /// </summary>
        public string SqlConnectionString { get; }

        /// <summary>
        /// Связь с базой данных в норме?
        /// </summary>
        public bool IsOk { get; private set; } = true;

        /// <summary>
        /// Считыватель в работе?
        /// </summary>
        public bool InWork { get; private set; }

        /// <summary>
        /// Считыватель уничтожен?
        /// </summary>
        public bool IsDisposed { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создает обработчик записи сообшений
        /// </summary>
        public SqlReader(string sqlConnectionString)
        {
            SqlConnectionString = sqlConnectionString;

            //Момент инициализации
            if (G.Settings.InitTestOpenDB)
                TestOpen();
            else
                IsOk = true;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Пытается установить соединение с базой данных, после чего закрывает его
        /// </summary>
        /// <returns></returns>
        public void TestOpen()
        {
            using (var connection = new SqlConnection(SqlConnectionString)) IsOk = UtilsSql.TryOpen(connection);
        }

        /// <summary>
        /// Считывает данных из базы данных в Excel файл
        /// </summary>
        public void CreateReport(DateTime begin, DateTime end, string fileName, bool withTambour, int nTambour = 0)
        {
            try
            {
                InWork = true;

                using (var package = new ExcelPackage())
                {
                    // Добавляем новый лист в пустую книгу
                    var worksheet = package.Workbook.Worksheets.Add("Отчет");

                    //Заголовочные константы
                    //const string cNTambour = "A1";
                    //const string cLength = "A3";
                    const string cNDefects = "D1";
                    const string cNDefectsValue = "D2";
                    const string cNLight = "E1";
                    const string cNLightValue = "E2";
                    const string cNDark = "F1";
                    const string cNDarkValue = "F2";
                    const string cDateTime = "A4";
                    const string cNTambour = "B4";
                    const string cType = "C4";
                    const string cSquare = "D4";
                    const string cXCoord = "E4";
                    const string cYCoord = "F4";

                    //Добавляем заголовки
                    //worksheet.Cells[cNTambour].Value = $"#Тамбур: {nTambour}";

                    worksheet.Cells[cDateTime].Value = "Дата";
                    worksheet.Cells[cNTambour].Value = "#";
                    worksheet.Cells[cType].Value = "Тип";
                    worksheet.Cells[cSquare].Value = "Площадь, мм2";
                    worksheet.Cells[cXCoord].Value = "На ширине, м";
                    worksheet.Cells[cYCoord].Value = "На длине, м";

                    //Устанавливаем курсив на все ячейки заголовка
                    worksheet.Cells["A1:F4"].Style.Font.Italic = true;

                    //Устанавливаем полужирный на все ячейки заголовки столбцов таблицы
                    worksheet.Cells["A4:F4"].Style.Font.Bold = true;

                    //Создаем автофильтр по заголовкам столбцов таблицы
                    worksheet.Cells["A4:F4"].AutoFilter = true;

                    //Стартовая строка для данных
                    var startRow = 5;

                    //Счетчики дефектов
                    int count = 0, countLight = 0, countDark = 0;

                    //Копируем заголовок при переходе на другие страницы
                    worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:4"];
                    worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:F"];

                    //Страничное представление
                    worksheet.View.PageLayoutView = true;

                    //Добавляем имя файла в нижний колонтитул
                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FileName;

                    //Добаляем временной диапазон в нижний колонтитул
                    worksheet.HeaderFooter.OddFooter.CenteredText =
                        $"{begin:yyyy-MM-dd HH:mm} - {end:yyyy-MM-dd HH:mm}";

                    //Добавляем номер страницы в нижний колонтитул и общее количество страниц
                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                        $"{ExcelHeaderFooter.PageNumber} / {ExcelHeaderFooter.NumberOfPages}";

                    //Устанавливаем некоторые свойства документа
                    package.Workbook.Properties.Title = "Отчет";
                    package.Workbook.Properties.Author = "ТБФ";
                    package.Workbook.Properties.Comments =
                        "Отчет из базы данных за определенный срок";

                    //Устанавливаем внешнее свойство документа
                    package.Workbook.Properties.Company = "Когерент";


                    //Устанавливаем связь с базой данных
                    using (var connection = new SqlConnection(SqlConnectionString))
                    {
                        IsOk = UtilsSql.TryOpen(connection);
                        if (!IsOk) return;

                        //Текст команды
                        var commandText = "SELECT * " +
                                          //"[_id]," +
                                          //"[_datetime]," +
                                          //"[_n_smena]," +
                                          //"[_tambur_num]," +
                                          //"[_type]," +
                                          //"[_x_coord]," +
                                          //"[_x_nom]," +
                                          //"[_y_coord]," +
                                          //"[_s]," +
                                          //"[_s_nom]," +
                                          //"[_l]," +
                                          //"[_l_nom]," +
                                          //"[_w]," +
                                          //"[_w_nom]" +
                                          "FROM [_tbf].[dbo].[_data]" +
                                          $"WHERE [_datetime] >= '{begin:yyyyMMdd HH:mm:ss.fff}' AND [_datetime] <='{end:yyyyMMdd HH:mm:ss.fff}'" +
                                          (withTambour ? $"AND [_tambur_num] = {nTambour} " : "") +
                                          "ORDER BY [_datetime] DESC";

                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = commandText;
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var row = startRow + count;

                                    //Это поле всегда NULL в БД
                                    //if (!reader.IsDBNull(6)) Console.WriteLine("7) _x_nom = " + reader.GetDecimal(6));

                                    var data = new SqlMessage(
                                        reader.GetDateTime(1),
                                        reader.GetInt16(2),
                                        reader.GetInt32(3),
                                        reader.GetBoolean(4),
                                        reader.GetDecimal(5),
                                        reader.GetDecimal(7),
                                        reader.GetDecimal(8),
                                        reader.GetDecimal(9),
                                        reader.GetDecimal(10),
                                        reader.GetDecimal(11),
                                        reader.GetDecimal(12),
                                        reader.GetDecimal(13)
                                    );

                                    //Записываем дату
                                    worksheet.Cells[row, 1].Value = data.Timestamp.ToString("yy-MM-dd HH:mm:ss");

                                    //Записываем номер тамбура
                                    worksheet.Cells[row, 2].Value = data.NTambour.ToString();

                                    //Записываем тип брака
                                    string type;
                                    //Если площадь больше 2кв.мм, то это крупный брак
                                    if (data.S > 2)
                                        type = data.Type ? "Дырка" : "Лепешка";
                                    else
                                        type = data.Type ? "Отверстие" : "Включение";
                                    //Хрен знает зачем, но так было..
                                    if ((float)data.XCoord < 0.02) type = "Трещина";
                                    worksheet.Cells[row, 3].Value = type;

                                    //Записываем площадь
                                    worksheet.Cells[row, 4].Value = ((float)data.S).ToString("F2");

                                    //Записываем координаты центра
                                    worksheet.Cells[row, 5].Value = ((float)data.XCoord).ToString("F2"); ;
                                    worksheet.Cells[row, 6].Value = ((float)data.YCoord).ToString("F2"); ;

                                    //Инкрементируем счетчики
                                    if (data.Type)
                                        countLight++;
                                    else
                                        countDark++;
                                    count++;
                                }
                            }
                        }
                    }

                    //Заполняем ячейки заголовка на основании считанных данных
                    //worksheet.Cells[cLength].Value = "Общая длина: ";
                    worksheet.Cells[cNDefects].Value = "Количество брака:";
                    worksheet.Cells[cNDefectsValue].Value = $"{count}";
                    worksheet.Cells[cNLight].Value = "Светлого:";
                    worksheet.Cells[cNLightValue].Value = $"{countLight}";
                    worksheet.Cells[cNDark].Value = "Темного:";
                    worksheet.Cells[cNDarkValue].Value = $"{countDark}";

                    //Рисуем границы полученной таблицы
                    using (var range = worksheet.Cells[startRow - 1, 1, count + startRow - 1, 6])
                    {
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                    //Центруем всю таблицу
                    using (var range = worksheet.Cells[1, 1, count + startRow - 1, 6])
                    {
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    //Автоподбор колонок для всех ячеек
                    worksheet.Cells.AutoFitColumns(0);

                    var file = KUtils.GetFileInfo(G.PathToReports, fileName);
                    package.SaveAs(file);
                }
            }
            catch (Exception e) { G.Logger.Error(e.ToString()); throw; }
            finally { InWork = false; }
        }



        #endregion

        public void Dispose()
        {
            //Дожидаемся окончания создания отчета
            while (InWork) Thread.Sleep(1);
            IsDisposed = true;
        }
    }
}