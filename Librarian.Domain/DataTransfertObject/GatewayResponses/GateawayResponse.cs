using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.GatewayResponses
{
    public class GateawayResponse<TResult>
    {
        public GateawayResponse(TResult data, bool success = false, IEnumerable<Error> erros = null)
        {
            this.Success = success;
            this.Errors = erros;
            this.Data = data;
        }

        public bool Success { get; }
        public IEnumerable<Error> Errors { get; }
        public TResult Data { get; }
    }
}
