namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class CreateAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateAuthorRequest(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
