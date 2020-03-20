using Librarian.Core.Domain.Enums;
using System;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class CreateShelfRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateShelfRequest(int maxQtyOfBooks, EFloor floor, EBookCategory bookCategory)
        {
            this.MaxQtyOfBooks = maxQtyOfBooks;
            this.Floor = floor;
            this.BookCategory = bookCategory;
        }

        public int MaxQtyOfBooks { get; set; }
        public EFloor Floor { get; set; }
        public EBookCategory BookCategory { get; set; }
    }
}
