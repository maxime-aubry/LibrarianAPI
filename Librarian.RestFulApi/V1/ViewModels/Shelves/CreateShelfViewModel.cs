using Librarian.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Librarian.RestFulAPI.V1.ViewModels.Shelves
{
    public class CreateShelfViewModel
    {
        [Required]
        [Range(1, 600)]
        public int MaxQtyOfBooks { get; set; }

        [Required]
        public EFloor Floor { get; set; }

        [Required]
        public EBookCategory BookCategory { get; set; }
    }
}
