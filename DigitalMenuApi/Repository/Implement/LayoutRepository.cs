using DigitalMenuApi.Data;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Repository.Implement
{
    public class LayoutRepository : BaseRepository<Layout>, ILayoutRepository
    {
        public LayoutRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
