using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelsFeedbackSystem.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] including);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including);
        void Add(TEntity t);
        void Delete(TEntity t);
        void Update(TEntity items);
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
