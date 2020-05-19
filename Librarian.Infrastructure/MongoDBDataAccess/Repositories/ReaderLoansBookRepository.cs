using AutoMapper;
using Librarian.Core.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ReaderLoansBookRepository : Repository<Librarian.Infrastructure.Entities.ReaderLoansBook, Librarian.Core.Domain.Entities.ReaderLoansBook>, IReaderLoansBookRepository
    {
        public ReaderLoansBookRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
