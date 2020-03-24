using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.DataTransfertObject.GatewayResponses.Repositories
{
    public interface IRepository<TResult>
        where TResult : class
    {
        Task<IEnumerable<TResult>> Get();
        Task<TResult> Get(string id);
        Task<string> Add(TResult model);
        Task<string> Update(string id, TResult model);
        Task Delete(string id);
    }
}
