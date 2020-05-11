using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class UserRepository : Repository<Librarian.Infrastructure.Entities.User, Librarian.Core.Domain.Entities.User>, IUserRepository
    {
        public UserRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
