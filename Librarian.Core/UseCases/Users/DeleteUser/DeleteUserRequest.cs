using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteUserRequest(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }
    }
}
