namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class AddCopiesRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public AddCopiesRequest(string bookId, int numberOfCopies)
        {
            this.BookId = bookId;
            this.NumberOfCopies = numberOfCopies;
        }

        public string BookId { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
