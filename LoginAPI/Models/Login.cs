using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LoginAPI.Models;

namespace LoginAPI.Models
{
    public class Login
    {
        //[Required(ErrorMessage = "UserName is required.")]
        //public string UserName { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //public string Password { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        //[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Invalid UserName format. Use only letters and numbers.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,12}$", ErrorMessage = "Invalid Password format.")]
        public string Password { get; set; }
    }

}
