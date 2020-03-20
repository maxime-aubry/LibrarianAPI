namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class GetShelfByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Shelf>>
    {
        public GetShelfByIdRequest(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}
