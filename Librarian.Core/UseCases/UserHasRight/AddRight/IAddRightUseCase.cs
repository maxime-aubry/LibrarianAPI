using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.UserHasRight.AddRight
{
    public interface IAddRightUseCase : IUseCaseRequestHandler<AddRightRequest, UseCaseResponseMessage<string>>
    {
    }
}
