using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class UpdateBookRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateBookRequest(string id, string title, IEnumerable<EBookCategory> categoryIds, DateTime releaseDate, int numberOfCopies, string shelfId)
        {
            this.Id = id;
            this.Title = title;
            this.CategoryIds = categoryIds;
            this.ReleaseDate = releaseDate;
            this.NumberOfCopies = numberOfCopies;
            this.ShelfId = shelfId;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<EBookCategory> CategoryIds { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfCopies { get; set; }
        public string ShelfId { get; set; }
    }
}
