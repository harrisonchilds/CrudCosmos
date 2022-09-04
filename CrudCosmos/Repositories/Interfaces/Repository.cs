using CrudCosmos.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudCosmos.Repositories.Interfaces
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly DbContext _dbContext;

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetById(string id)
        {
            return await _dbContext.Set<T>()
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public virtual async Task<T> Create(T entity)
        {
            if (entity.Id is null)
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return await GetById(entity.Id);
        }

        public virtual async Task<T> Update(T entity)
        {
            var entry = _dbContext.Add(entity);
            entry.State = EntityState.Unchanged;

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return await GetById(entity.Id);
        }

        public virtual async Task Delete(string id)
        {
            var entity = await GetById(id);

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}