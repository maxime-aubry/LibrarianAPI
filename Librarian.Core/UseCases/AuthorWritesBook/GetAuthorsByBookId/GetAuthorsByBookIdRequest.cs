using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.AuthorWritesBook.GetAuthorsByBookId
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
