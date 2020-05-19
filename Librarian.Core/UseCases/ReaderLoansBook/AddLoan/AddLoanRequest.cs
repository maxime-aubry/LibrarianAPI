using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.AddLoan
{
    public class AddLoanRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public AddLoanRequest(string readerId, string bookId)
        {
            this.ReaderId = readerId;
            this.BookId = bookId;
        }

        public string ReaderId { get; set; }
        public string BookId { get; set; }
    }
}
