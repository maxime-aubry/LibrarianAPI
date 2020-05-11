using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public class GetUsersRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<User>>>
    {
        public GetUsersRequest()
        {
        }
    }
}
