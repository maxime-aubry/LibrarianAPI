using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Users.GetUserById
{
    public interface IGetUserByIdUseCase : IUseCaseRequestHandler<GetUserByIdRequest, UseCaseResponseMessage<User>>
    {
    }
}
