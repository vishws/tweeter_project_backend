using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(20, ErrorMessage = "Name should not exceed 20 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "NewPassword is required.")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password should be 8 to 12 characters long.")]
        [MaxLength(12)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-])[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\-]+$", ErrorMessage = "Password must contain at least 1 special character, 1 letter, and 1 capital letter.")]
        public string NewPassword { get; set; }

    }

}