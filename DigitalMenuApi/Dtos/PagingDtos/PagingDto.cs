using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Dtos.PagingDtos
{
    public class PagingDto<TEntity> where TEntity : class
    {
        public PagingDto(IQueryable<TEntity> result)
        {
            Result = result;
            Count = result.Count();
        }

        public PagingDto()
        {
        }

        public IQueryable<TEntity> Result { get; set; }
        public int Count { get; set; }

    }
}
