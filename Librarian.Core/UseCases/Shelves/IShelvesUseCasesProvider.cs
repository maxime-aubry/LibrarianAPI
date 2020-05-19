using Librarian.Core.UseCases.Shelves.CreateShelf;
using Librarian.Core.UseCases.Shelves.DeleteShelf;
using Librarian.Core.UseCases.Shelves.GetAvailableShelves;
using Librarian.Core.UseCases.Shelves.GetShelfById;
using Librarian.Core.UseCases.Shelves.GetShelves;

namespace Librarian.Core.UseCases.Shelves
{
    public interface IShelvesUseCasesProvider
    {
        IGetShelfByIdUseCase GetShelfById { get; set; }
        IGetShelvesUseCase GetShelves { get; set; }
        ICreateShelfUseCase CreateShelf { get; set; }
        IDeleteShelfUseCase DeleteShelf { get; set; }
        IGetAvailableShelvesUseCase GetAvailableShelves { get; set; }
    }
}
