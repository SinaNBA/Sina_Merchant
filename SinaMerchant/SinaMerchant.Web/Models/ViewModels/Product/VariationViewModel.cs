namespace SinaMerchant.Web.Models.ViewModels
{
    public class VariationViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } // category foreign key
        public string Name { get; set; } // like size, color, etc
    }
}
