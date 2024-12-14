using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitiesLibrary;
using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using PostingService;

namespace NotificationService
{
    /// <summary>
    /// Модуль, ответственный за отправку уведомлений
    /// </summary>
    public static class Notifier
    {
        /// <summary>
        /// Метод, подписывающий функцию уведомления на событие
        /// </summary>
        public static void Subscribe()
        {
            PostResponder.ResponseSent += Notify;
        }

        /// <summary>
        /// Функция уведомления
        /// </summary>
        /// <param name="notification">Уведомление</param>
        public static void Notify(Notification notification)
        {
            int id = CurrentSession.CurrentUserId;

            string query = "INSERT INTO notifications (text, senderid, receiverid, postid, status, created_at) VALUES\n" +
                           "(@text, @senderid, @receiverid, @postid, @status, @created_at)";

            QueryExecutor.ExecuteQuery(() =>
            {
                using (var command = new MySqlCommand(query, QueryExecutor.conn))
                {
                    command.Parameters.AddWithValue("@text", notification.Text);
                    command.Parameters.AddWithValue("@senderid", notification.SenderId);
                    command.Parameters.AddWithValue("@receiverid", notification.ReceiverId);
                    command.Parameters.AddWithValue("@postid", notification.PostId);
                    command.Parameters.AddWithValue("@status", notification.CurrentStatus);
                    command.Parameters.AddWithValue("@created_at", notification.CreatedAt);

                    command.ExecuteNonQuery();
                }
            });

        }
    }
}
