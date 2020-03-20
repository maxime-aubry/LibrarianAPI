using System;

namespace Librarian.Infrastructure.Entities
{
    public class ReaderRatesBook : BaseObject
    {
        public ReaderRatesBook(string id, string readerId, string bookId, float rate, string comment, DateTime dateOfRate)
            : base(id)
        {
            this.Id = id;
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.Rate = rate;
            this.Comment = comment;
            this.DateOfRate = dateOfRate;
        }

        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public float Rate { get; set; }
        public string Comment { get; set; }
        public DateTime DateOfRate { get; set; }
    }
}
