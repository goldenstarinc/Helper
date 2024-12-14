using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;

namespace PostService
{
    public static class FinderUtilityClass
    {
        /// <summary>
        /// Метод, ищущий автора поста по id
        /// </summary>
        /// <param name="post_id">id поста</param>
        public static int FindPostAuthor(int post_id)
        {
            int id = -1;

            string query = "SELECT usernameid FROM posts\n" +
                           "WHERE id = @post_id;";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@post_id", post_id);
                    id = Convert.ToInt32(command.ExecuteScalar());

                    if (id == 0) throw new Exception("Такого поста не существует.");
                }
            });

            return id;
        }

        /// <summary>
        /// Метод, возвращающий имя пользователя по id
        /// </summary>
        public static string GetUsernameByID(int id)
        {
            string query = "SELECT username FROM users WHERE id = @id;";

            string username = null;

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        username = Convert.ToString(result);
                    }
                }
            });

            return username;
        }

        /// <summary>
        /// Метод, возвращающий имя пользователя по id
        /// </summary>
        public static string GetPostTitleById(int id)
        {
            string query = "SELECT title FROM posts WHERE id = @id;";

            string title = null;

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        title = Convert.ToString(result);
                    }
                }
            });

            return title;
        }
    }
}
