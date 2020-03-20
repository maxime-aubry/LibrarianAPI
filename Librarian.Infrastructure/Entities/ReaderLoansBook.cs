using System;

namespace Librarian.Infrastructure.Entities
{
    public class ReaderLoansBook : BaseObject
    {
        public ReaderLoansBook(string id, string readerId, string bookId, DateTime dateOfLoaning, DateTime? endDateOfLoaning, bool? isLost)
            : base(id)
        {
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.DateOfLoaning = dateOfLoaning;
            this.EndDateOfLoaning = endDateOfLoaning;
            this.IsLost = isLost;
        }

        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public DateTime DateOfLoaning { get; set; }
        public DateTime? EndDateOfLoaning { get; set; }
        public bool? IsLost { get; set; }
    }
}
