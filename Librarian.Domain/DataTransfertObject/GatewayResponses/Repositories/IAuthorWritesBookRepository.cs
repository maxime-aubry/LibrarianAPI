using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IAuthorWritesBookRepository : Librarian.Core.DataTransfertObject.GatewayResponses.Repositories.IRepository<Librarian.Core.Domain.Entities.AuthorWritesBook>
    {
        Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>>> GetBooks(string authorId);
        Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>>> GetAuthors(string bookId);
        Task<GateawayResponse<IEnumerable<string>>> AddAuthors(string bookId, List<string> authorIds);
        Task<GateawayResponse<string>> DeleteAuthors(string bookId, List<string> authorIds);
    }
}
