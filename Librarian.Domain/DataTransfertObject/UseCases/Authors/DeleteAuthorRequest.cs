namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class DeleteAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteAuthorRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
