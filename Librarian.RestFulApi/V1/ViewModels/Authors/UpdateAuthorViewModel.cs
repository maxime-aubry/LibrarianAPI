using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Authors
{
    public class UpdateAuthorViewModel
    {
        [Required]
        [StringLength(24)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
