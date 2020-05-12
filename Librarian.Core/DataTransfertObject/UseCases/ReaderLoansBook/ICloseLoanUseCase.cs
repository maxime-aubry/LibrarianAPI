namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public interface ICloseLoanUseCase : IUseCaseRequestHandler<CloseLoanRequest, UseCaseResponseMessage<string>>
    {
    }
}
