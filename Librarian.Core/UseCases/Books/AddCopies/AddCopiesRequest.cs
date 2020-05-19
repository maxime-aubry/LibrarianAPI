using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.AddCopies
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
