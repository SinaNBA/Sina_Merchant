namespace SinaMerchant.Web.Models.ViewModels
{
    public class ShopOrderViewModel
    {
        public int Id { get; set; }
        public int SiteUserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }// total price
    }
}
