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

        public IQueryable<TEntity> Entities => _entities;

        public DbSet<TEntity> DbSet => _entities;

        public async Task<bool> InsertAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            Save();
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

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filterExpression, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _entities;
            if (disableTracking) query = query.AsNoTracking();
            return query.SingleOrDefault(filterExpression);
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _entities.AnyAsync(filterExpression);
        }

        public bool Edit(TEntity entity)
        {
            //if (_context.Entry(entity).State == EntityState.Added) _context.Attach(entity);

            _context.Update(entity);
            Save();
            return true;
        }

        public bool Delete(TEntity entity)
        {
            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    _context.Attach(entity);
            //}
            _entities.Remove(entity);
            Save();
            return true;
        }

        public async Task<bool> DeleteById(object id)
        {
            var entity = await GetById(id);
            return Delete(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
