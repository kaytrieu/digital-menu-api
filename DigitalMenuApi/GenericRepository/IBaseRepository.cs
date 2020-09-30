using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ModelsFeedbackSystem.Repository
{
    public interface IBaseRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        T Get(object id);
        void Add(T t);
        void Delete(object key);
        void Update(object key, T items);
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
