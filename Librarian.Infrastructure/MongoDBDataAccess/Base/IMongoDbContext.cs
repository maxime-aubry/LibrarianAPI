using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public interface IMongoDbContext : IDisposable
    {
        IMongoDatabase Database { get; }
        IMongoCollection<TEntity> GetCollection<TEntity>();
        Task CleanDatase();
    }
}
