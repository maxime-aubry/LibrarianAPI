using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.CreateBook
{
    public interface ICreateBookUseCase : IUseCaseRequestHandler<CreateBookRequest, UseCaseResponseMessage<string>>
    {
    }
}
