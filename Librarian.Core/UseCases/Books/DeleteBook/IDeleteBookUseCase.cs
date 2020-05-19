using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.DeleteBook
{
    public interface IDeleteBookUseCase : IUseCaseRequestHandler<DeleteBookRequest, UseCaseResponseMessage<string>>
    {
    }
}
