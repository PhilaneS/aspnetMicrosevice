using Catelog.API.Data;
using Catelog.API.Entities;
using MongoDB.Driver;
using ZstdSharp;

namespace Catelog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatelogContext _catelogContext;

        public ProductRepository(ICatelogContext catelogContext)
        {
            _catelogContext = catelogContext ?? throw new ArgumentNullException(nameof(catelogContext));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catelogContext.Products.Find(p => true).ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _catelogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p=> p.Name, name);            
            return await _catelogContext.Products.Find(filterDefinition).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            FilterDefinition<Product> filterDefinition = 
                Builders<Product>.Filter.Eq(p => p.Name, CategoryName);

            return await _catelogContext.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _catelogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catelogContext
                .Products
                .ReplaceOneAsync(filter: g => g.Id ==product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filterDefinition =
                Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _catelogContext.Products.DeleteOneAsync(filterDefinition);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
