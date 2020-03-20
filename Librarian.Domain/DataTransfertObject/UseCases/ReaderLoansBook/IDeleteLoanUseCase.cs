namespace Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook
{
    public interface IDeleteLoanUseCase : IUseCaseRequestHandler<DeleteLoanRequest, UseCaseResponseMessage<string>>
    {
    }
}
