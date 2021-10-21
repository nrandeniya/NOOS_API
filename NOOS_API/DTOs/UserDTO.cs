using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_API.DTOs
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[Display(Name = "Password must include at least one; digit(0-9), uppercase, lovercase and a special character")]
        [StringLength(15, ErrorMessage ="Your password must be between {2} to {1} characters", MinimumLength =5)]
        public string Password { get; set; }

     
    }
}
