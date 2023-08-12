using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatelog()
        {
          var response = await  _client.GetAsync("/api/v1/catelog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatelog(string id)
        {
            var response = await _client.GetAsync($"/api/v1/catelog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatelogByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/catelog/GetCatelogByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
