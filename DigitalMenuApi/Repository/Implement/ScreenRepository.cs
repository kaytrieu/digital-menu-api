using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;

namespace DigitalMenuApi.Repository
{
    public class ScreenRepository : BaseRepository<Screen>, IScreenRepository
    {
        public ScreenRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}