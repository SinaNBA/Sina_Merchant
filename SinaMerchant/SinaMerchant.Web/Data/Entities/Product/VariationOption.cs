namespace SinaMerchant.Web.Data.Entities
{
    public class VariationOption
    {
        public int Id { get; set; }
        public int VariationId { get; set; } // Variation foreign key
        public string Value { get; set; } // like Red, XL, etc

        public Variation Variation { get; set; }
        public ICollection<ProductConfig>? ProductConfigs { get; set; }
    }
}
