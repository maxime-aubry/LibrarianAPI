using Librarian.Core.DataTransfertObject;
using Newtonsoft.Json;
using System.Net;

namespace Librarian.RestFulAPI.Tools.Presenters
{
    public class JsonPresenter<TResult> : IJsonPresenter<TResult>
    {
        public JsonPresenter()
        {
            this.ContentResult = new JsonContentResult();
        }

        public JsonContentResult ContentResult { get; set; }

        public void Handle(UseCaseResponseMessage<TResult> response)
        {
            this.ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonConvert.SerializeObject(response);
        }
    }
}
