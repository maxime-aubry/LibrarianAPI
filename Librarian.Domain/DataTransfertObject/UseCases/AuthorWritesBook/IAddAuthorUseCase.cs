namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public interface IAddAuthorUseCase : IUseCaseRequestHandler<AddAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
