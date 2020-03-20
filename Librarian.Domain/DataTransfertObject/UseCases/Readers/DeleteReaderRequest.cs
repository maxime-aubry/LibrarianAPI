namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class DeleteReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteReaderRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
