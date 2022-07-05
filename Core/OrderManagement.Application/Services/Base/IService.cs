using OrderManagement.Application.Results;

namespace OrderManagement.Application.Services.Base
{
    public interface IService<T> where T : class
    {
        Task<IDataResult<List<T>>> GetAllAsync();
        Task<IDataResult<T>> GetByIdAsync(int id);
    }
}