using CrudCosmos.Models.Interfaces;
using CrudCosmos.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudCosmos.Services.Interfaces
{
    public abstract class Service<T> : IService<T> where T : class, IEntity
    {
        protected readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> GetById(string id) => await _repository.GetById(id);

        public virtual async Task<IEnumerable<T>> GetAll() => await _repository.GetAll();

        public virtual async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression) => await _repository.GetByCondition(expression);

        public virtual async Task<T> Create(T entity) => await _repository.Create(entity);

        public virtual async Task<T> Update(T entity) => await _repository.Update(entity);

        public virtual async Task Delete(string id) => await _repository.Delete(id);
    }
}