namespace SinaMerchant.Web.Models.ViewModels
{
    public class CreateCategoryViewModel
    {        
        public List<ProductCategoryViewModel>? Parents { get; set; } // a self-join( a foreign key to another category's ID record)
        public string Name { get; set; } // product category's name
        //public string? ParentName { get; set; } // product parent category's name
    }
}
