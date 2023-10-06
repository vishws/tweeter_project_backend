using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LoginAPI.Models
{
    public class Registration
    {
        //public int UserId { get; set; }
        //public string UserName { get; set; }
        //public string LoginName { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
        //public string ContactNo { get; set; }
        //public string Address { get; set; }
        //public int IsApporved { get; set; }
        //public int Status { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Name should not exceed 20 characters.")]
        
        public string UserName { get; set; }

        //[Required(ErrorMessage = "LoginName is required.")]

        public string LoginName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Password should be 8 to 12 characters long.")]
        [MaxLength(12)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-])[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\-]+$", ErrorMessage = "Password must contain at least 1 special character, 1 letter, and 1 capital letter.")]
        public string Password { get; set; }
  


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ContactNo is required.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }


}
