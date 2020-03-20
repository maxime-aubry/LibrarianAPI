namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public interface IDeleteReaderUseCase : IUseCaseRequestHandler<DeleteReaderRequest, UseCaseResponseMessage<string>>
    {
    }
}
