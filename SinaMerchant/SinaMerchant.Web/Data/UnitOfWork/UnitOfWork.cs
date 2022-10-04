using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Repositories;

namespace SinaMerchant.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<SiteUser> _siteRepository;

        public UnitOfWork(ApplicationDbContext context, IRepository<SiteUser> siteRepository)
        {
            _context = context;
            _siteRepository = siteRepository;
        }

        public IRepository<SiteUser> SiteUserRepository => _siteRepository;

        public IRepository<OrderDetail> OrderDetailRepository => OrderDetailRepository;

        public IRepository<ShopOrder> ShopOrderRepository => ShopOrderRepository;

        public IRepository<Product> ProductRepository => ProductRepository;

        public IRepository<ProductCategory> ProductCategoryRepository => ProductCategoryRepository;

        public IRepository<ProductConfig> ProductConfigRepository => ProductConfigRepository;

        public IRepository<ProductItem> ProductItemRepository => ProductItemRepository;

        public IRepository<Variation> VariationRepository => VariationRepository;

        public IRepository<VariationOption> VariationOptionRepository => VariationOptionRepository;

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
