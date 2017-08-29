using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Registration.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        // [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        // [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Required]
        [MinLength(8)]
        // [RegularExpression(@"^[a-zA-Z]+$")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and password-confirmation must match.")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirmation { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

    }
}
