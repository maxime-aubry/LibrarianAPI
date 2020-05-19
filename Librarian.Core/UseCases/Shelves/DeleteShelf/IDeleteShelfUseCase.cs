using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Shelves.DeleteShelf
{
    public interface IDeleteShelfUseCase : IUseCaseRequestHandler<DeleteShelfRequest, UseCaseResponseMessage<string>>
    {
    }
}
