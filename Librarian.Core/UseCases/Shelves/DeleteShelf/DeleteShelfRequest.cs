using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Shelves.DeleteShelf
{
    public class DeleteShelfRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteShelfRequest(string id)
        {
            this.ShelfId = id;
        }

        public string ShelfId { get; set; }
    }
}
