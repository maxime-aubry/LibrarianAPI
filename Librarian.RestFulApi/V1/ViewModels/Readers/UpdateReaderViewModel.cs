using System;
using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Readers
{
    public class UpdateReaderViewModel
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

        [Required]
        public DateTime Birthday { get; set; }
    }
}
