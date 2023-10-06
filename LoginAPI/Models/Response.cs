using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginAPI.Models;

namespace LoginAPI.Models
{
    public class Response
    {
        public string Status { get; set; }

        public int TweetID { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}