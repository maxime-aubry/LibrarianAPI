using Librarian.Core.Domain.Enums;

namespace Librarian.RestFulAPI.V1.ViewModels.Shelves
{
    public class CreateShelfViewModel
    {
        public int MaxQtyOfBooks { get; set; }
        public EFloor Floor { get; set; }
        public EBookCategory BookCategory { get; set; }
    }
}
