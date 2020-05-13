using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ReaderRepository : Repository<Librarian.Infrastructure.Entities.Reader, Librarian.Core.Domain.Entities.Reader>, IReaderRepository
    {
        public ReaderRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
