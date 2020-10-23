using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository.Implement
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }

    }
}
