using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IGetAuthorByIdUseCase : IUseCaseRequestHandler<GetAuthorByIdRequest, UseCaseResponseMessage<Author>>
    {
    }
}
