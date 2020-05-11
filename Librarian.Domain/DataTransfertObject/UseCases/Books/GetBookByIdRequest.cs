using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class GetBookByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Book>>
    {
        public GetBookByIdRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
