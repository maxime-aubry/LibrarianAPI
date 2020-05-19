using AutoMapper;
using Librarian.Core.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class UserRepository : Repository<Librarian.Infrastructure.Entities.User, Librarian.Core.Domain.Entities.User>, IUserRepository
    {
        public UserRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
