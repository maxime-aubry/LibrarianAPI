using AutoMapper;
using Librarian.Core.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class ReaderRatesBookRepository : Repository<Librarian.Infrastructure.Entities.ReaderRatesBook, Librarian.Core.Domain.Entities.ReaderRatesBook>, IReaderRatesBookRepository
    {
        public ReaderRatesBookRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
