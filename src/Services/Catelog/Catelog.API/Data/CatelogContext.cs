using Catelog.API.Entities;
using MongoDB.Driver;

namespace Catelog.API.Data
{
    public class CatelogContext : ICatelogContext
    {
        public CatelogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var databse = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            
            Products = databse.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatelogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
