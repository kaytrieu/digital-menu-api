using DigitalMenuApi.Data;
using Microsoft.EntityFrameworkCore;
using ModelsFeedbackSystem.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelsFeedbackSystem.GenericRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;


        public BaseRepository(DigitalMenuBoxContext dbContext) // 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public void Add(TEntity t)
        {
            _dbSet.Add(t);
        }

        public void Delete(TEntity t)
        {
            if (t != null)
            {
                _dbSet.Remove(t);
            }
            throw new ArgumentNullException(nameof(TEntity));

        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });
            return query.Where(predicate).FirstOrDefault<TEntity>();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Update(TEntity items)
        {
            //Default is nothing
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });
            return query;
        }
    }
}
