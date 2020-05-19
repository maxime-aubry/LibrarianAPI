using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Authors.CreateAuthor
{
    public interface ICreateAuthorUseCase : IUseCaseRequestHandler<CreateAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
