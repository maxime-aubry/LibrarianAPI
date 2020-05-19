using Librarian.Core.UseCases.UserHasRight.AddRight;
using Librarian.Core.UseCases.UserHasRight.DeleteRight;

namespace Librarian.Core.UseCases.UserHasRight
{
    public class UserHasRightUseCasesProvider : IUserHasRightUseCasesProvider
    {
        public UserHasRightUseCasesProvider(
            IAddRightUseCase addRight,
            IDeleteRightUseCase deleteRight
        )
        {
            this.AddRight = addRight;
            this.DeleteRight = deleteRight;
        }

        public IAddRightUseCase AddRight { get; set; }
        public IDeleteRightUseCase DeleteRight { get; set; }
    }
}
