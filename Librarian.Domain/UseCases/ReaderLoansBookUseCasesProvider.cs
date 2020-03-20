using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;

namespace Librarian.Core.UseCases
{
    public class ReaderLoansBookUseCasesProvider : IReaderLoansBookUseCasesProvider
    {
        public ReaderLoansBookUseCasesProvider(
            IAddLoanUseCase addLoan,
            ICloseLoanUseCase closeLoan,
            ICloseLoanAndDeclareAsLostUseCase closeLoanAndDeclareAsLost,
            IDeleteLoanUseCase deleteLoan)
        {
            this.AddLoan = addLoan;
            this.CloseLoan = closeLoan;
            this.CloseLoanAndDeclareAsLost = closeLoanAndDeclareAsLost;
            this.DeleteLoan = deleteLoan;
        }

        public IAddLoanUseCase AddLoan { get; set; }
        public ICloseLoanUseCase CloseLoan { get; set; }
        public ICloseLoanAndDeclareAsLostUseCase CloseLoanAndDeclareAsLost { get; set; }
        public IDeleteLoanUseCase DeleteLoan { get; set; }
    }
}
