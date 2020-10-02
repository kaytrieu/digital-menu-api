using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;

namespace DigitalMenuApi.Repository
{
    public class ProductListRepository : BaseRepository<ProductList>, IProductListRepository
    {
        public ProductListRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}