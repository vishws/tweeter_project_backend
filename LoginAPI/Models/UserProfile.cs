using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class UserProfile
    {
        public int ProfileID { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Bio { get; set; }
        public int FollowerCount { get; set; }
    }
}