namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class DeleteShelfRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteShelfRequest(string id)
        {
            this.ShelfId = id;
        }

        public string ShelfId { get; set; }
    }
}
