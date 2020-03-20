using Librarian.Core.Domain.Enums;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class UpdateShelfRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateShelfRequest(string id, int maxQtyOfBooks, EFloor floor, EBookCategory bookCategory)
        {
            this.Id = id;
            this.MaxQtyOfBooks = maxQtyOfBooks;
            this.Floor = floor;
            this.BookCategory = bookCategory;
        }

        public string Id { get; set; }
        public int MaxQtyOfBooks { get; set; }
        public EFloor Floor { get; set; }
        public EBookCategory BookCategory { get; set; }
    }
}
