using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Readers
{
    public class CloseLoanViewModel
    {
        [Required]
        [StringLength(24)]
        public string LoanId { get; set; }
    }
}
