using DigitalMenuApi.Data;
using DigitalMenuApi.Models;
using ModelsFeedbackSystem.GenericRepository;
using ModelsFeedbackSystem.Repository;

namespace DigitalMenuApi.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DigitalMenuBoxContext dbContext) : base(dbContext)
        {
        }
    }
}