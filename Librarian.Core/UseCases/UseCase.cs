using Librarian.Core.DataTransfertObject.GatewayResponses;

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
