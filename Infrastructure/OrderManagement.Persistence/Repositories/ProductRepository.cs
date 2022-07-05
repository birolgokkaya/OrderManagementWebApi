using OrderManagement.Application.Repositories;
using OrderManagement.Domain.Entities;
using OrderManagement.Persistence.Contexts;

namespace OrderManagement.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(OrderManagementDbContext context) : base(context)
        {
        }
    }
}