namespace SinaMerchant.Web.Models.ViewModels
{
    public class SendActivationEmailViewModel
    {
        public string Email { get; set; }
        public string EmailActiveCode { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
