using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;

namespace Librarian.Core.UseCases
{
    public interface IReaderLoansBookUseCasesProvider
    {
        IAddLoanUseCase AddLoan { get; set; }
        ICloseLoanUseCase CloseLoan { get; set; }
        ICloseLoanAndDeclareAsLostUseCase CloseLoanAndDeclareAsLost { get; set; }
        IDeleteLoanUseCase DeleteLoan { get; set; }
    }
}
