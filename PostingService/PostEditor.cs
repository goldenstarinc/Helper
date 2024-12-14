using MySqlConnector;
using PostingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;

namespace PostingService
{
    /// <summary>
    /// Класс, ответственный за изменение опубликованного поста
    /// </summary>
    public static class PostEditor
    {
        public static void EditPost(PostFields fields, int id)
        {
            string query = "UPDATE posts\n" +
                           "SET title = @title, content = @content, location = @location, date_expired = @date_expired\n" +
                           "WHERE id = @id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@title", fields.Title);
                    command.Parameters.AddWithValue("@content", fields.Content);
                    command.Parameters.AddWithValue("@location", fields.Location);
                    command.Parameters.AddWithValue("@date_expired", fields.DateExpired);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            });
        }
    }
}
