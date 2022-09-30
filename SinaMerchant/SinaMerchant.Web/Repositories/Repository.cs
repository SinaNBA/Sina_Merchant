using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data.Context;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public bool Insert(TEntity entity)
        {
            _entities.Add(entity);
            return true;
        }

        public ICollection<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public TEntity GetById(object id)
        {
            throw new NotImplementedException();
        }

        public TEntity Filter(Expression<Func<TEntity, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(object id)
        {
            throw new NotImplementedException();
        }

    }
}
