using Librarian.Core.DataTransfertObject;

namespace Librarian.HexagonalArchitecture.Tools.Presenters
{
    public interface IPresenter<TResult> : IOutputPort<UseCaseResponseMessage<TResult>>
    {
    }
}
