using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;

namespace Librarian.Core.UseCases
{
    public class ReaderLoansBookUseCasesProvider : IReaderLoansBookUseCasesProvider
    {
        public ReaderLoansBookUseCasesProvider(
            IGetLoansUseCase getLoans,
            IAddLoanUseCase addLoan,
            ICloseLoanUseCase closeLoan,
            ICloseLoanAndDeclareAsLostUseCase closeLoanAndDeclareAsLost,
            IDeleteLoanUseCase deleteLoan)
        {
            this.GetLoans = getLoans;
            this.AddLoan = addLoan;
            this.CloseLoan = closeLoan;
            this.CloseLoanAndDeclareAsLost = closeLoanAndDeclareAsLost;
            this.DeleteLoan = deleteLoan;
        }

        public IGetLoansUseCase GetLoans { get; set; }
        public IAddLoanUseCase AddLoan { get; set; }
        public ICloseLoanUseCase CloseLoan { get; set; }
        public ICloseLoanAndDeclareAsLostUseCase CloseLoanAndDeclareAsLost { get; set; }
        public IDeleteLoanUseCase DeleteLoan { get; set; }
    }
}
