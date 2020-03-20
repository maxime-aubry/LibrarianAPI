using System;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IReaderLoansBookRepository : Librarian.Core.DataTransfertObject.GatewayResponses.Repositories.IRepository<Librarian.Core.Domain.Entities.ReaderLoansBook>
    {
        Task<GateawayResponse<string>> Close(string id);
        Task<GateawayResponse<string>> CloseAndDeclareAsLost(string id);
    }
}
