using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using ModelsFeedbackSystem.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalMenuApi.Repository.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DigitalMenuBoxContext _dbContext;

        public AccountRepository(DigitalMenuBoxContext dbContext)  { _dbContext = dbContext; }

        public void Add(Account t)
        {
            throw new NotImplementedException();
        }

        public void Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Account Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            return _dbContext.Account.Include(account => account.Role).Include(account => account.Store).AsEnumerable<Account>();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(object key, Account items)
        {
            throw new NotImplementedException();
        }
    }
}
