using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class CreateBookRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateBookRequest(string title, IEnumerable<EBookCategory> categoryIds, DateTime releaseDate, int numberOfCopies, string shelfId)
        {
            this.Title = title;
            this.CategoryIds = categoryIds;
            this.ReleaseDate = releaseDate;
            this.NumberOfCopies = numberOfCopies;
            this.ShelfId = shelfId;
        }

        public string Title { get; set; }
        public IEnumerable<EBookCategory> CategoryIds { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfCopies { get; set; }
        public string ShelfId { get; set; }
    }
}
