using Librarian.Infrastructure.MongoDBDataAccess.Base.Attributes;
using Microsoft.Win32.SafeHandles;
using MongoDB.Driver;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(ILibrarianDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            this.Database = client.GetDatabase(settings.DatabaseName);
        }

        private bool disposed = false;
        private SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public IMongoDatabase Database { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                handle.Dispose();

            disposed = true;
        }

        /// <summary>
        /// Get a collection from database.
        /// Always use this method in a repository ! If a collection is deleted, its will be recreated automatically.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>Returns a IMongoCollection of TEntity Type.</returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            CollectionInfoAttribute collectionInfo = typeof(TEntity).GetCustomAttribute(typeof(CollectionInfoAttribute), true) as CollectionInfoAttribute;

            if (collectionInfo == null)
                return null;

            IMongoCollection<TEntity> collection = this.Database.GetCollection<TEntity>(collectionInfo.CollectionName);

            if (collection == null)
                this.Database.CreateCollection(collectionInfo.CollectionName);

            collection = this.Database.GetCollection<TEntity>(collectionInfo.CollectionName);
            return collection;
        }

        public async Task CleanDatase()
        {
            await this.GetCollection<Librarian.Infrastructure.Entities.Author>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.AuthorWritesBook>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.Book>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.ReaderLoansBook>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.ReaderRatesBook>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.Reader>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.Shelf>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.UserHasRight>().DeleteManyAsync(item => true);
            await this.GetCollection<Librarian.Infrastructure.Entities.User>().DeleteManyAsync(item => true);
        }
    }
}
