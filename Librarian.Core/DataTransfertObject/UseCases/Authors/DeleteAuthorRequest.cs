namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class DeleteAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteAuthorRequest(string id)
        {
            this.AuthorId = id;
        }

        public string AuthorId { get; set; }
    }
}
