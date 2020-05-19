using Librarian.Core.UseCases.ReaderLoansBook.AddLoan;
using Librarian.Core.UseCases.ReaderLoansBook.CloseLoan;
using Librarian.Core.UseCases.ReaderLoansBook.CloseLoanAndDeclareAsLost;
using Librarian.Core.UseCases.ReaderLoansBook.DeleteLoan;
using Librarian.Core.UseCases.ReaderLoansBook.GetLoans;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public interface IReaderLoansBookUseCasesProvider
    {
        IGetLoansUseCase GetLoans { get; set; }
        IAddLoanUseCase AddLoan { get; set; }
        ICloseLoanUseCase CloseLoan { get; set; }
        ICloseLoanAndDeclareAsLostUseCase CloseLoanAndDeclareAsLost { get; set; }
        IDeleteLoanUseCase DeleteLoan { get; set; }
    }
}
