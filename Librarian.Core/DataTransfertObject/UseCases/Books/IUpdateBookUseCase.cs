namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IUpdateBookUseCase : IUseCaseRequestHandler<UpdateBookRequest, UseCaseResponseMessage<string>>
    {
    }
}
