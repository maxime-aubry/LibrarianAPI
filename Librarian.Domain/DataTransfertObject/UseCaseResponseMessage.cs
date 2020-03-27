using System.Collections.Generic;
using System.Linq;

namespace Librarian.Core.DataTransfertObject
{
    public class UseCaseResponseMessage<TResult>
    {
        public UseCaseResponseMessage(TResult result, bool success = false, string message = null, IEnumerable<Error> errors = null)
        {
            this.Success = success;
            this.Message = message;
            this.Result = result;
            this.Errors = errors?.Select(e => e.Description);
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public TResult Result { get; }
    }
}
