using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserUseCase : UseCase, IDeleteUserUseCase
    {
        public DeleteUserUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteUserRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.UserHasRight>> userRights = await this.repositories.UserHasRight.Get();

                if (!userRights.Success)
                    throw new UseCaseException("User rights not found", userRights.Errors);

                IEnumerable<string> userRightIds = (from ur in userRights.Data
                                                        where ur.UserId == message.UserId
                                                        select ur.Id);

                if (userRightIds.Any())
                {
                    GateawayResponse<string> deletedUserRights = await this.repositories.UserHasRight.DeleteMany(userRightIds);

                    if (!deletedUserRights.Success)
                        throw new UseCaseException("User rights not deleted", deletedUserRights.Errors);
                }

                GateawayResponse<string> deletedUser = await this.repositories.Users.Delete(message.UserId);

                if (!deletedUser.Success)
                    throw new UseCaseException("User not deleted", deletedUser.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
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
