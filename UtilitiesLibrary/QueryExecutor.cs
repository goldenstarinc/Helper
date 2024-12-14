using MySqlConnector;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using System.Data.SqlClient;
namespace UtilitiesLibrary
{
    /// <summary>
    /// Класс, отвечающий за выполнение запроса к базе данных
    /// </summary>
    public static class QueryExecutor
    {
        public static MySqlConnection conn = new MySqlConnection(CurrentSession.ConnectionString);
        public static void ExecuteQuery(Action action)
        {
            if (conn == null) throw new Exception("Соединение не было установлено.");

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            try
            {
                action();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
