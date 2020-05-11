using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ShelfRepository : Repository<Librarian.Infrastructure.Entities.Shelf, Librarian.Core.Domain.Entities.Shelf>, IShelfRepository
    {
        public ShelfRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
