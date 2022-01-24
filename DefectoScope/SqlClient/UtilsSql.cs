using System;
using System.Data.SqlClient;

namespace DefectoScope
{
    public static class UtilsSql
    {
        /// <summary>
        /// Пытается открыть SQL соединение
        /// </summary>
        /// <param name="sqlConnection">Соединение</param>
        /// <returns>Успех?</returns>
        public static bool TryOpen(SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Open();
                return true;
            }
            catch (Exception e)
            {
                G.Logger.Error(e.ToString());
                return false;
            }
        }
    }
}