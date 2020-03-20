using System.Collections.Generic;

namespace Librarian.RestFulAPI.Tests.Tools
{
    public class ContentResult<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public TResult Result { get; set; }
    }
}
