using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base
{
    public interface IRepository<TEntity, TResult>
        where TEntity : BaseObject
        where TResult : class
    {
        Task<GateawayResponse<IEnumerable<TResult>>> Get();
        Task<GateawayResponse<TResult>> Get(string id);
        Task<GateawayResponse<string>> Add(TResult book);
        Task<GateawayResponse<string>> Update(string id, TResult book);
        Task<GateawayResponse<string>> Delete(string id);
    }
}
