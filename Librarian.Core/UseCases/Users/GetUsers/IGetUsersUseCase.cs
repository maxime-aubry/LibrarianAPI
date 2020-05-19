using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Users.GetUsers
{
    public interface IGetUsersUseCase : IUseCaseRequestHandler<GetUsersRequest, UseCaseResponseMessage<IEnumerable<User>>>
    {
    }
}
