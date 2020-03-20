using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public class GetAuthorsByBookIdRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>>
    {
        public GetAuthorsByBookIdRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
