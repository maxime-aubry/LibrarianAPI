using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class GetReaderByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Reader>>
    {
        public GetReaderByIdRequest(string id)
        {
            this.ReaderId = id;
        }

        public string ReaderId { get; set; }
    }
}
