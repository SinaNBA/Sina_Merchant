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

        public IQueryable<TEntity> entities => _entities;

        public async Task<bool> InsertAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await Save();
            return true;
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }

        public ICollection<TEntity>? Filter(Expression<Func<TEntity, bool>> filterExpression)
        {

            return _entities.Where(filterExpression).ToList();

        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _entities.FirstOrDefaultAsync(filterExpression);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _entities.SingleOrDefaultAsync(filterExpression);
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _entities.AnyAsync(filterExpression);
        }

        public async Task<bool> Edit(TEntity entity)
        {
            _entities.Update(entity);
            await Save();
            return true;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _entities.Remove(entity);
            await Save();
            return true;
        }

        public async Task<bool> DeleteById(object id)
        {
            var entity = await GetById(id);
            return await Delete(entity);
        }

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
