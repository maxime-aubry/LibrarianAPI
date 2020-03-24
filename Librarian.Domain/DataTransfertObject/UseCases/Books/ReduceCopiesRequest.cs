namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class ReduceCopiesRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public ReduceCopiesRequest(string bookId, int numberOfCopies)
        {
            this.BookId = bookId;
            this.NumberOfCopies = numberOfCopies;
        }

        public string BookId { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
