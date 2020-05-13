using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class AuthorRepository : Repository<Librarian.Infrastructure.Entities.Author, Librarian.Core.Domain.Entities.Author>, IAuthorRepository
    {
        public AuthorRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
