using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(10)]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
