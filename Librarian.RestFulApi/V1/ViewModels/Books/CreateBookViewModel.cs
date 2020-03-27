using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class CreateBookViewModel
    {
        public string Title { get; set; }
        public IEnumerable<EBookCategory> Categories { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfCopies { get; set; }
        public string ShelfId { get; set; }
    }
}
