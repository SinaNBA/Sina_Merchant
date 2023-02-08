namespace SinaMerchant.Web.Models.ViewModels
{
    public class CreateCategoryViewModel
    {
        //public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }

        //public List<ProductCategoryViewModel>? Parents { get; set; } // a self-join( a foreign key to another category's ID record)        
        //public string? ParentName { get; set; } // product parent category's name
    }
}
