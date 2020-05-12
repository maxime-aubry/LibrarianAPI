namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
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
