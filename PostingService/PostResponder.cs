using MySqlConnector;
using PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;

namespace PostingService
{
    /// <summary>
    /// Класс, отвечающий за отклик на пост
    /// </summary>
    public static class PostResponder
    {
        public delegate void ResponseSentEventHandler(Notification notification);

        public static event ResponseSentEventHandler ResponseSent;

        /// <summary>
        /// Отправить заявку
        /// </summary>
        /// <param name="post_id">Id поста</param>
        public static void SendResponse(int post_id)
        {
            int id = CurrentSession.CurrentUserId;

            if (id == 0) throw new Exception("Не выполнен вход в аккаунт.");
            else if (CheckIfRespondExists(id, post_id)) throw new Exception("Вы уже откликались на этот пост.");


            string query = "INSERT INTO responses (post_id, responder_id) VALUES\n" +
                           "(@post_id, @id);";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("post_id", post_id);
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            });

            Notification notification = new Notification(id, FinderUtilityClass.FindPostAuthor(post_id), post_id, DateTime.Now);
            ResponseSent?.Invoke(notification);
        }

        /// <summary>
        /// Отозвать заявку
        /// </summary>
        /// <param name="post_id">Id поста</param>
        public static void UnsendResponse(int post_id)
        {
            int id = CurrentSession.CurrentUserId;

            if (id == 0) throw new Exception("Не выполнен вход в аккаунт.");
            else if (!CheckIfRespondExists(id, post_id)) throw new Exception("Отклика не было.");

            string query = "DELETE FROM responses\n" +
                           "WHERE post_id = @post_id AND responder_id = @responder_id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@post_id", post_id);
                    command.Parameters.AddWithValue("@responder_id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// Функция для проверки наличия отклика на определенный пост указанным пользователем
        /// </summary>
        /// <param name="responder_id">Имя пользователя</param>
        /// <param name="post_id">Id поста</param>
        /// <returns>
        /// True - если отклик сущестует, иначе - false
        /// </returns>
        public static bool CheckIfRespondExists(int responder_id, int post_id)
        {
            bool respondExists = false;

            string query = "SELECT COUNT(*) FROM responses WHERE post_id = @post_id AND responder_id = @responder_id";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@post_id", post_id);
                    command.Parameters.AddWithValue("@responder_id", responder_id);
                    int count = Convert.ToInt16(command.ExecuteScalar());

                    // если пользователь с таким именем есть в базе данных
                    respondExists = count > 0;
                }
            });

            return respondExists;
        }
    }
}
