using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }
        public string Bio { get; set; }

        public string profilepictureurl { get; set; }

        public int followercount { get; set; }
        public int profileid { get; set; }
    }
}