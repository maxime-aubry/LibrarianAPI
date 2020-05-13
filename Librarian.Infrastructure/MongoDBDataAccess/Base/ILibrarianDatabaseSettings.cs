namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public interface ILibrarianDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
