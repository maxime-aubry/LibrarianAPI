using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class AuthorWritesBookRepository : Repository<Librarian.Infrastructure.Entities.AuthorWritesBook, Librarian.Core.Domain.Entities.AuthorWritesBook>, IAuthorWritesBookRepository
    {
        public AuthorWritesBookRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
