using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Readers.GetReaderById
{
    public class GetReaderByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Reader>>
    {
        public GetReaderByIdRequest(string readerId)
        {
            this.ReaderId = readerId;
        }

        public string ReaderId { get; set; }
    }
}
