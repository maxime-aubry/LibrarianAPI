using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public class GetAuthorsByBookIdRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Author>>>
    {
        public GetAuthorsByBookIdRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
