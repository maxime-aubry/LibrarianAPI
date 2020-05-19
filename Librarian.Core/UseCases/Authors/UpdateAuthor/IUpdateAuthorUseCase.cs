using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Authors.UpdateAuthor
{
    public interface IUpdateAuthorUseCase : IUseCaseRequestHandler<UpdateAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
