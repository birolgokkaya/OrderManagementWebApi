using OrderManagement.Application.Repositories;
using OrderManagement.Domain.Entities;
using OrderManagement.Persistence.Contexts;

namespace OrderManagement.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(OrderManagementDbContext context) : base(context)
        {
        }
    }
}