﻿namespace SinaMerchant.Web.Data.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int? ParentId { get; set; } // a self-join( a foreign key to another category's ID record)
        public string Name { get; set; } // product category's name

        public ProductCategory? Parent { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}