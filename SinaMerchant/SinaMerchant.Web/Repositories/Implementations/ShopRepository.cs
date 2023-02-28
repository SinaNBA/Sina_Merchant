using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;
using SinaMerchant.Web.Entities;
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
            _entities.AddAsync(entity);
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

        public async Task<TEntity> GetSingleNoTracking(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _entities.Where(filterExpression).AsNoTracking().SingleOrDefaultAsync();
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _context.Attach(entity);

                _context.Entry(entity).State = EntityState.Modified;
                Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _entities.Remove(entity);
                Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public async Task<bool> DeleteById(object id)
        {
            var entity = await GetById(id);
            return Delete(entity);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
