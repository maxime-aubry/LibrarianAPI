using System;

namespace Librarian.Core.Domain.Entities
{
    public class ReaderLoansBook
    {
        public ReaderLoansBook()
        {

        }

        public ReaderLoansBook(string id, string readerId, string bookId, DateTime dateOfLoaning, DateTime? endDateOfLoaning = null, bool? isLost = null)
            : base()
        {
            this.Id = id;
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.DateOfLoaning = dateOfLoaning;
            this.EndDateOfLoaning = endDateOfLoaning;
            this.IsLost = isLost;
        }

        public ReaderLoansBook(string readerId, string bookId, DateTime dateOfLoaning, DateTime? endDateOfLoaning = null, bool? isLost = null)
            : base()
        {
            this.Id = string.Empty;
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.DateOfLoaning = dateOfLoaning;
            this.EndDateOfLoaning = endDateOfLoaning;
            this.IsLost = isLost;
        }

        public string Id { get; set; }
        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public DateTime DateOfLoaning { get; set; }
        public DateTime? EndDateOfLoaning { get; set; }
        public bool? IsLost { get; set; }
    }
}
