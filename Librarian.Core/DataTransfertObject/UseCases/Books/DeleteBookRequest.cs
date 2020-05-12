namespace Librarian.Core.DataTransfertObject.UseCases.Books
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
