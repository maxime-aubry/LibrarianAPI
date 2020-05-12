namespace Librarian.Core.DataTransfertObject.UseCases.Users
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
