using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;

namespace Librarian.Infrastructure.MongoDBDataAccess
{
    public class RepositoryProvider : IRepositoryProvider
    {
        public IAuthorRepository Author { get; set; }
        public IAuthorWritesBookRepository AuthorWritesBooks { get; set; }
        public IBookRepository Books { get; set; }
        public IReaderLoansBookRepository ReaderLoansBook { get; set; }
        public IReaderRatesBookRepository ReaderRatesBook { get; set; }
        public IReaderRepository Reader { get; set; }
        public IShelfRepository Shelves { get; set; }
        public IUserHasRightRepository UserHasRight { get; set; }
        public IUserRepository Users { get; set; }
    }
}
