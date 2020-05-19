using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Books.GetBooks
{
    public class GetBooksRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Book>>>
    {
        public GetBooksRequest()
        {
        }
    }
}
