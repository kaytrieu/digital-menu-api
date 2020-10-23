using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class BoxTypeRepository : BaseRepository<BoxType>, IBoxTypeRepository
    {
        public BoxTypeRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}