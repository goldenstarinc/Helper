using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;

namespace PostingService
{
    public static class PostRemover
    {
        /// <summary>
        /// Класс, ответственный за изменение опубликованного поста
        /// </summary>
        public static void RemovePost(int id)
        {
            string query = "DELETE FROM posts WHERE id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }
    }
}
