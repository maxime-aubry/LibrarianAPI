using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Users.GetUsers
{
    public class GetUsersUseCase : UseCase, IGetUsersUseCase
    {
        public GetUsersUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetUsersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<User>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<User>> users = await this.repositories.Users.Get();

                if (!users.Success)
                    throw new UseCaseException("Users not found", users.Errors);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<User>>(users.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<User>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
