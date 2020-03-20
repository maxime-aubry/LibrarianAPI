using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class CreateRateBookViewModel
    {
        [Required]
        [StringLength(24)]
        public string ReaderId { get; set; }

        [Required]
        [StringLength(24)]
        public string BookId { get; set; }

        [Required]
        [Range(0, 5)]
        public float Rate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Commment { get; set; }
    }
}
