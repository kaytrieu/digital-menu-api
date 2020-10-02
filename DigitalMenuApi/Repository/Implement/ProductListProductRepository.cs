using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;

namespace DigitalMenuApi.Repository
{
    public class ProductListProductRepository : BaseRepository<ProductListProduct>, IProductListProductRepository
    {
        public ProductListProductRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}