namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IDeleteBookUseCase : IUseCaseRequestHandler<DeleteBookRequest, UseCaseResponseMessage<string>>
    {
    }
}
