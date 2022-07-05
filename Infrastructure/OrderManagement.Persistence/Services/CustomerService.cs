using OrderManagement.Application.DTO.Customers;
using OrderManagement.Application.Repositories;
using OrderManagement.Application.Results;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IResult> AddAsync(CreateCustomerModel model)
        {
            Customer customer = new();
            customer.FullName = model.FullName;
            customer.Address = model.Address;

            var result = await _customerRepository.AddAsync(customer);
            return result ? new SuccessResult() : new ErrorResult();
        }

        public async Task<IResult> Delete(DeleteCustomerModel model)
        {
            var deleteCustomer = await _customerRepository.GetAsync(x => x.Id == model.Id);
            if (deleteCustomer is null) return new ErrorResult("Customer cannot be found!");

            var result = _customerRepository.Delete(deleteCustomer);
            return result ? new SuccessResult("Deleted successfully") : new ErrorResult();
        }

        public async Task<IDataResult<List<Customer>>> GetAllAsync()
        {
            var products = await _customerRepository.GetAllAsync();
            return new SuccessDataResult<List<Customer>>(products);
        }

        public async Task<IDataResult<Customer>> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetAsync(x => x.Id == id);
            return new SuccessDataResult<Customer>(customer);
        }

        public IResult Update(UpdateCustomerModel model)
        {
            var updateCustomer = _customerRepository.GetAsNoTrackingAsync(x => x.Id == model.Id).Result;
            if (updateCustomer is null) return new ErrorResult("Customer cannot be found!");

            Customer customer = new();
            customer.Id = model.Id;
            customer.FullName = model.FullName;
            customer.Address = model.Address;

            var result = _customerRepository.Update(customer);
            return result ? new SuccessResult() : new ErrorResult();
        }
    }
}