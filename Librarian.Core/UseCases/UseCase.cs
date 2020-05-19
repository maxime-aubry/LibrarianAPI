using Librarian.Core.Repositories;

namespace Librarian.Core.UseCases
{
    public class UseCase
    {
        public UseCase(
            IRepositoryProvider repositories
        )
        {
            this.repositories = repositories;
        }

        protected readonly IRepositoryProvider repositories;
    }
}
