using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Users.CreateUser
{
    public interface ICreateUserUseCase : IUseCaseRequestHandler<CreateUserRequest, UseCaseResponseMessage<string>>
    {
    }
}
