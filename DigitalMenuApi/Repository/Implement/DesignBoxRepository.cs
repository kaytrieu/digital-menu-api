using DigitalMenuApi.Data;
using DigitalMenuApi.GenericRepository;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.Repository.Implement
{
    public class DesignBoxRepository : BaseRepository<DesignBox>, IDesignBoxRepository
    {
        public DesignBoxRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
