﻿using DigitalMenuApi.Data;
using DigitalMenuApi.Dtos.PagingDtos;
using DigitalMenuApi.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DigitalMenuApi.GenericRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly DbContext _dbContext;
        public readonly DbSet<TEntity> _dbSet;


        public BaseRepository(DigitalMenuSystemContext dbContext) // 
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
                return;
            }
            throw new ArgumentNullException(nameof(TEntity));

        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            return DbSetIncluding(query, including).Where(predicate).FirstOrDefault<TEntity>();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includer = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (includer != null)
                query = includer(query);

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

            return DbSetIncluding(query, including);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            return DbSetIncluding(query, including).Where(predicate);
        }

        public PagingDto<TEntity> GetAll(int page, int limit, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] including)
        {

            IQueryable<TEntity> query = _dbSet.AsQueryable();

            PagingDto<TEntity> dto = new PagingDto<TEntity>(DbSetIncluding(query, including).Where(predicate), page, limit);

            return dto;
        }

        public PagingDto<TEntity> GetAll(int page, int limit, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            PagingDto<TEntity> dto = new PagingDto<TEntity>(DbSetIncluding(query, including), page, limit);

            return dto;
        }

        private IQueryable<TEntity> DbSetIncluding(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] including)
        {
            if (including != null)
                including.ToList().ForEach(include =>
                {
                    if (include != null)
                        query = query.Include(include);
                });

            return query;
        }
        public PagingDto<TEntity> GetAllWithOrdered(int page, int limit, Expression<Func<TEntity, object>> orderBy, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            PagingDto<TEntity> dto = new PagingDto<TEntity>(DbSetIncluding(query, including).OrderBy(orderBy), page, limit);

            return dto;
        }

        public PagingDto<TEntity> GetAllWithOrdered(int page, int limit, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            PagingDto<TEntity> dto = new PagingDto<TEntity>(DbSetIncluding(query, including).Where(predicate).OrderBy(orderBy), page, limit);

            return dto;
        }

        public PagingDto<TEntity> GetAllWithOrderedDecs(int page, int limit, Expression<Func<TEntity, object>> orderBy, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            PagingDto<TEntity> dto = new PagingDto<TEntity>(DbSetIncluding(query, including).OrderByDescending(orderBy), page, limit);

            return dto;
        }

        public PagingDto<TEntity> GetAllWithOrderedDecs(int page, int limit, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, params Expression<Func<TEntity, object>>[] including)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            PagingDto<TEntity> dto = new PagingDto<TEntity>(DbSetIncluding(query, including).Where(predicate).OrderByDescending(orderBy), page, limit);

            return dto;
        }

    }
}
