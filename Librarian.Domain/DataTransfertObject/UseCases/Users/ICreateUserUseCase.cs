namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public interface ICreateUserUseCase : IUseCaseRequestHandler<CreateUserRequest, UseCaseResponseMessage<string>>
    {
    }
}
