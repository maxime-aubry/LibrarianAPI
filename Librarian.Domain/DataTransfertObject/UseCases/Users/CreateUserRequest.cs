namespace Librarian.Core.DataTransfertObject.UseCases.Users
{
    public class CreateUserRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateUserRequest(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
    }
}
