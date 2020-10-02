using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;

namespace DigitalMenuApi.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}