using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.DeleteLoan
{
    public class DeleteLoanRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteLoanRequest(string loanId)
        {
            this.LoanId = loanId;
        }

        public string LoanId { get; set; }
    }
}
