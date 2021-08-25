using AutoMapper;
using DigitalMenuApi.Data;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Service.Implement
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(DigitalMenuSystemContext dbContext, IMapper mapper,
                               IAccountRepository accountRepository ) : base(dbContext, mapper)
        {
            _accountRepository = accountRepository;
        }
    }
}
