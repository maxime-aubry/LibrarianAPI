using Librarian.Core.Domain.Enums;

namespace Librarian.Core.Domain.Entities
{
    public class Shelf
    {
        public Shelf()
        {

        }

        public Shelf(string id, string name, int maxQtyOfBooks, int qtyOfRemainingPlaces, EFloor floor, EBookCategory bookCategory)
            : base()
        {
            this.Id = id;
            this.Name = name;
            this.MaxQtyOfBooks = maxQtyOfBooks;
            this.QtyOfRemainingPlaces = qtyOfRemainingPlaces;
            this.Floor = floor;
            this.BookCategory = bookCategory;
        }

        public Shelf(string name, int maxQtyOfBooks, int qtyOfRemainingPlaces, EFloor floor, EBookCategory bookCategory)
            : base()
        {
            this.Name = name;
            this.MaxQtyOfBooks = maxQtyOfBooks;
            this.QtyOfRemainingPlaces = qtyOfRemainingPlaces;
            this.Floor = floor;
            this.BookCategory = bookCategory;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int MaxQtyOfBooks { get; set; }
        public int QtyOfRemainingPlaces { get; set; }
        public EFloor Floor { get; set; }
        public EBookCategory BookCategory { get; set; }
    }
}
