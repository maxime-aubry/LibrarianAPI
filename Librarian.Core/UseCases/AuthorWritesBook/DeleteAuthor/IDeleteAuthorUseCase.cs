using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.AuthorWritesBook.DeleteAuthor
{
    public interface IDeleteAuthorUseCase : IUseCaseRequestHandler<DeleteAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
