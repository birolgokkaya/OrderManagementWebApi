using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Repositories;
using OrderManagement.Domain.Entities;
using OrderManagement.Persistence.Contexts;
using System.Linq.Expressions;

namespace OrderManagement.Persistence.Repositories
{
    public class CustomerOrderRepository : Repository<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(OrderManagementDbContext context) : base(context)
        {

        }

        public override async Task<List<CustomerOrder>> GetAllAsync()
        {
            return await _context.CustomerOrders.Include(c => c.Customer).Include(c => c.OrderProducts).ThenInclude(x => x.Product).ToListAsync();
        }

        public override async Task<CustomerOrder> GetAsync(Expression<Func<CustomerOrder, bool>> filter)
        {
            return await _context.CustomerOrders.Include(x => x.Customer).Include(c => c.OrderProducts).ThenInclude(x => x.Product).Where(filter).FirstOrDefaultAsync();
        }

        public override async Task<List<CustomerOrder>> GetManyAsync(Expression<Func<CustomerOrder, bool>> filter)
        {
            return await _context.CustomerOrders.Include(x => x.Customer).Include(c => c.OrderProducts).ThenInclude(x => x.Product).Where(filter).ToListAsync();
        }
    }
}