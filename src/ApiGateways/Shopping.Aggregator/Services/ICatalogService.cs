using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatelog();
        Task<IEnumerable<CatalogModel>> GetCatelogByCategory(string category);
        Task<CatalogModel> GetCatelog(string id);
    }
}
