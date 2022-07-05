using Nest;
using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Repositories;
using OrderManagement.Application.Results;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IElasticSearchRepository<Product> _elasticSearchRepository;

        public ProductService(IProductRepository productRepository, IElasticSearchRepository<Product> elasticSearchRepository)
        {
            _productRepository = productRepository;
            _elasticSearchRepository = elasticSearchRepository;
        }

        public async Task<IDataResult<List<Product>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return new SuccessDataResult<List<Product>>(products);
        }

        public async Task<IResult> AddAsync(CreateProductModel model)
        {
            Product product = new();
            product.Name = model.Name;
            product.Barcode = model.Barcode;
            product.Description = model.Description;
            product.Price = model.Price;

            var result = await _productRepository.AddAsync(product);
            if (!result) return new ErrorResult();

            _elasticSearchRepository.Add(product);

            return new SuccessResult();
        }

        public async Task<IResult> Delete(DeleteProductModel model)
        {
            var deleteProduct = await _productRepository.GetAsync(x => x.Id == model.Id);
            if (deleteProduct is null) return new ErrorResult("Product cannot be found!");

            var result = _productRepository.Delete(deleteProduct);
            if (!result) return new ErrorResult();

            _elasticSearchRepository.Delete(deleteProduct);

            return new SuccessResult("Deleted successfully");
        }

        public async Task<IDataResult<Product>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetAsync(x => x.Id == id);
            return new SuccessDataResult<Product>(product);
        }

        public IResult Update(UpdateProductModel model)
        {
            var updateProduct = _productRepository.GetAsNoTrackingAsync(x => x.Id == model.Id).Result;
            if (updateProduct is null) return new ErrorResult("Product cannot be found!");

            Product product = new();
            product.Id = model.Id;
            product.Name = model.Name;
            product.Barcode = model.Barcode;
            product.Description = model.Description;
            product.Price = model.Price;

            var result = _productRepository.Update(product);

            if (!result) return new ErrorResult();
            _elasticSearchRepository.Update(product);

            return new SuccessResult();
        }

        public async Task<IDataResult<List<Product>>> Search(SearchProductModel model)
        {
            var searchDescriptor = new SearchDescriptor<Product>();
            searchDescriptor.Query(q => q.QueryString(m => m.Fields(f => f.Field(fi => fi.Name)).Query($"*{model.ProductName}*")));
            var search = await _elasticSearchRepository.Search(searchDescriptor);
            var result = search.Documents.ToList();
            return new SuccessDataResult<List<Product>>(result);
        }
    }
}