namespace SinaMerchant.Web.Entities
{
    // a class for the variance between products like size, color, etc
    public class Variation : EntityBase
    {
        public int CategoryId { get; set; } // category foreign key
        public string Name { get; set; } // like size, color, etc

        public ProductCategory Category { get; set; }
        public ICollection<VariationOption> VariationOptions { get; set; }
    }
}
