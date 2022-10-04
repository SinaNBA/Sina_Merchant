namespace SinaMerchant.Web.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int? ParentId { get; set; } // a self-join( a foreign key to another category's ID record)
        public string Name { get; set; } // product category's name

        public ProductCategory? Parent { get; set; }
        public ICollection<ProductCategory> Childs { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Variation> Variations { get; set; }
    }
}
