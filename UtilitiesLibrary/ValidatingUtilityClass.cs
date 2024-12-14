using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
namespace UtilitiesLibrary
{
    /// <summary>
    /// Класс, содержащий функцию валидации введенного номера телефона
    /// </summary>
    public class PhoneNumberValidator : IDataValidator<string>
    {
        /// <summary>
        /// Метод, проверяющий корректность номера телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <returns>True - если номер телефона прошел валидацию, иначе - false</returns>
        public bool IsValid(string phoneNumber)
        {
            string regexPattern = @"^[+]?[1-9]\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

            Regex regex = new Regex(regexPattern);

            if (!regex.IsMatch(phoneNumber))
            {
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Класс, содержащий функцию валидации введенного имени пользователя
    /// </summary>
    public class NameValidator : IDataValidator<string>
    {
        string[] bannedWords =
        {
            "bitch",
            "ass",
            "fuck",
            "nigger",
            "asshole",
            "moron"
        };

        /// <summary>
        /// Метод, проверяющий корректность имени пользователя
        /// </summary>
        /// <param name="userName">Имя для регистрации</param>
        /// <returns></returns>
        public bool IsValid(string userName)
        {
            Func<string, bool> isBannedWord = bannedWord => userName.ToLower().Contains(bannedWord.ToLower());

            return !bannedWords.Any(isBannedWord);
        }
    }


    /// <summary>
    /// Класс, содержащий функцию проверки наличия роли у пользователя
    /// </summary>
    public class RoleValidator : IDataValidator<string>
    {

        /// <summary>
        /// Метод, проверяющий наличие роли у пользователя
        /// </summary>
        /// <param name="roleName">Роль для проверки</param>
        /// <returns>True - если пользователь обладает ролью, иначе - false</returns>
        public bool IsValid(string roleName)
        {
            int id = CurrentSession.CurrentUserId;

            bool result = false;

            string query = "SELECT r.rolename\n" +
                            "FROM users u\n" +
                            "JOIN roles r ON u.roleid = r.id\n" +
                            "WHERE u.id = @id;";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    string role = Convert.ToString(command.ExecuteScalar());

                    if (role == roleName)
                    {
                        result = true;
                    }
                };
            });

            return result;
        }
    }

    /// <summary>
    /// Маркер интерфейса для валидации данных
    /// </summary>
    interface IDataValidator<T>
    {
        bool IsValid(T valueToValidate);
    }
}
