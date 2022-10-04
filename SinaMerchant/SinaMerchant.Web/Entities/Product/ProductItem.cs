namespace SinaMerchant.Web.Entities
{
    public class ProductItem
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; } // product foreign key
        public string? SKU { get; set; } // stock keeping unit
        public uint QtyInStock { get; set; }
        public string? ImageName { get; set; }// a gallery for a product
        public decimal Price { get; set; }

        public Product Product { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ProductConfig> ProductConfigs { get; set; }
    }
}
