using CrudCosmos.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudCosmos.Services.Interfaces
{
    public interface IService<T> where T : class, IEntity
    {
        public Task<T> GetById(string id);

        public Task<IEnumerable<T>> GetAll();

        public Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression);

        public Task<T> Create(T entity);

        public Task<T> Update(T entity);

        public Task Delete(string id);
    }
}