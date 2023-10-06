using System;

namespace LoginAPI.Models
{
    public class GetReTweetsDTO
    {
        public int ReTweetID { get; set; }

        public int UserID { get; set; }

        public int TweetID { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}