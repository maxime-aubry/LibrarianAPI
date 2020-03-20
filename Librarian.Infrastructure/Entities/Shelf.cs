namespace Librarian.Infrastructure.Entities
{
    public class Shelf : BaseObject
    {
        public Shelf(string id, string name, int maxQtyOfBooks, int floor, int bookCategory)
            : base(id)
        {
            this.Name = name;
            this.MaxQtyOfBooks = maxQtyOfBooks;
            this.Floor = floor;
            this.BookCategory = bookCategory;
        }

        public string Name { get; set; }
        public int MaxQtyOfBooks { get; set; }
        public int Floor { get; set; }
        public int BookCategory { get; set; }
    }
}
