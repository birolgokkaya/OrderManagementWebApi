using OrderManagement.Application.DTO.Customers;
using OrderManagement.Application.Results;
using OrderManagement.Application.Services.Base;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services
{
    public interface ICustomerService : IService<Customer>
    {
        Task<IResult> AddAsync(CreateCustomerModel model);
        Task<IResult> Delete(DeleteCustomerModel model);
        IResult Update(UpdateCustomerModel model);
    }
}