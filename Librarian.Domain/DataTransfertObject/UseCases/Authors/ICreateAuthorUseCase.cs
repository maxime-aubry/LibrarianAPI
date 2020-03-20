namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface ICreateAuthorUseCase : IUseCaseRequestHandler<CreateAuthorRequest, UseCaseResponseMessage<string>>
    {
    }
}
