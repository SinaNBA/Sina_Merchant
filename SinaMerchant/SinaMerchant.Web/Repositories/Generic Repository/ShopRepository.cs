using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public class ShopRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public bool Insert(TEntity entity)
        {
            _entities.Add(entity);
            return true;
        }

        public ICollection<TEntity>? GetAll()
        {
            return _entities.ToList();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }

        public ICollection<TEntity>? Filter(Expression<Func<TEntity, bool>> filterExpression)
        {

            return _entities.Where(filterExpression).ToList();

        }

        public bool Update(TEntity entity)
        {
            _entities.Update(entity);
            return true;
        }

        public bool Delete(TEntity entity)
        {
            _entities.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteById(object id)
        {
            var entity = await GetById(id);
            return Delete(entity);
        }

    }
}
