using Librarian.Core.UseCases.Shelves.CreateShelf;
using Librarian.Core.UseCases.Shelves.DeleteShelf;
using Librarian.Core.UseCases.Shelves.GetAvailableShelves;
using Librarian.Core.UseCases.Shelves.GetShelfById;
using Librarian.Core.UseCases.Shelves.GetShelves;

namespace Librarian.Core.UseCases.Shelves
{
    public class ShelvesUseCasesProvider : IShelvesUseCasesProvider
    {
        public ShelvesUseCasesProvider(
            IGetShelfByIdUseCase getShelfById,
            IGetShelvesUseCase getShelves,
            ICreateShelfUseCase createShelf,
            IDeleteShelfUseCase deleteShelf,
            IGetAvailableShelvesUseCase getAvailableShelves
        )
        {
            this.GetShelfById = getShelfById;
            this.GetShelves = getShelves;
            this.CreateShelf = createShelf;
            this.DeleteShelf = deleteShelf;
            this.GetAvailableShelves = getAvailableShelves;
        }

        public IGetShelfByIdUseCase GetShelfById { get; set; }

        public IGetShelvesUseCase GetShelves { get; set; }

        public ICreateShelfUseCase CreateShelf { get; set; }

        public IDeleteShelfUseCase DeleteShelf { get; set; }
        public IGetAvailableShelvesUseCase GetAvailableShelves { get; set; }
    }
}
