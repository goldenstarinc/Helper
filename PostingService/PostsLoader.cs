using MySqlConnector;
using PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;
using System.Data.SqlClient;

namespace PostingService
{
    /// <summary>
    /// Класс, ответственный за прогрузку постов
    /// </summary>
    public class PostsLoader
    {
        public List<PostFields> LoadPosts()
        {
            List<PostFields> posts = new List<PostFields>();

            QueryExecutor.ExecuteQuery(() =>
            {
                string query = "SELECT * FROM posts ORDER BY date_posted DESC";

                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PostFields post = new PostFields()
                            {
                                Title = reader.GetString(1),
                                Content = reader.GetString(2),
                                PosterId = reader.GetInt32(3),
                                Location = reader.GetString(4),
                                DatePosted = reader.GetDateTime(5),
                                DateExpired = reader.GetDateTime(6)
                            };
                            posts.Add(post);
                        }
                    }
                }
            });

            return posts;
        }
    }
}
