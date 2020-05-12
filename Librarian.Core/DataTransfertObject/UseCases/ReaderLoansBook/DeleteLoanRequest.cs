namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
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
