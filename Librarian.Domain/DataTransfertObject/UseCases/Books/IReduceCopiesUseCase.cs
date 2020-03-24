namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IReduceCopiesUseCase : IUseCaseRequestHandler<ReduceCopiesRequest, UseCaseResponseMessage<string>>
    {
    }
}
