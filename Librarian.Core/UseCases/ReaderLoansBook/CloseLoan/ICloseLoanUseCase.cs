using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.CloseLoan
{
    public interface ICloseLoanUseCase : IUseCaseRequestHandler<CloseLoanRequest, UseCaseResponseMessage<string>>
    {
    }
}
