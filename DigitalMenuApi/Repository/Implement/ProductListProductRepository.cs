using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class ProductListProductRepository : BaseRepository<ProductListProduct>, IProductListProductRepository
    {
        public ProductListProductRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}