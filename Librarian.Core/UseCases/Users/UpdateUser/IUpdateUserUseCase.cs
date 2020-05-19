using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Users.UpdateUser
{
    public interface IUpdateUserUseCase : IUseCaseRequestHandler<UpdateUserRequest, UseCaseResponseMessage<string>>
    {
    }
}
