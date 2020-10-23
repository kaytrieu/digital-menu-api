using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class ProductListRepository : BaseRepository<ProductList>, IProductListRepository
    {
        public ProductListRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}