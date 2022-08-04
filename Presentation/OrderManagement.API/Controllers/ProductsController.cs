using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Services;
using Serilog;

namespace OrderManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("ListById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(CreateProductModel model)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            await _productService.AddAsync(model);
            return Ok();
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(UpdateProductModel model)
        {
            var result = _productService.Update(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductModel model)
        {
            var result = await _productService.Delete(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(SearchProductModel model)
        {
            var result = await _productService.Search(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }            
    }
}