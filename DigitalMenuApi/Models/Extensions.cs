using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Models.Extensions
{
    public static class Extensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int page, int limit)
        {
            if (page != 0 && limit != 0)
            {
                query = query.Skip(limit * (page - 1)).Take(limit);
            }

            return query;
        }
    }
}
