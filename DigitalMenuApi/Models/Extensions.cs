using Microsoft.AspNetCore.Authorization;
using System.Linq;

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

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public class AuthorizeRolesAttribute : AuthorizeAttribute
        {
            public AuthorizeRolesAttribute(params string[] roles) : base()
            {
                Roles = string.Join(",", roles);
            }
        }
    }
}
