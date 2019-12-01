using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(int id);
        TEntity FindById(int id);
        TEntity[] GetAll();
        TEntity[] Find(Expression<Func<TEntity, bool>> predicate);
        abstract void Update(int id, TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
