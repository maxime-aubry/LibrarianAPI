using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public class UpdateBookRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateBookRequest(string bookId, string title, IEnumerable<EBookCategory> categoryIds, DateTime releaseDate, string shelfId)
        {
            this.BookId = bookId;
            this.Title = title;
            this.CategoryIds = categoryIds;
            this.ReleaseDate = releaseDate;
            this.ShelfId = shelfId;
        }

        public string BookId { get; set; }
        public string Title { get; set; }
        public IEnumerable<EBookCategory> CategoryIds { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ShelfId { get; set; }
    }
}
