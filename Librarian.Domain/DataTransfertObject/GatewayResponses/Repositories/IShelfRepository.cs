using Librarian.Core.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IShelfRepository : Librarian.Core.DataTransfertObject.GatewayResponses.Repositories.IRepository<Librarian.Core.Domain.Entities.Shelf>
    {
        Task<GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>> GetAvailableShelves(EBookCategory category, int numberOfCopies);
    }
}
