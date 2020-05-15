using Microsoft.AspNetCore.Mvc;

namespace Librarian.HexagonalArchitecture.Tools.Presenters
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            this.ContentType = "application/json";
        }
    }
}
