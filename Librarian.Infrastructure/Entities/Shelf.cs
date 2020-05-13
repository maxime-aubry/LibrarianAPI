using Librarian.Infrastructure.MongoDBDataAccess.Base.Attributes;

namespace Librarian.Infrastructure.Entities
{
    [CollectionInfo(CollectionName = "Shelves")]
    public class Shelf : BaseObject
    {
        public Shelf(string id, string name, int maxQtyOfBooks, int qtyOfRemainingPlaces, int floor, int bookCategory)
            : base(id)
        {
            this.Name = name;
            this.MaxQtyOfBooks = maxQtyOfBooks;
            this.QtyOfRemainingPlaces = qtyOfRemainingPlaces;
            this.Floor = floor;
            this.BookCategory = bookCategory;
        }

        public string Name { get; set; }
        public int MaxQtyOfBooks { get; set; }
        public int QtyOfRemainingPlaces { get; set; }
        public int Floor { get; set; }
        public int BookCategory { get; set; }
    }
}
