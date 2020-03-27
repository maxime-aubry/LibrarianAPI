using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IRepository<TResult>
        where TResult : class
    {
        Task<GateawayResponse<IEnumerable<TResult>>> Get();
        Task<GateawayResponse<TResult>> Get(string id);
        Task<GateawayResponse<string>> Add(TResult model);
        Task<GateawayResponse<string>> Update(string id, TResult model);
        Task<GateawayResponse<string>> Delete(string id);
        Task<GateawayResponse<string>> DeleteMany(IEnumerable<string> ids);
    }
}
