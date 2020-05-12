using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Users
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
