namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class DeleteBookRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteBookRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
