namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class DeleteReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteReaderRequest(string readerId)
        {
            this.ReaderId = readerId;
        }

        public string ReaderId { get; set; }
    }
}
