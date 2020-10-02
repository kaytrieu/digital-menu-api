using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;

namespace DigitalMenuApi.Repository
{
    public class BoxRepository : BaseRepository<Box>, IBoxRepository
    {
        public BoxRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}