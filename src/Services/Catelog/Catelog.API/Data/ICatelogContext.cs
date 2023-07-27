using Catelog.API.Entities;
using MongoDB.Driver;

namespace Catelog.API.Data
{
    public interface ICatelogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
