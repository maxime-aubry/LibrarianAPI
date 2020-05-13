namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public class LibrarianDatabaseSettings : ILibrarianDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
