using Librarian.Core.DataTransfertObject.UseCases.Shelves;

namespace Librarian.Core.UseCases
{
    public class ShelvesUseCasesProvider : IShelvesUseCasesProvider
    {
        public ShelvesUseCasesProvider(
            IGetShelfByIdUseCase getShelfById,
            IGetShelvesUseCase getShelves,
            ICreateShelfUseCase createShelf,
            IUpdateShelfUseCase updateShelf,
            IDeleteShelfUseCase deleteShelf,
            IGetAvailableShelvesUseCase getAvailableShelves
        )
        {
            this.GetShelfById = getShelfById;
            this.GetShelves = getShelves;
            this.CreateShelf = createShelf;
            this.UpdateShelf = updateShelf;
            this.DeleteShelf = deleteShelf;
            this.GetAvailableShelves = getAvailableShelves;
        }

        public IGetShelfByIdUseCase GetShelfById { get; set; }

        public IGetShelvesUseCase GetShelves { get; set; }

        public ICreateShelfUseCase CreateShelf { get; set; }

        public IUpdateShelfUseCase UpdateShelf { get; set; }

        public IDeleteShelfUseCase DeleteShelf { get; set; }
        public IGetAvailableShelvesUseCase GetAvailableShelves { get; set; }
    }
}
