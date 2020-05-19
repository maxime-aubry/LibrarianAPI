using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Users.GetUserById
{
    public class GetUserByIdUseCase : UseCase, IGetUserByIdUseCase
    {
        public GetUserByIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetUserByIdRequest message, IOutputPort<UseCaseResponseMessage<User>> outputPort)
        {
            try
            {
                GateawayResponse<User> user = await this.repositories.Users.Get(message.UserId);

                if (!user.Success)
                    throw new UseCaseException("User not found", user.Errors);

                outputPort.Handle(new UseCaseResponseMessage<User>(user.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<User>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
