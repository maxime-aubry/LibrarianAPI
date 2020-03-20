using Microsoft.AspNetCore.Mvc;

namespace Librarian.RestFulAPI.Tools.Presenters
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            this.ContentType = "application/json";
        }
    }
}
