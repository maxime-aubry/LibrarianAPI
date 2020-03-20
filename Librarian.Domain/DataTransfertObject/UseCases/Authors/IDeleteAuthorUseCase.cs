namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IDeleteAuthorUseCase : IUseCaseRequestHandler<DeleteAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
