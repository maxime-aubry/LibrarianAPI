using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;

namespace Librarian.Core.UseCases.Shelves.GetShelfById
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
