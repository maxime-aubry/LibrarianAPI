using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ReaderRatesBookRepository : Repository<Librarian.Infrastructure.Entities.ReaderRatesBook, Librarian.Core.Domain.Entities.ReaderRatesBook>, IReaderRatesBookRepository
    {
        public ReaderRatesBookRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
