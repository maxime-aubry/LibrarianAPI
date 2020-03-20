using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IBookRepository : Librarian.Core.DataTransfertObject.GatewayResponses.Repositories.IRepository<Librarian.Core.Domain.Entities.Book>
    {
        Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>> GetByFilters(string title, IEnumerable<int> categories, IEnumerable<string> authorIds);
        Task<GateawayResponse<string>> AddCopies(string id, int numberOfCopies);
        Task<GateawayResponse<string>> ReduceCopies(string id, int numberOfCopies);
    }
}
