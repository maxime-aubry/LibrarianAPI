using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public class AddAuthorsRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<string>>>
    {
        public AddAuthorsRequest(string bookId, List<string> authorIds)
        {
            this.BookId = bookId;
            this.AuthorIds = authorIds;
        }

        public string BookId { get; set; }
        public List<string> AuthorIds { get; set; }
    }
}
