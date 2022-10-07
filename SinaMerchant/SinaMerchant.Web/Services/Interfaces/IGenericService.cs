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
        Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression);
        Task<bool> Edit(TViewModel entityModel);
        Task<bool> Delete(TViewModel entityModel);
        Task<bool> DeleteById(object id);

        IQueryable<TViewModel> entities { get; }
    }
}
