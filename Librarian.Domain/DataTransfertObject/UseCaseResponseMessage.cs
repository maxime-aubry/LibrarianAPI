using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject
{
    public class UseCaseResponseMessage<TResult>
    {
        public UseCaseResponseMessage(IEnumerable<string> errors, bool success = false, string message = null)
        {
            this.Errors = errors;
            this.Success = success;
            this.Message = message;
            this.Errors = errors;
        }

        public UseCaseResponseMessage(TResult result, bool success = false, string message = null)
        {
            this.Errors = null;
            this.Success = success;
            this.Message = message;
            this.Result = result;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; }
        public TResult Result { get; }
    }
}
