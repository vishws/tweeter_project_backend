using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class Comments
    {
        public int CommentID { get; set; }
        public int TweetID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}