namespace SinaMerchant.Web.Entities
{
    // a bridge table for ProductItem and VariationOption
    public class ProductConfig : EntityBase
    {
        public int ProductItemId { get; set; } // ProductItem foreign key
        public int? VariationOptionId { get; set; } // VariationOption foreign key

        public ProductItem ProductItem { get; set; }
        public VariationOption? VariationOption { get; set; }
    }
}
