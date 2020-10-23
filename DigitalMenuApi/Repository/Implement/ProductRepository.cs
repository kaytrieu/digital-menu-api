using DigitalMenuApi.Data;
using DigitalMenuApi.Models;

namespace DigitalMenuApi.GenericRepository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DigitalMenuSystemContext dbContext) : base(dbContext)
        {
        }
    }
}