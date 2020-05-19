using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.DeleteBook
{
    public class DeleteBookRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteBookRequest(string bookId)
        {
            this.BookId = bookId;
        }

        public string BookId { get; set; }
    }
}
