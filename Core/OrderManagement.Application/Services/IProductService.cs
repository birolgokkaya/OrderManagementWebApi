using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Results;
using OrderManagement.Application.Services.Base;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services
{
    public interface IProductService : IService<Product>
    {
        Task<IResult> AddAsync(CreateProductModel model);
        Task<IResult> Delete(DeleteProductModel model);
        IResult Update(UpdateProductModel model);
        Task<IDataResult<List<Product>>> Search(SearchProductModel model);
    }
}