namespace SinaMerchant.Web.Models.ViewModels
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; } // product foreign key
        public string? SKU { get; set; } // stock keeping unit
        public uint QtyInStock { get; set; }
        public string? ImageName { get; set; }// a gallery for a product
        public decimal Price { get; set; }
    }
}
