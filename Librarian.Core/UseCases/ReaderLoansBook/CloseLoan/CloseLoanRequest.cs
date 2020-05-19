using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.CloseLoan
{
    public class CloseLoanRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CloseLoanRequest(string loanId)
        {
            this.LoanId = loanId;
        }

        public string LoanId { get; set; }
    }
}
