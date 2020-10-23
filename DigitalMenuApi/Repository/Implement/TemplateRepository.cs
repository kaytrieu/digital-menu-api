using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}