using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinaMerchant.Web.Repositories;
using System.Linq.Expressions;

namespace SinaMerchant.Web.Services
{
    public class GenericService<TEntity, TViewModel> : IGenericService<TEntity, TViewModel>
        where TEntity : class where TViewModel : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<TViewModel> Entities => _mapper.Map<IEnumerable<TViewModel>>(_repository.Entities);

        public DbSet<TEntity> EntitiesSet => _repository.DbSet;

        public async Task<bool> InsertAsync(TViewModel entityModel)
        {
            var entity = _mapper.Map<TEntity>(entityModel);
            return await _repository.InsertAsync(entity);
        }

        public async Task<ICollection<TViewModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<ICollection<TViewModel>>(entities);
        }

        public async Task<TViewModel> GetById(object id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<TViewModel>(entity);
        }

        public async Task<TViewModel> GetFirstAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            var entity = await _repository.GetFirstAsync(filterExpression);
            return _mapper.Map<TViewModel>(entity);
        }

        public ICollection<TViewModel>? Filter(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _mapper.Map<ICollection<TViewModel>>(_repository.Filter(filterExpression));
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _repository.GetSingleAsync(filterExpression);
        }

        public TViewModel GetSingle(Expression<Func<TEntity, bool>> filterExpression, bool disableTracking = true)
        {
            var query = _repository.GetSingle(filterExpression, disableTracking);
            return _mapper.Map<TViewModel>(query);
        }

        public Task<bool> IsExist(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _repository.IsExist(filterExpression);
        }

        public bool Edit(TViewModel entityModel)
        {
            var entity = _mapper.Map<TEntity>(entityModel);
            return _repository.Edit(entity);
        }

        public bool Delete(TViewModel entityModel)
        {
            var entity = _mapper.Map<TEntity>(entityModel);
            return _repository.Delete(entity);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await _repository.DeleteById(id);
        }

    }
}
