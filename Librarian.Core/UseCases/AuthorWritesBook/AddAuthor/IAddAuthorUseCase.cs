using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.AuthorWritesBook.AddAuthor
{
    public interface IAddAuthorUseCase : IUseCaseRequestHandler<AddAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
