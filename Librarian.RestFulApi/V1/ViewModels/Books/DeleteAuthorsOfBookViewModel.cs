using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class DeleteAuthorsOfBookViewModel
    {
        [Required]
        [StringLength(24)]
        public string BookId { get; set; }

        [Required]
        public string AuthorId { get; set; }
    }
}
