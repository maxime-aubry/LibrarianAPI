using Librarian.Core.DataTransfertObject;

namespace Librarian.RestFulAPI.Tools.Presenters
{
    public interface IPresenter<TResult> : IOutputPort<UseCaseResponseMessage<TResult>>
    {
    }
}
