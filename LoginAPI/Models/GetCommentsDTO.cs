using System;

namespace LoginAPI.Models
{
    public class GetCommentsDTO
    {
        public int CommentID { get; set; }

        public string Content { get; set; }

        public int UserID { get; set; }

        public int TweetID { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}