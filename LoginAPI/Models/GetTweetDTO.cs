using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class GetTweetDTO
    {
        public int TweetID { get; set; }
        public string Content { get; set; }
        public int UserID { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}