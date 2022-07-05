using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO.Customers;
using OrderManagement.Application.Services;

namespace OrderManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("ListById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _customerService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCustomer(CreateCustomerModel model)
        {
            if(!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            await _customerService.AddAsync(model);
            return Ok();
        }

        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateCustomer(UpdateCustomerModel model)
        {
            var result = _customerService.Update(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(DeleteCustomerModel model)
        {
            var result = await _customerService.Delete(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}