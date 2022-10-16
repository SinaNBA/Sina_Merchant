namespace SinaMerchant.Web.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; } // a self-join( a foreign key to another category's ID record)
        public string Name { get; set; } // product category's name
        //public string? ParentName { get; set; } // product parent category's name
    }
}
