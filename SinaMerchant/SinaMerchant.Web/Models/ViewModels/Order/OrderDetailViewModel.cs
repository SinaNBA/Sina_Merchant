
namespace SinaMerchant.Web.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // ShopOrder table foreign key
        public int ProductItemId { get; set; } // ProductItem table foreign key        
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
