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
        Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression);
        Task<bool> Edit(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> DeleteById(object id);
        Task Save();

        IQueryable<TEntity> entities { get; }


    }
}
