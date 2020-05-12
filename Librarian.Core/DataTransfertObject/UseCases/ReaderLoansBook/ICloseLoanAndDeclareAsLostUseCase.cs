namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public interface ICloseLoanAndDeclareAsLostUseCase : IUseCaseRequestHandler<CloseLoanAndDeclareAsLostRequest, UseCaseResponseMessage<string>>
    {
    }
}
