using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.AuthorWritesBook.GetAuthorsByBookId
{
    public interface IGetAuthorsByBookIdUseCase : IUseCaseRequestHandler<GetAuthorsByBookIdRequest, UseCaseResponseMessage<IEnumerable<Author>>>
    {
    }
}
