using PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingService
{
    /// <summary>
    /// Структура, хранящая информацию об уведомлениях
    /// </summary>
    public struct Notification
    {
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int PostId { get; set; }
        public Status CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public Notification(int senderId, int receiverId, int postId, DateTime createdAt)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            CreatedAt = createdAt;
            PostId = postId;
            CurrentStatus = Status.Unread;
            Text = $"Пользователь {FinderUtilityClass.GetUsernameByID(senderId)} откликнулся на ваш пост: {FinderUtilityClass.GetPostTitleById(postId)}.";
        }
    }

    /// <summary>
    /// Enum, содержащий возможные состояния уведомления
    /// </summary>
    public enum Status
    {
        Unread = 1,
        Read = 2
    }
}
