using MongoDB.Driver;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<Librarian.Infrastructure.Entities.Author> Authors { get; set; }
        IMongoCollection<Librarian.Infrastructure.Entities.AuthorWritesBook> AuthorWritesBook { get; set; }
        IMongoCollection<Librarian.Infrastructure.Entities.Book> Books { get; set; }
        IMongoCollection<Librarian.Infrastructure.Entities.Reader> Readers { get; set; }
        IMongoCollection<Librarian.Infrastructure.Entities.ReaderLoansBook> ReaderLoansBook { get; set; }
        IMongoCollection<Librarian.Infrastructure.Entities.ReaderRatesBook> ReaderRatesBook { get; set; }
        IMongoCollection<Librarian.Infrastructure.Entities.Shelf> Shelves { get; set; }
        IMongoCollection<TEntity> GetCollection<TEntity>();
        Task CleanDatase();
    }
}
