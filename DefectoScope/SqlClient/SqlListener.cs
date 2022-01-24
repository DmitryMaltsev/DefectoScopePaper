using System;
using System.Data;
using System.Data.SqlClient;

namespace DefectoScope
{
    /// <summary>
    /// Обработчик записи сообшений в базу данных
    /// </summary>
    public class SqlListener
    {
        #region Конструкторы

        /// <summary>
        /// Создает обработчик записи сообшений
        /// </summary>
        public SqlListener(string sqlConnectionString)
        {
            SqlConnectionString = sqlConnectionString;

            ////Момент инициализации
            //if (G.Settings.InitTestOpenDB)
            //    TestOpen();
            //else
            //    IsOk = true;
        }

        #endregion

        #region Свойства

        /// <summary>
        ///     Строка подключения к базе данных
        /// </summary>
        public string SqlConnectionString { get; }

        /// <summary>
        /// Связь с базой данных в норме?
        /// </summary>
        public bool IsOk { get; private set; } = true;

        #endregion

        #region Методы

        ///// <summary>
        ///// Пытается установить соединение с базой данных, после чего закрывает его
        ///// </summary>
        ///// <returns></returns>
        //public void TestOpen()
        //{
        //    using (var connection = new SqlConnection(SqlConnectionString)) IsOk = UtilsSql.TryOpen(connection);
        //}



        /// <summary>
        /// Записывает сообщение в базу данных
        /// </summary>
        /// <param name="message"></param>
        public void Write(SqlMessage message)
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                IsOk = UtilsSql.TryOpen(connection);
                if (!IsOk) return;

                //Текст команды
                const string commandText = "INSERT INTO [_tbf].[dbo].[_data] " +
                                       "(_datetime, _n_smena, _tambur_num, _type, " +
                                       "_x_coord, _y_coord, _s, _s_nom, _l, _l_nom, _w, _w_nom) "
                                       + " values (@datetime, @n_smena, @tambur_num, @type," +
                                       "@x_coord, " +
                                       //"@x_nom, " +
                                       "@y_coord, @s, @s_nom, @l, @l_nom, @w, @w_nom) ";

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = commandText;

                    //Создаем параметры
                    //var idParam = new SqlParameter("@id", SqlDbType.Int);
                    var timestamp = new SqlParameter("@datetime", SqlDbType.DateTime);
                    var nShift = new SqlParameter("@n_smena", SqlDbType.SmallInt);
                    var nTambour = new SqlParameter("@tambur_num", SqlDbType.Int);
                    var type = new SqlParameter("@type", SqlDbType.Bit);
                    var xCoord = new SqlParameter("@x_coord", SqlDbType.Decimal);
                    //var xNomParam = new SqlParameter("@x_nom", SqlDbType.Decimal);
                    var yCoord = new SqlParameter("@y_coord", SqlDbType.Decimal);
                    var s = new SqlParameter("@s", SqlDbType.Decimal);
                    var sNom = new SqlParameter("@s_nom", SqlDbType.Decimal);
                    var l = new SqlParameter("@l", SqlDbType.Decimal);
                    var lNom = new SqlParameter("@l_nom", SqlDbType.Decimal);
                    var w = new SqlParameter("@w", SqlDbType.Decimal);
                    var wNom = new SqlParameter("@w_nom", SqlDbType.Decimal);

                    //Добавляем параметры в команду
                    //cmd.Parameters.Add(idParam);
                    cmd.Parameters.Add(timestamp);
                    cmd.Parameters.Add(nShift);
                    cmd.Parameters.Add(nTambour);
                    cmd.Parameters.Add(type);
                    cmd.Parameters.Add(xCoord);
                    //cmd.Parameters.Add(xNomParam);
                    cmd.Parameters.Add(yCoord);
                    cmd.Parameters.Add(s);
                    cmd.Parameters.Add(sNom);
                    cmd.Parameters.Add(l);
                    cmd.Parameters.Add(lNom);
                    cmd.Parameters.Add(w);
                    cmd.Parameters.Add(wNom);

                    //Задаем значения параметрам
                    //idParam.Value = 1;
                    timestamp.Value = message.Timestamp;
                    nShift.Value = message.NShift;
                    nTambour.Value = message.NTambour;
                    type.Value = message.Type;
                    xCoord.Value = message.XCoord;
                    //xNomParam.Value = (decimal)1;
                    yCoord.Value = message.YCoord;
                    s.Value = message.S;
                    sNom.Value = message.SNom;
                    l.Value = message.L;
                    lNom.Value = message.LNom;
                    w.Value = message.W;
                    wNom.Value = message.WNom;

                    try
                    {
                        //Пытаемся записать
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        G.Logger.Error(e.ToString());
                        IsOk = false;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }


        }

        #endregion


    }
}