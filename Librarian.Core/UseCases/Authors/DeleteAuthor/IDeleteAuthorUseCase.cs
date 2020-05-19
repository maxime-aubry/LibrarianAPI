using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Authors.DeleteAuthor
{
    public interface IDeleteAuthorUseCase : IUseCaseRequestHandler<DeleteAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
