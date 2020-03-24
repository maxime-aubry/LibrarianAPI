using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ReaderLoansBookRepository : Repository<Librarian.Infrastructure.Entities.ReaderLoansBook, Librarian.Core.Domain.Entities.ReaderLoansBook>, IReaderLoansBookRepository
    {
        public ReaderLoansBookRepository(IMongoDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
