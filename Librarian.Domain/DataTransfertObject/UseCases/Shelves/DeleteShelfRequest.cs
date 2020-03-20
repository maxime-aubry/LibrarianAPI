namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class DeleteShelfRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteShelfRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
