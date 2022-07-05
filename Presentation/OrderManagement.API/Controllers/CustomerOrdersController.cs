using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO.CustomerOrders;
using OrderManagement.Application.DTO.OrderProducts;
using OrderManagement.Application.Services;

namespace OrderManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrdersController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _customerOrderService.GetAllAsync();
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _customerOrderService.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(CreateCustomerOrderModel model)
        {
            var result = await _customerOrderService.AddAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AddOrderProduct")]
        public async Task<IActionResult> AddOrderItem(CreateOrderProductModel model)
        {
            var result = await _customerOrderService.AddOrderItemAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateOrderProduct")]
        public IActionResult UpdateOrderItem(UpdateOrderProductModel model)
        {
            var result = _customerOrderService.UpdateOrderItem(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateCustomerOrderAddress(UpdateCustomerOrderAddressModel model)
        {
            var result = await _customerOrderService.UpdateCustomerOrderAddressAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteOrderProduct")]
        public async Task<IActionResult> DeleteOrderItem(DeleteOrderProductModel model)
        {
            var result = await _customerOrderService.DeleteOrderItemAsync(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(DeleteCustomerOrderModel model)
        {
            var result = await _customerOrderService.Delete(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}