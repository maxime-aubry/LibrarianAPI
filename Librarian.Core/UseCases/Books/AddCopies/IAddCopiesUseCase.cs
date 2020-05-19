using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.AddCopies
{
    public interface IAddCopiesUseCase : IUseCaseRequestHandler<AddCopiesRequest, UseCaseResponseMessage<string>>
    {
    }
}
