using System.ComponentModel.DataAnnotations;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class SitUserViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
    }
}
