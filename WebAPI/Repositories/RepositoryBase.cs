using Dot.Net.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        public LocalDbContext AppDbContext { get; }

        public RepositoryBase(LocalDbContext dbContext)
        {
            AppDbContext = dbContext;
        }

        public virtual void Add(TEntity entity)
        {
            if(entity != null)
            {
                AppDbContext.Set<TEntity>().Add(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = AppDbContext.Set<TEntity>().Find(id);
            if(entity != null)
            {
                AppDbContext.Set<TEntity>().Remove(entity);
            }
        }

        public virtual TEntity[] Find(Expression<Func<TEntity, bool>> predicate)
        {
            return AppDbContext.Set<TEntity>().Where(predicate).ToArray();
        }

        public virtual TEntity FindById(int id)
        {
            return AppDbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity[] GetAll()
        {
            return AppDbContext.Set<TEntity>().ToArray();
        }

        public abstract void Update(int id, TEntity entity);

        public virtual int SaveChanges()
        {
            return AppDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await AppDbContext.SaveChangesAsync();
        }
    }
}
