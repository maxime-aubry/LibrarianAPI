namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class GetReaderByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Reader>>
    {
        public GetReaderByIdRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
