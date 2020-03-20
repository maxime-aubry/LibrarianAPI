using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.Core.Domain.Entities
{
    public class FindBooksByFilters
    {
        public FindBooksByFilters()
        {

        }

        public FindBooksByFilters(string id, string title, IEnumerable<EBookCategory> categories, DateTime releaseDate, IEnumerable<AuthorOfBook> authors, float pertinence, int numberOfViews, float rate)
            : base()
        {
            this.Id = id;
            this.Title = title;
            this.Categories = categories;
            this.ReleaseDate = releaseDate;
            this.Authors = authors;
            this.Pertinence = pertinence;
            this.NumberOfViews = numberOfViews;
            this.Rate = rate;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<EBookCategory> Categories { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<AuthorOfBook> Authors { get; set; }
        public float Pertinence { get; set; }
        public int NumberOfViews { get; set; }
        public float Rate { get; set; }
    }

    public class AuthorOfBook
    {
        public AuthorOfBook()
        {

        }

        public AuthorOfBook(string id, string firstName, string lastName)
            : base()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public AuthorOfBook(string firstName, string lastName)
            : base()
        {
            this.Id = string.Empty;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
