using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;

namespace DigitalMenuApi.Repository
{
    public class BoxTypeRepository : BaseRepository<BoxType>, IBoxTypeRepository
    {
        public BoxTypeRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}