using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using UtilitiesLibrary;

namespace AccountService
{
    /// <summary>
    /// Класс, ответственный за аутентификацию пользователя в системе
    /// </summary>
    public class AuthenticationClass
    {
        /// <summary>
        /// Вход в учетную запись по паролю
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="hashedPassword">Пароль для проверки</param>
        public static void LogIn(string userName, string hashedPassword)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND hashedpass = @hashedpass";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@username", userName);
                    command.Parameters.AddWithValue("@hashedpass", hashedPassword);

                    int count = Convert.ToInt16(command.ExecuteScalar());

                    if (count == 0) throw new Exception("Неверный пароль.");
                }
            });

            SetCurrentUserId(userName);
        }

        /// <summary>
        /// Выход из аккаунта
        /// </summary>
        public static void LogOut()
        {
            CurrentSession.CurrentUserId = 0;
            ProfileDataProvider.userData = null;
        }

        /// <summary>
        /// Сохраняет id пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        public static void SetCurrentUserId(string userName)
        {
            string query = "SELECT id from users where username = @username";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@username", userName);

                    CurrentSession.CurrentUserId = Convert.ToInt16(command.ExecuteScalar());
                }
            });
        }
    }
}
