using OrderManagement.Application.Repositories.Base;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Repositories
{
    public interface ICustomerOrderRepository : IRepository<CustomerOrder>
    {
    }
}