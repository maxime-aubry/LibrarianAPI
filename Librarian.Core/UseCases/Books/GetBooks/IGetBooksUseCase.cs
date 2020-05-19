using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Books.GetBooks
{
    public interface IGetBooksUseCase : IUseCaseRequestHandler<GetBooksRequest, UseCaseResponseMessage<IEnumerable<Book>>>
    {
    }
}
