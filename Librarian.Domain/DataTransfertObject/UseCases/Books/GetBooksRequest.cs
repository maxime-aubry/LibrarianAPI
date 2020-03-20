using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class GetBooksRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>>
    {
        public GetBooksRequest()
        {
        }
    }
}
