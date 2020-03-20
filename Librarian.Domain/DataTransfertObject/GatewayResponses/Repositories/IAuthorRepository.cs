using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IAuthorRepository : Librarian.Core.DataTransfertObject.GatewayResponses.Repositories.IRepository<Librarian.Core.Domain.Entities.Author>
    {
        Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>> GetByFilters(string firstName, string lastName);
    }
}
