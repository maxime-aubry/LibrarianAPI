using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Authors
{
    public class CreateAuthorViewModel
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
