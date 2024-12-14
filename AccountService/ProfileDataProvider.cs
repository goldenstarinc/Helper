using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using UtilitiesLibrary;

namespace AccountService
{
    /// <summary>
    /// Модуль, хранящий данные о текущем пользователе
    /// </summary>
    public static class ProfileDataProvider
    {
        public static Dictionary<string, object> userData { get; set; }

        /// <summary>
        /// Метод, записывающий в словарь данные пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public static void GetCurrentUserData(int id)
        {
            userData = new Dictionary<string, object>();

            if (id == 0) throw new Exception("Не выполнен вход в систему.");

            string query = @"
                            SELECT u.id, u.username, u.phonenumber, r.rolename
                            FROM users u
                            LEFT JOIN roles r ON u.roleid = r.id
                            WHERE u.id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Если есть хоть одна строка с данными
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                // Заносим данные в словарь
                                userData.Add(reader.GetName(i), reader.GetValue(i));
                            }
                        }
                    }
                }
            });
        }
    }
}
