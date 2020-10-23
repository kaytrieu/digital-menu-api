using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}