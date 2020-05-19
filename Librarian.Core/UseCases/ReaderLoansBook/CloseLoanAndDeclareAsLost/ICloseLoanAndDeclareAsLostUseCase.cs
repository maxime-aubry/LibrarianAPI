using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderLoansBook.CloseLoanAndDeclareAsLost
{
    public interface ICloseLoanAndDeclareAsLostUseCase : IUseCaseRequestHandler<CloseLoanAndDeclareAsLostRequest, UseCaseResponseMessage<string>>
    {
    }
}
