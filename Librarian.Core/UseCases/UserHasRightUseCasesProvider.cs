using Librarian.Core.DataTransfertObject.UseCases.UserHasRight;

namespace Librarian.Core.UseCases
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
