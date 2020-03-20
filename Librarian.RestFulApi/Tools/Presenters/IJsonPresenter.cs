namespace Librarian.RestFulAPI.Tools.Presenters
{
    public interface IJsonPresenter<TResult> : IPresenter<TResult>
    {
        JsonContentResult ContentResult { get; set; }
    }
}
