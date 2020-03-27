using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class UpdateBookViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<EBookCategory> Categories { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ShelfId { get; set; }
    }
}
