using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Data;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<bool> InsertAsync(TEntity entity);
        Task<ICollection<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        ICollection<TEntity>? Filter(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filterExpression);
        TEntity GetSingle(Expression<Func<TEntity, bool>> filterExpression, bool disableTracking);
        Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression);
        bool Edit(TEntity entity);
        bool Delete(TEntity entity);
        Task<bool> DeleteById(object id);
        void Save();

        IQueryable<TEntity> Entities { get; }


    }
}
