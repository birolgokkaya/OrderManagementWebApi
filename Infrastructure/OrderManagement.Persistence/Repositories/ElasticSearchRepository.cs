using Microsoft.Extensions.Configuration;
using Nest;
using OrderManagement.Application.Repositories;

namespace OrderManagement.Persistence.Repositories
{
    public class ElasticSearchRepository<T> : IElasticSearchRepository<T> where T : class
    {
        private readonly IElasticClient _client;
        private readonly string _indexName;
        public ElasticSearchRepository(IConfiguration configuration)
        {
            var node = new Uri(configuration.GetConnectionString("ElasticSearch"));
            var settings = new ConnectionSettings(node);
            _client = new ElasticClient(settings);
            _indexName = typeof(T).Name.ToLower();
        }

        public void Add(T entity)
        {
            _client.Index(entity, idx => idx.Index(_indexName));
        }

        public void Delete(T entity)
        {
            _client.Delete<T>(entity, idx => idx.Index(_indexName));
        }

        public async Task<ISearchResponse<T>> Search(SearchDescriptor<T> predicate)
        {
            predicate.Index(_indexName);
            var result = await _client.SearchAsync<T>(predicate);

            return result;
        }

        public void Update(T entity)
        {
            _client.Index(entity, idx => idx.Index(_indexName));
        }
    }
}