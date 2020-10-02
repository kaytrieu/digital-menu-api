using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;

namespace DigitalMenuApi.Repository.Implement
{
    public class AccountRoleRepository : BaseRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}
