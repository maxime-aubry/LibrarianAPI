using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Users.GetUserById
{
    public class GetUserByIdRequest : IUseCaseRequest<UseCaseResponseMessage<User>>
    {
        public GetUserByIdRequest(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }
    }
}
