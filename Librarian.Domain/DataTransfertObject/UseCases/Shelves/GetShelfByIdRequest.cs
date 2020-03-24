using Librarian.Core.Domain.Entities;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class GetShelfByIdRequest : IUseCaseRequest<UseCaseResponseMessage<Shelf>>
    {
        public GetShelfByIdRequest(string id)
        {
            this.ShelfId = id;
        }

        public string ShelfId { get; set; }
    }
}
