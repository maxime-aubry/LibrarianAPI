namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public interface IDeleteShelfUseCase : IUseCaseRequestHandler<DeleteShelfRequest, UseCaseResponseMessage<string>>
    {
    }
}
