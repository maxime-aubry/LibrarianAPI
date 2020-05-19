using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.AuthorWritesBook.GetBooksByAuthorId
{
    public interface IGetBooksByAuthorIdUseCase : IUseCaseRequestHandler<GetBooksByAuthorIdRequest, UseCaseResponseMessage<IEnumerable<Book>>>
    {
    }
}
