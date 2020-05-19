using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Authors.GetAuthorById
{
    public interface IGetAuthorByIdUseCase : IUseCaseRequestHandler<GetAuthorByIdRequest, UseCaseResponseMessage<Author>>
    {
    }
}
