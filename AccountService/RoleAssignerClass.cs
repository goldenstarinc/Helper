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
    /// Класс, содержащий логику выдачи пользователю роли гостя
    /// </summary>
    public class RoleAssigner
    {
        /// <summary>
        /// Метод, присваивающий юзеру роль
        /// </summary>
        public void AssignRole(string roleName)
        {
            int id = CurrentSession.CurrentUserId;

            string query = "UPDATE users\n" +
                           "SET roleid = (SELECT id FROM roles WHERE rolename = @roleName)\n" +
                           "WHERE id = @id";


            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@roleName", roleName);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }
    }
}
