using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class ReduceCopiesViewModel
    {
        [Required]
        [StringLength(24)]
        public string BookId { get; set; }

        [Required]
        public int NumberOfCopies { get; set; }
    }
}
