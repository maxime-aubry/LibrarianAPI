namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public interface IDeleteUserUseCase : IUseCaseRequestHandler<DeleteUserRequest, UseCaseResponseMessage<string>>
    {
    }
}
