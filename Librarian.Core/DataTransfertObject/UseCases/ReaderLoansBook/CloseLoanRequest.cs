namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
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
