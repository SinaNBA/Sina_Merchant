using SinaMerchant.Web.Data.Entities;
using SinaMerchant.Web.Repositories;

namespace SinaMerchant.Web.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<SiteUser> SiteUserRepository { get; }
        IRepository<OrderDetail> OrderDetailRepository { get; }
        IRepository<ShopOrder> ShopOrderRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductCategory> ProductCategoryRepository { get; }
        IRepository<ProductConfig> ProductConfigRepository { get; }
        IRepository<ProductItem> ProductItemRepository { get; }
        IRepository<Variation> VariationRepository { get; }
        IRepository<VariationOption> VariationOptionRepository { get; }
        void Save();
    }
}
