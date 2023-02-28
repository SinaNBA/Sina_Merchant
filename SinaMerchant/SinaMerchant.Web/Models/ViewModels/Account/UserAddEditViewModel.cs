using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class UserAddEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [Remote("VerifyEmail","User","Admin",AdditionalFields =nameof(PageMode))]
        public string Email { get; set; }
        public string PageMode { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(200)]
        [RegularExpression(@"^(?=.*[0-9A-Za-z])(?=.*\d)[A-Za-z\d]{6,10}$", ErrorMessage = "Passwords must be contain at least an letter and a digit. Min length of '6' , Max length of '10'.")]
        public string Password { get; set; }     
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
        public string? EmailActiveCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
