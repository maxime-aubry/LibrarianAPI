using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.DeleteLoan
{
    public interface IDeleteLoanUseCase : IUseCaseRequestHandler<DeleteLoanRequest, UseCaseResponseMessage<string>>
    {
    }
}
