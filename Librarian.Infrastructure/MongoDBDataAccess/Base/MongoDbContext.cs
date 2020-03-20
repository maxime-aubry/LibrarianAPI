using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(ILibrarianDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            this.Database = client.GetDatabase(settings.DatabaseName);
            this.collectionNames = new Dictionary<Type, string>()
            {
                { typeof(Librarian.Infrastructure.Entities.Author), settings.AuthorsCollectionName },
                { typeof(Librarian.Infrastructure.Entities.AuthorWritesBook), settings.AuthorWritesBookCollectionName },
                { typeof(Librarian.Infrastructure.Entities.Book), settings.BooksCollectionName },
                { typeof(Librarian.Infrastructure.Entities.Reader), settings.ReadersCollectionName},
                { typeof(Librarian.Infrastructure.Entities.ReaderLoansBook), settings.ReaderLoansBookCollectionName},
                { typeof(Librarian.Infrastructure.Entities.ReaderRatesBook), settings.ReaderRatesBookCollectionName},
                { typeof(Librarian.Infrastructure.Entities.Shelf), settings.ShelvesCollectionName },
            };

            // collections
            this.Authors = this.GetCollection<Librarian.Infrastructure.Entities.Author>();
            this.AuthorWritesBook = this.GetCollection<Librarian.Infrastructure.Entities.AuthorWritesBook>();
            this.Books = this.GetCollection<Librarian.Infrastructure.Entities.Book>();
            this.Readers = this.GetCollection<Librarian.Infrastructure.Entities.Reader>();
            this.ReaderLoansBook = this.GetCollection<Librarian.Infrastructure.Entities.ReaderLoansBook>();
            this.ReaderRatesBook = this.GetCollection<Librarian.Infrastructure.Entities.ReaderRatesBook>();
            this.Shelves = this.GetCollection<Librarian.Infrastructure.Entities.Shelf>();
        }

        private IDictionary<Type, string> collectionNames { get; set; }
        public IMongoDatabase Database { get; private set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.Author> Authors { get; set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.AuthorWritesBook> AuthorWritesBook { get; set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.Book> Books { get; set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.Reader> Readers { get; set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.ReaderLoansBook> ReaderLoansBook { get; set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.ReaderRatesBook> ReaderRatesBook { get; set; }
        public IMongoCollection<Librarian.Infrastructure.Entities.Shelf> Shelves { get; set; }

        /// <summary>
        /// Get a collection from database.
        /// Always use this method in a repository ! If a collection is deleted, its will be recreated automatically.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>Returns a IMongoCollection of TEntity Type.</returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            string collectionName = this.collectionNames[typeof(TEntity)];
            IMongoCollection<TEntity> collection = this.Database.GetCollection<TEntity>(collectionName);
            if (collection == null)
                this.Database.CreateCollection(collectionName);
            collection = this.Database.GetCollection<TEntity>(collectionName);
            return collection;
        }

        public async Task CleanDatase()
        {
            await this.Authors.DeleteManyAsync(item => true);
            await this.AuthorWritesBook.DeleteManyAsync(item => true);
            await this.Books.DeleteManyAsync(item => true);
            await this.ReaderLoansBook.DeleteManyAsync(item => true);
            await this.ReaderRatesBook.DeleteManyAsync(item => true);
            await this.Readers.DeleteManyAsync(item => true);
            await this.Shelves.DeleteManyAsync(item => true);
        }
    }
}
