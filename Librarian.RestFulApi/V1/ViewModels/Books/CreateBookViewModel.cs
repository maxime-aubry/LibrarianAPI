using Librarian.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class CreateBookViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public IEnumerable<EBookCategory> Categories { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int NumberOfCopies { get; set; }

        [Required]
        [StringLength(24)]
        public string ShelfId { get; set; }
    }
}
