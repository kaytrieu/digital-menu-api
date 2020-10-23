using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class ScreenRepository : BaseRepository<Screen>, IScreenRepository
    {
        public ScreenRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}