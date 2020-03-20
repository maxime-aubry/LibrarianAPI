using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class AddAuthorsToBookViewModel
    {
        [Required]
        [StringLength(24)]
        public string BookId { get; set; }

        [Required]
        public List<string> AuthorIds { get; set; }
    }
}
