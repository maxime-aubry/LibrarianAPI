using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.UserHasRight.DeleteRight
{
    public interface IDeleteRightUseCase : IUseCaseRequestHandler<DeleteRightRequest, UseCaseResponseMessage<string>>
    {
    }
}
