using MySqlConnector;
using PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UtilitiesLibrary;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using PostingService;

namespace NotificationService
{
    /// <summary>
    /// Класс, ответственный за проверку новых уведомлений
    /// </summary>
    public class NotificationsChecker
    {
        public List<Notification> Notifications { get; private set; }
        public NotificationsChecker()
        {
            Notifications = new List<Notification>();
            Notifications = GetNewNotifications();
        }
        public List<Notification> GetNewNotifications()
        {
            int id = CurrentSession.CurrentUserId;
            int count = GetNumberOfNewNotifications();

            if (count == 0) throw new Exception("Новых уведомлений нет.");

            string query = "SELECT text FROM notifications WHERE receiverid = @id AND status = @status";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@status", Status.Unread);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Notification notification = new Notification()
                            {
                                Text = reader.GetString(0)
                            };
                            Notifications.Add(notification);
                        }
                    }
                }
            });

            return Notifications;
        }

        /// <summary>
        /// Метод, меняющий статус просмотренных уведомлений на "read"
        /// </summary>
        /// <param name="id">Id уведомления</param>
        public List<Notification> NotificationChecked(int id)
        {
            string query = "UPDATE notifications\n" +
                           "SET status = @status\n" +
                           "WHERE id = @id;";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@status", Status.Read);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Notification notification = new Notification()
                            {
                                Text = reader.GetString(0)
                            };
                            Notifications.Add(notification);
                        }
                    }
                }
            });

            return Notifications;
        }

        /// <summary>
        /// Метод, получающий количество новых сообщений пользователя
        /// </summary>
        /// <returns>Количество новых сообщений пользователя</returns>
        public static int GetNumberOfNewNotifications()
        {
            int id = CurrentSession.CurrentUserId;
            int count = 0;
            string query = "SELECT COUNT(*) FROM notifications WHERE receiverid = @id AND status = @status";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@status", Status.Unread);

                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            });

            return count;
        }
    }
}
