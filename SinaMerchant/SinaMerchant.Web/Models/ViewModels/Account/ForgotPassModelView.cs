using System.ComponentModel.DataAnnotations;

namespace SinaMerchant.Web.Models.ViewModels
{
    public class ForgotPassModelView
    {
        [Required]        
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
