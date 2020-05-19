using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Users.UpdateUser
{
    public class UpdateUserUseCase : UseCase, IUpdateUserUseCase
    {
        public UpdateUserUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(UpdateUserRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<User> user = await this.repositories.Users.Get(message.UserId);
                user.Data.FirstName = message.FirstName;
                user.Data.LastName = message.LastName;
                GateawayResponse<string> userId = await this.repositories.Users.Add(user.Data);

                outputPort.Handle(new UseCaseResponseMessage<string>(userId.Data, true, null, null));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
