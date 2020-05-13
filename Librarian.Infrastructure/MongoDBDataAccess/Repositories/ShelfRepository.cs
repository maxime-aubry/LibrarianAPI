using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ShelfRepository : Repository<Librarian.Infrastructure.Entities.Shelf, Librarian.Core.Domain.Entities.Shelf>, IShelfRepository
    {
        public ShelfRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
