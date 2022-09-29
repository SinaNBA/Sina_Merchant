namespace SinaMerchant.Web.Data.Entities
{
    // order's detail or shopping cart
    public class OrderDetail
    {

        public int Id { get; set; }
        public int OrderId { get; set; } // ShopOrder table foreign key
        public int ProductItemId { get; set; } // ProductItem table foreign key        
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ShopOrder Order { get; set; }
        public ProductItem ProductItem { get; set; }

    }
}
