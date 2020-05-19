using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Helpers;
using Librarian.Core.Repositories;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Users.CreateUser
{
    public class CreateUserUseCase : UseCase, ICreateUserUseCase
    {
        public CreateUserUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CreateUserRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                User user = new User(message.FirstName, message.LastName, UserHelpers.GetLogin(message.FirstName, message.LastName));
                GateawayResponse<string> userId = await this.repositories.Users.Add(user);

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
