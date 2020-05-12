using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public interface IGetUsersUseCase : IUseCaseRequestHandler<GetUsersRequest, UseCaseResponseMessage<IEnumerable<User>>>
    {
    }
}
