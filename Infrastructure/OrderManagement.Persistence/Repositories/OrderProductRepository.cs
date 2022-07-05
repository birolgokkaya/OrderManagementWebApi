using OrderManagement.Application.Repositories;
using OrderManagement.Domain.Entities;
using OrderManagement.Persistence.Contexts;

namespace OrderManagement.Persistence.Repositories
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(OrderManagementDbContext context) : base(context)
        {
        }
    }
}