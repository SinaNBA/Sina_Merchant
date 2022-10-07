namespace SinaMerchant.Web.Models.ViewModels
{
    public class VariationOptionViewModel
    {
        public int Id { get; set; }
        public int VariationId { get; set; } // Variation foreign key
        public string Value { get; set; } // like Red, XL, etc
    }
}
