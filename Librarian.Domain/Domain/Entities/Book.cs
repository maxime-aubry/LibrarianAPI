using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.Core.Domain.Entities
{
    public class Book
    {
        public Book()
        {

        }

        public Book(string id, string title, IEnumerable<EBookCategory> categories, DateTime realeaseDate, int numberOfCopies, string shelfId)
            : base()
        {
            this.Id = id;
            this.Title = title;
            this.Categories = categories;
            this.RealeaseDate = realeaseDate;
            this.NumberOfCopies = numberOfCopies;
            this.ShelfId = shelfId;
        }

        public Book(string title, IEnumerable<EBookCategory> categories, DateTime realeaseDate, int numberOfCopies, string shelfId)
            : base()
        {
            this.Id = string.Empty;
            this.Title = title;
            this.Categories = categories;
            this.RealeaseDate = realeaseDate;
            this.NumberOfCopies = numberOfCopies;
            this.ShelfId = shelfId;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<EBookCategory> Categories { get; set; }
        public DateTime RealeaseDate { get; set; }
        public int NumberOfCopies { get; set; }
        public IEnumerable<AuthorOfBook> Authors { get; set; }
        public string ShelfId { get; set; }
    }
}
