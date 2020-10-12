using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;
using System.Linq;

namespace DigitalMenuApi.Repository
{
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}