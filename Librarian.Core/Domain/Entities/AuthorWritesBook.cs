namespace Librarian.Core.Domain.Entities
{
    public class AuthorWritesBook
    {
        public AuthorWritesBook()
        {

        }

        public AuthorWritesBook(string id, string authorId, string bookId)
            : base()
        {
            this.Id = id;
            this.AuthorId = authorId;
            this.BookId = bookId;
        }

        public AuthorWritesBook(string authorId, string bookId)
            : base()
        {
            this.Id = string.Empty;
            this.AuthorId = authorId;
            this.BookId = bookId;
        }

        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string BookId { get; set; }
    }
}
