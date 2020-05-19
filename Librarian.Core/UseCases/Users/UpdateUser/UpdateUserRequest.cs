using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Users.UpdateUser
{
    public class UpdateUserRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateUserRequest(string userId, string firstName, string lastName)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
