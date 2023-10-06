using System;
using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models
{
    public class Tweet
    {
        public int TweetID { get; set; }

        public int UserID { get; set; }

     
        [MaxLength(100, ErrorMessage = "Tweet content should not exceed 100 characters.")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}