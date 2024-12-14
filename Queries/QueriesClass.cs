using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using UtilitiesLibrary;
using MySqlConnector;

namespace Queries
{
    public class QueriesClass
    {
        /// <summary>
        /// Метод, получающий количество новых сообщений пользователя
        /// </summary>
        /// <returns>Количество новых сообщений пользователя</returns>
        public static int GetNumberOfNewNotifications()
        {
            int id = CurrentSession.CurrentUserId;
            int count = 0;
            string query = "SELECT COUNT(*) FROM notifications WHERE receiverid = @id AND status = @status";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@status", Status.Unread);

                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            });

            return count;
        }
    }
}
