namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface ICreateBookUseCase : IUseCaseRequestHandler<CreateBookRequest, UseCaseResponseMessage<string>>
    {
    }
}
