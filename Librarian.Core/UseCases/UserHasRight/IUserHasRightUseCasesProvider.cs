using Librarian.Core.UseCases.UserHasRight.AddRight;
using Librarian.Core.UseCases.UserHasRight.DeleteRight;

namespace Librarian.Core.UseCases.UserHasRight
{
    public interface IUserHasRightUseCasesProvider
    {
        IAddRightUseCase AddRight { get; set; }
        IDeleteRightUseCase DeleteRight { get; set; }
    }
}
