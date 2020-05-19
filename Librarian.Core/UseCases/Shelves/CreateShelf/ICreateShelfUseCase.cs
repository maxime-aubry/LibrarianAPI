using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Shelves.CreateShelf
{
    public interface ICreateShelfUseCase : IUseCaseRequestHandler<CreateShelfRequest, UseCaseResponseMessage<string>>
    {
    }
}
