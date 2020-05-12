namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public interface IDeleteAuthorUseCase : IUseCaseRequestHandler<DeleteAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
