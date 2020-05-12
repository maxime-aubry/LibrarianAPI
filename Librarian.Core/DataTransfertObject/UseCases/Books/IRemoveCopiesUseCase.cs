namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IRemoveCopiesUseCase : IUseCaseRequestHandler<ReduceCopiesRequest, UseCaseResponseMessage<string>>
    {
    }
}
