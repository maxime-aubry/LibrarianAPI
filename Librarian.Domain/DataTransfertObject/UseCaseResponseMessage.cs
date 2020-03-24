using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject
{
    public class UseCaseResponseMessage<TResult>
    {
        public UseCaseResponseMessage(TResult result, bool success = false, string message = null)
        {
            this.Success = success;
            this.Message = message;
            this.Result = result;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Result { get; }
    }
}
