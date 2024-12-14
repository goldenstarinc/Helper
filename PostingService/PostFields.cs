using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingService
{
    /// <summary>
    /// Структура, отвечающая за поля поста
    /// </summary>
    public struct PostFields
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int PosterId { get; set; }
        public string Location { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateExpired { get; set; }

        public PostFields(string title, string content, int posterId, string location, DateTime datePosted, DateTime dateExpired)
        {
            Title = title;
            Content = content;
            PosterId = posterId;
            Location = location;
            DatePosted = DateTime.Now;
            DateExpired = dateExpired;
        }

        public override string ToString()
        {
            return $"Title: {Title}; Content: {Content}; PosterId: {PosterId}; Location: {Location}; DatePosted: {DatePosted}; DateExpired: {DateExpired}";
        }
    }
}
