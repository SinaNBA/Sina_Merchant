using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using System.Runtime.CompilerServices;

namespace SinaMerchant.Web.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region DbSets
        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShopOrder> ShopOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductConfig> ProductConfigs { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Variation> Variations { get; set; }
        public DbSet<VariationOption> VariationOptions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteUser>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Email).HasMaxLength(50);
                x.Property(x => x.Password).HasMaxLength(10);
                x.Property(x => x.FName).HasMaxLength(50);
                x.Property(x => x.LName).HasMaxLength(50);
                x.Property(x => x.Address).HasMaxLength(50);
                x.Property(x => x.City).HasMaxLength(50);
                x.Property(x => x.PostalCode).HasMaxLength(50);
                x.Property(x => x.Country).HasMaxLength(50);
                x.Property(x => x.Phone).HasMaxLength(50);
                x.HasMany(x => x.ShopOrders).WithOne(x => x.SiteUser);
            });

            modelBuilder.Entity<OrderDetail>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.ShopOrder).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
                x.HasOne(x => x.ProductItem).WithMany(x => x.OrderDetails).HasForeignKey(x => x.ProductItemId);
                x.Property(x => x.Price).HasPrecision(10, 2);
            });

            modelBuilder.Entity<ShopOrder>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.SiteUser).WithMany(x => x.ShopOrders).HasForeignKey(x => x.SiteUserId);
                x.Property(x => x.Price).HasPrecision(10, 2);
                x.HasMany(x => x.OrderDetails).WithOne(x => x.ShopOrder);
            });

            modelBuilder.Entity<Product>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                x.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
                x.Property(x => x.Name).HasMaxLength(50);
                x.Property(x => x.Description).HasMaxLength(50);
                x.Property(x => x.ImageName).HasMaxLength(50);
                x.HasMany(x => x.ProductItems).WithOne(x => x.Product);
            });

            modelBuilder.Entity<ProductCategory>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Parent).WithMany(x => x.Childs).HasForeignKey(x => x.ParentId);
                x.Property(x => x.Name).HasMaxLength(50);
                x.HasMany(x => x.Childs).WithOne(x => x.Parent);
                x.HasMany(x => x.Products).WithOne(x => x.Category);
                x.HasMany(x => x.Variations).WithOne(x => x.Category);
            });

            modelBuilder.Entity<ProductConfig>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.ProductItem).WithMany(x => x.ProductConfigs).HasForeignKey(x => x.ProductItemId);
                x.HasOne(x => x.VariationOption).WithMany(x => x.ProductConfigs).HasForeignKey(x => x.VariationOptionId);
            });

            modelBuilder.Entity<ProductItem>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Product).WithMany(x => x.ProductItems).HasForeignKey(x => x.ProductId);
                x.Property(x => x.SKU).HasMaxLength(50);
                x.Property(x => x.ImageName).HasMaxLength(50);
                x.HasMany(x => x.OrderDetails).WithOne(x => x.ProductItem);
                x.HasMany(x => x.ProductConfigs).WithOne(x => x.ProductItem);
            });

            modelBuilder.Entity<Variation>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Category).WithMany(x => x.Variations).HasForeignKey(x => x.CategoryId);
                x.Property(x => x.Name).HasMaxLength(50);
                x.HasMany(x => x.VariationOptions).WithOne(x => x.Variation);
            });

            modelBuilder.Entity<VariationOption>(x =>
            {
                x.HasKey(x => x.Id);
                x.HasOne(x => x.Variation).WithMany(x => x.VariationOptions).HasForeignKey(x => x.VariationId);
                x.Property(x => x.Value).HasMaxLength(50);
                x.HasMany(x => x.ProductConfigs).WithOne(x => x.VariationOption);
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
