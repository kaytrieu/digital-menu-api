﻿using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;

namespace DigitalMenuApi.Repository.Implement
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }

    }
}
