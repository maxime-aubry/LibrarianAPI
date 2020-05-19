using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.RemoveCopies
{
    public interface IRemoveCopiesUseCase : IUseCaseRequestHandler<RemoveCopiesRequest, UseCaseResponseMessage<string>>
    {
    }
}
