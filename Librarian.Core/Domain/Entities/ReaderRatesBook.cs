using System;

namespace Librarian.Core.Domain.Entities
{
    public class ReaderRatesBook
    {
        public ReaderRatesBook()
        {

        }

        public ReaderRatesBook(string id, string readerId, string bookId, float rate, string comment, DateTime dateOfRate)
            : base()
        {
            this.Id = id;
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.Rate = rate;
            this.Comment = comment;
            this.DateOfRate = dateOfRate;
        }

        public ReaderRatesBook(string readerId, string bookId, float rate, string comment, DateTime dateOfRate)
            : base()
        {
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.Rate = rate;
            this.Comment = comment;
            this.DateOfRate = dateOfRate;
        }

        public string Id { get; set; }
        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public float Rate { get; set; }
        public string Comment { get; set; }
        public DateTime DateOfRate { get; set; }

    }
}
