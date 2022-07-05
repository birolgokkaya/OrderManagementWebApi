using OrderManagement.Application.DTO.CustomerOrders;
using OrderManagement.Application.DTO.OrderProducts;
using OrderManagement.Application.Repositories;
using OrderManagement.Application.Results;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public CustomerOrderService(ICustomerOrderRepository customerOrderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository)
        {
            _customerOrderRepository = customerOrderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderProductRepository = orderProductRepository;
        }

        public async Task<IResult> AddAsync(CreateCustomerOrderModel model)
        {
            var customer = _customerRepository.GetAsync(x => x.Id == model.CustomerId).Result;
            if (customer is null) return new ErrorResult("Customer cannot be found!");

            CustomerOrder customerOrder = new();
            customerOrder.CustomerId = model.CustomerId;
            customerOrder.OrderAddress = model.CustomerAddress;

            foreach (var product in model.OrderProducts)
            {
                var addProduct = _productRepository.GetAsync(x => x.Id == product.ProductId).Result;
                if (addProduct is null) return new ErrorResult("Product cannot be found!");

                var orderProduct = new OrderProduct { ProductId = product.ProductId, Quantity = product.Quantity };
                customerOrder.OrderProducts.Add(orderProduct);
            }

            var result = await _customerOrderRepository.AddAsync(customerOrder);
            return result ? new SuccessResult() : new ErrorResult();
        }

        public async Task<IResult> AddOrderItemAsync(CreateOrderProductModel model)
        {
            var createOrderProduct = _customerOrderRepository.GetAsync(x => x.Id == model.CustomerOrderId).Result;
            if (createOrderProduct is null) return new ErrorResult("Order cannot be found!");

            var product = _productRepository.GetAsync(x => x.Id == model.ProductId).Result;
            if (product is null) return new ErrorResult("Product cannot be found!");

            OrderProduct orderProduct = new();
            orderProduct.ProductId = model.ProductId;
            orderProduct.Quantity = model.Quantity;
            orderProduct.CustomerOrderId = model.CustomerOrderId;

            var result = await _orderProductRepository.AddAsync(orderProduct);
            return result ? new SuccessResult() : new ErrorResult();
        }

        public async Task<IResult> Delete(DeleteCustomerOrderModel model)
        {
            var deleteOrder = _customerOrderRepository.GetAsync(x => x.Id == model.Id).Result;
            if (deleteOrder is null) return new ErrorResult("Order cannot be found!");

            var result = _customerOrderRepository.Delete(deleteOrder);
            return result ? new SuccessResult("Deleted successfully") : new ErrorResult();
        }

        public async Task<IResult> DeleteOrderItemAsync(DeleteOrderProductModel model)
        {
            var deleteOrderProduct = _orderProductRepository.GetAsync(x => x.Id == model.OrderProductId).Result;
            if (deleteOrderProduct is null) return new ErrorResult("OrderProduct cannot be found!");

            var result = _orderProductRepository.Delete(deleteOrderProduct);
            return result ? new SuccessResult("Deleted successfully") : new ErrorResult();
        }

        public async Task<IDataResult<List<CustomerOrder>>> GetAllAsync()
        {
            var orders = await _customerOrderRepository.GetAllAsync();
            return new SuccessDataResult<List<CustomerOrder>>(orders);
        }

        public async Task<IDataResult<CustomerOrder>> GetByIdAsync(int id)
        {
            var order = await _customerOrderRepository.GetAsync(x => x.Id == id);
            return new SuccessDataResult<CustomerOrder>(order);
        }

        public async Task<IResult> UpdateCustomerOrderAddressAsync(UpdateCustomerOrderAddressModel model)
        {
            var customerOrderDb = await _customerOrderRepository.GetAsNoTrackingAsync(x => x.Id == model.CustomerOrderId);
            if (customerOrderDb is null) return new ErrorResult("CustomerOrder cannot be found!");

            var customerOrder = new CustomerOrder { Id = model.CustomerOrderId, OrderAddress = model.Address, CustomerId = customerOrderDb.CustomerId };

            var result = _customerOrderRepository.Update(customerOrder);
            return result ? new SuccessResult() : new ErrorResult();
        }

        public IResult UpdateOrderItem(UpdateOrderProductModel model)
        {
            var updateProduct = _orderProductRepository.GetAsNoTrackingAsync(x => x.Id == model.OrderProductId).Result;
            if (updateProduct is null) return new ErrorResult("OrderProduct cannot be found!");

            OrderProduct orderProduct = new();
            orderProduct.Id = model.OrderProductId;
            orderProduct.Quantity = model.Quantity;
            orderProduct.CustomerOrderId = updateProduct.CustomerOrderId;
            orderProduct.ProductId = updateProduct.ProductId;

            var result = _orderProductRepository.Update(orderProduct);
            return result ? new SuccessResult() : new ErrorResult();
        }
    }
}