namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class LibrarianDatabaseSettings : ILibrarianDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AuthorsCollectionName { get; set; }
        public string AuthorWritesBookCollectionName { get; set; }
        public string BooksCollectionName { get; set; }
        public string ReadersCollectionName { get; set; }
        public string ReaderLoansBookCollectionName { get; set; }
        public string ReaderRatesBookCollectionName { get; set; }
        public string ShelvesCollectionName { get; set; }
        public string UserHasRightCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
