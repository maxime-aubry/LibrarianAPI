using AutoMapper;
using Librarian.Core.Repositories;
using Librarian.Infrastructure.MongoDBDataAccess.Base;

namespace Librarian.Infrastructure.MongoDBDataAccess.Repositories
{
    public class BookRepository : Repository<Librarian.Infrastructure.Entities.Book, Librarian.Core.Domain.Entities.Book>, IBookRepository
    {
        public BookRepository(ILibrarianDatabaseSettings settings, IMapper mapper)
            : base(settings, mapper)
        {
        }
    }
}
