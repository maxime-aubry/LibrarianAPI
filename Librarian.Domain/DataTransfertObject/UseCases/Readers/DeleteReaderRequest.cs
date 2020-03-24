namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class DeleteReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteReaderRequest(string id)
        {
            this.ReaderId = id;
        }

        public string ReaderId { get; set; }
    }
}
