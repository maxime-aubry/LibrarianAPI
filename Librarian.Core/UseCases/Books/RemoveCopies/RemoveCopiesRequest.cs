using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Books.RemoveCopies
{
    public class RemoveCopiesRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public RemoveCopiesRequest(string bookId, int numberOfCopies)
        {
            this.BookId = bookId;
            this.NumberOfCopies = numberOfCopies;
        }

        public string BookId { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
