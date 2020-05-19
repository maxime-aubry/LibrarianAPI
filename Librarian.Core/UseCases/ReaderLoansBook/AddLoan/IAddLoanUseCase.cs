using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.AddLoan
{
    public interface IAddLoanUseCase : IUseCaseRequestHandler<AddLoanRequest, UseCaseResponseMessage<string>>
    {
    }
}
