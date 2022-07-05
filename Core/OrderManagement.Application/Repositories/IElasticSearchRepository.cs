using Nest;

namespace OrderManagement.Application.Repositories
{
    public interface IElasticSearchRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<ISearchResponse<T>> Search(SearchDescriptor<T> predicate);
    }
}