using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;

namespace DigitalMenuApi.Repository
{
    public class ScreenTemplateRepository : BaseRepository<ScreenTemplate>, IScreenTemplateRepository
    {
        public ScreenTemplateRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}