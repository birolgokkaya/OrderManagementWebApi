using OrderManagement.Application.DTO.CustomerOrders;
using OrderManagement.Application.DTO.OrderProducts;
using OrderManagement.Application.Results;
using OrderManagement.Application.Services.Base;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services
{
    public interface ICustomerOrderService : IService<CustomerOrder>
    {
        Task<IResult> AddAsync(CreateCustomerOrderModel model);
        Task<IResult> Delete(DeleteCustomerOrderModel model);
        IResult UpdateOrderItem(UpdateOrderProductModel model);
        Task<IResult> UpdateCustomerOrderAddressAsync(UpdateCustomerOrderAddressModel model);
        Task<IResult> DeleteOrderItemAsync(DeleteOrderProductModel model);
        Task<IResult> AddOrderItemAsync(CreateOrderProductModel model);
    }
}