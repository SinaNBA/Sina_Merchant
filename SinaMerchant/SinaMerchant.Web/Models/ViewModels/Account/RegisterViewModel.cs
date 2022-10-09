using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Remote("VerifyEmail", "Account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
        [RegularExpression(@"^(?=.*[0-9A-Za-z])(?=.*\d)[A-Za-z\d]{6,10}$", ErrorMessage = "Passwords must be contain at least an letter and a digit. Min length of '6' , Max length of '10'.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [MaxLength(50)]
        [DisplayName("First Name")]
        public string? FName { get; set; }
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string? LName { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [MaxLength(50)]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
        [MaxLength(50)]
        public string? Country { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        public bool IsAdmin { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
    }
}
