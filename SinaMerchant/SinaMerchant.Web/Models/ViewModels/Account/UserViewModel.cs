using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
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
    }
}
