namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class UpdateAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateAuthorRequest(string id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
