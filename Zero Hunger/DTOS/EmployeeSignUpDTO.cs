using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.DTOS
{
    public class EmployeeSignUpDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Range(1000000000, 9999999999, ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters")]
        public string Password { get; set; }

    }
}