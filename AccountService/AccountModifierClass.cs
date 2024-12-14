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
    /// Класс, содержащий логику изменения пароля пользователя
    /// </summary>
    public class AccountPasswordModifier : IModifier<string>
    {
        /// <summary>
        /// Метод, изменяющий пароль пользователя 
        /// </summary>
        /// <param name="newHashedPassword">Новый хэшированный пароль</param>
        public void Modify(string newHashedPassword)
        {
            int id = CurrentSession.CurrentUserId;

            string query = "UPDATE users\n" +
                           "SET hashedPass = @newHashedPassword\n" +
                           "WHERE id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@newHashedPassword", newHashedPassword);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            });
        }
    }

    /// <summary>
    /// Класс, содержащий логику изменения имени пользователя
    /// </summary>
    public class AccountUsernameModifier : IModifier<string>
    {
        /// <summary>
        /// Метод, изменяющий имя пользователя 
        /// </summary>
        /// <param name="newUsername">Новое имя пользователя</param>
        public void Modify(string newUsername)
        {
            if (RegistrationClass.CheckIfUserExists(newUsername)) throw new Exception("Пользователь с таким именем уже существует.");

            int id = CurrentSession.CurrentUserId;

            string query = "UPDATE users\n" +
                           "SET username = @newUsername\n" +
                           "WHERE id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@newUsername", newUsername.Trim());
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            });
        }
    }

    /// <summary>
    /// Класс, содержащий логику изменения телефона пользователя
    /// </summary>
    public class AccountPhoneNumberModifier : IModifier<string>
    {
        /// <summary>
        /// Метод, изменяющий номер телефона пользователя 
        /// </summary>
        /// <param name="newPhoneNumber">Новый номер телефона пользователя</param>
        public void Modify(string newPhoneNumber)
        {
            int id = CurrentSession.CurrentUserId;

            string query = "UPDATE users\n" +
                           "SET phonenumber = @newPhoneNumber\n" +
                           "WHERE id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            });
        }
    }

    /// <summary>
    /// Маркер интерфейса для изменения данных пользователя
    /// </summary>
    public interface IModifier<T>
    {
        void Modify(T newValue);
    }

    public static class AccountRemover
    {
        /// <summary>
        /// Удаляет пользователя из базы данных
        /// </summary>
        /// <param name="hashedPassword">Хэшированный пароль</param>
        public static void RemoveUser(string hashedPassword)
        {
            int id = CurrentSession.CurrentUserId;

            if (id == 0) throw new Exception("Не выполнен вход в систему.");

            if (!CheckPassword(id, hashedPassword)) throw new Exception("Пароль неверен.");

            string query = "DELETE FROM users WHERE id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// Проверяет правильность введенного пароля
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="hashedPassword">Хэшированный пароль</param>
        /// <returns>True - если пароль введен верно, иначе - false</returns>
        public static bool CheckPassword(int id, string hashedPassword)
        {
            string query = "SELECT COUNT(*) FROM users WHERE id = @id AND hashedpass = @hashedpass";
            bool result = false;
            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@hashedpass", hashedPassword);

                    int count = Convert.ToInt16(command.ExecuteScalar());

                    if (count == 1)
                    {
                        result = true;
                    }
                }
            });

            return result;
        }
    }
}
