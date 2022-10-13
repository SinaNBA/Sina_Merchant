using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class ResetPassViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(200)]
        [RegularExpression(@"^(?=.*[0-9A-Za-z])(?=.*\d)[A-Za-z\d]{6,10}$", ErrorMessage = "Passwords must be contain at least an letter and a digit. Min length of '6' , Max length of '10'.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(200)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
