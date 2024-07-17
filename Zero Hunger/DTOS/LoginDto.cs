using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.DTOS
{
    public class LoginDto
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters")]
        public string Password { get; set; }
    }
}