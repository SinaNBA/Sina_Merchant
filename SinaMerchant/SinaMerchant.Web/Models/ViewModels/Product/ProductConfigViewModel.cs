namespace SinaMerchant.Web.Models.ViewModels
{
    public class ProductConfigViewModel
    {
        public int Id { get; set; }
        public int ProductItemId { get; set; } // ProductItem foreign key
        public int? VariationOptionId { get; set; } // VariationOption foreign key
    }
}
