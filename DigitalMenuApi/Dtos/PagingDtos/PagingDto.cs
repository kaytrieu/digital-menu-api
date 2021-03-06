using DigitalMenuApi.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.PagingDtos
{
    public class PagingDto<TEntity> where TEntity : class
    {
        public PagingDto(IQueryable<TEntity> src, int page, int limit)
        {
            Result = src.Paging<TEntity>(page, limit);
            Count = src.Count();
        }

        public PagingDto()
        {
        }

        public IQueryable<TEntity> Result { get; set; }
        public int Count { get; set; }

    }
}
