using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Readers.DeleteReader
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
