﻿namespace SinaMerchant.Web.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public int CategoryId { get; set; } // category foreign key
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }

        public ProductCategory Category { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }
    }
}
