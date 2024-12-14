using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;

namespace PostingService
{
    /// <summary>
    /// Класс, ответственный за создание поста и добавление его в базу данных
    /// </summary>
    public static class PostsCreator
    {
        public static void AddPost(PostFields fields)
        {
            string title = fields.Title;
            string content = fields.Content;
            int poster = fields.PosterId;
            string location = fields.Location;
            DateTime datePosted = fields.DatePosted;
            DateTime dateExpired = fields.DateExpired;

            string query = "INSERT INTO posts (title, content, usernameid, location, date_posted, date_expired) VALUES" +
                           "(@title, @content, @usernameid, @location, @date_posted, @date_expired);";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@content", content);
                    command.Parameters.AddWithValue("@usernameid", poster);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@date_posted", datePosted);
                    command.Parameters.AddWithValue("@date_expired", dateExpired);

                    command.ExecuteNonQuery();
                }
            });
        }
    }
}
