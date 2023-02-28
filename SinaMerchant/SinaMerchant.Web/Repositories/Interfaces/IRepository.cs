using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Entities;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        bool Insert(TEntity entity);
        Task<ICollection<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        ICollection<TEntity>? Filter(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> GetSingleNoTracking(Expression<Func<TEntity, bool>> filterExpression);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        Task<bool> DeleteById(object id);
        void Save();
    }
}
