﻿using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelsFeedbackSystem.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including);
        IQueryable<TEntity> GetAll(int page, int limit, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including);
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] including);
        IQueryable<TEntity> GetAll(int page, int limit, params Expression<Func<TEntity, object>>[] including);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includer = null);
        void Add(TEntity t);
        void Delete(TEntity t);
        void Update(TEntity items);
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
