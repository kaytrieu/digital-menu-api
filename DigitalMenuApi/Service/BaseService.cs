using AutoMapper;
using DigitalMenuApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        public readonly DbContext _dbContext;
        public readonly DbSet<TEntity> _dbSet;
        public readonly IMapper _mapper;

        public BaseService(DigitalMenuSystemContext dbContext, IMapper mapper) // 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _mapper = mapper;
        }
    }
}
