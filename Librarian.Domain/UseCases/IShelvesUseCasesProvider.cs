using Librarian.Core.DataTransfertObject.UseCases.Shelves;

namespace Librarian.Core.UseCases
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
