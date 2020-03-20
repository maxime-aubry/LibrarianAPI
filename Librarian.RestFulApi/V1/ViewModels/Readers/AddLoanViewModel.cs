using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Readers
{
    public class AddLoanViewModel
    {
        [Required]
        [StringLength(24)]
        public string ReaderId { get; set; }

        [Required]
        [StringLength(24)]
        public string BookId { get; set; }
    }
}
