using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class BoxRepository : BaseRepository<Box>, IBoxRepository
    {
        public BoxRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}