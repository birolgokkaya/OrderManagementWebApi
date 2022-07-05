using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Application.Repositories;
using OrderManagement.Application.Services;
using OrderManagement.Infrastructure.Services;
using OrderManagement.Persistence.Contexts;
using OrderManagement.Persistence.Repositories;

namespace OrderManagement.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<OrderManagementDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddScoped<ICustomerOrderService, CustomerOrderService>();        
            services.AddScoped<IProductService, ProductService>();        
            services.AddScoped<IProductRepository, ProductRepository>();        
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped(typeof(IElasticSearchRepository<>), typeof(ElasticSearchRepository<>));
        }
    }
}