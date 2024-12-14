using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;

namespace AccountService
{
    /// <summary>
    /// Класс, ответственный за регистрацию пользователя в системее
    /// </summary>
    public class RegistrationClass
    {

        /// <summary>
        /// Функция внесения нового пользователя в базу данных
        /// </summary>
        /// <param name="userName">Имя нового пользователя</param>
        /// <param name="hashedPassword">Пароль нового пользователя</param>
        /// <param name="role">Роль нового пользователя</param>
        public static void SignUp(string userName, string hashedPassword, string phoneNumber, int role = 2)
        {
            NameValidator validator = new NameValidator();

            if (!validator.IsValid(userName))
            {
                throw new Exception("Некорректное имя.");
            }
            else if (CheckIfUserExists(userName))
            {
                throw new Exception("Пользователь с таким именен уже существует.");
            }


            string query = "INSERT INTO users (username, hashedpass, phonenumber, roleid) VALUE" +
                           "(@username, @hashedpass, @phonenumber, (SELECT id FROM roles WHERE rolename = 'guest'));";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@username", userName);
                    command.Parameters.AddWithValue("@hashedpass", hashedPassword);
                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);
                    command.Parameters.AddWithValue("@role", role);

                    command.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// Функция для проверки наличия пользователя с заданным именем в базе данных
        /// </summary>
        /// <param name="userName">Имя пользователя для поиска</param>
        /// <returns>
        /// True - если пользователь с данным именем был найден, иначе - false
        /// </returns>
        public static bool CheckIfUserExists(string userName)
        {
            bool userExists = false;

            string query = "SELECT COUNT(*) FROM users WHERE username = @username";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@username", userName);
                    int count = Convert.ToInt16(command.ExecuteScalar());

                    // если пользователь с таким именем есть в базе данных
                    userExists = count > 0;
                }
            });

            return userExists;
        }
    }
}
