using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.CloseLoanAndDeclareAsLost
{
    public class CloseLoanAndDeclareAsLostRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CloseLoanAndDeclareAsLostRequest(string loanId)
        {
            this.LoanId = loanId;
        }

        public string LoanId { get; set; }
    }
}
