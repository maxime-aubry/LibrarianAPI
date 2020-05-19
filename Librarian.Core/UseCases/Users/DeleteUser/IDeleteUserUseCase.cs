using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Users.DeleteUser
{
    public interface IDeleteUserUseCase : IUseCaseRequestHandler<DeleteUserRequest, UseCaseResponseMessage<string>>
    {
    }
}
