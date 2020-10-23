using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository.Implement
{
    public class AccountRoleRepository : BaseRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
