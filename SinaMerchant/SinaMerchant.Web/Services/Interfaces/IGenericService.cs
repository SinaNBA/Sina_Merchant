using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Services
{
    public interface IGenericService<TEntity, TViewModel> where TEntity : class where TViewModel : class
    {
        Task<bool> InsertAsync(TViewModel entityModel);
        Task<ICollection<TViewModel>> GetAll();
        Task<TViewModel> GetById(object id);
        ICollection<TViewModel>? Filter(Expression<Func<TEntity, bool>> filterExpression);
        Task<TViewModel> GetFirstAsync(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filterExpression);
        TViewModel GetSingle(Expression<Func<TEntity, bool>> filterExpression, bool disableTracking);
        Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression);
        bool Edit(TViewModel entityModel);
        bool Delete(TViewModel entityModel);
        Task<bool> DeleteById(object id);

        IEnumerable<TViewModel> Entities { get; }
        DbSet<TEntity> EntitiesSet { get; }
    }
}
