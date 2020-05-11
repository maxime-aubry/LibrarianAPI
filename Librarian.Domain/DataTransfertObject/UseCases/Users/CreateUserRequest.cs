namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public class CreateUserRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateUserRequest(string firstName, string lastName, string login)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Login = login;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
    }
}
