using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class GetBooksRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Book>>>
    {
        public GetBooksRequest()
        {
        }
    }
}
