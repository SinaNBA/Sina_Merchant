namespace SinaMerchant.Web.Entities
{    
    // a bridge table for ProductItem and VariationOption
    public class ProductConfig
    {
        public int Id { get; set; }
        public int ProductItemId { get; set; } // ProductItem foreign key
        public int VariationOptionId { get; set; } // VariationOption foreign key
        
    }
}
