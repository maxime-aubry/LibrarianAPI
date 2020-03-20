namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public interface ILibrarianDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AuthorsCollectionName { get; set; }
        string AuthorWritesBookCollectionName { get; set; }
        string BooksCollectionName { get; set; }
        string ReadersCollectionName { get; set; }
        string ReaderLoansBookCollectionName { get; set; }
        string ReaderRatesBookCollectionName { get; set; }
        string ShelvesCollectionName { get; set; }
    }
}
