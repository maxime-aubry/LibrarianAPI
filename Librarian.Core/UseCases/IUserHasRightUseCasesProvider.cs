using Librarian.Core.DataTransfertObject.UseCases.UserHasRight;

namespace Librarian.Core.UseCases
{
    public interface IUserHasRightUseCasesProvider
    {
        IAddRightUseCase AddRight { get; set; }
        IDeleteRightUseCase DeleteRight { get; set; }
    }
}
