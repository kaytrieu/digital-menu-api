using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class ScreenTemplateRepository : BaseRepository<ScreenTemplate>, IScreenTemplateRepository
    {
        public ScreenTemplateRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}