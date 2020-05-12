using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.UserHasRight;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.UserHasRight
{
    public class DeleteRightUseCase : UseCase, IDeleteRightUseCase
    {
        public DeleteRightUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteRightRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.UserHasRight>> rights = await this.repositories.UserHasRight.Get();

                if (!rights.Success)
                    throw new UseCaseException("Rights not found", rights.Errors);

                Librarian.Core.Domain.Entities.UserHasRight userRight = (from ur in rights.Data
                                                                            where ur.UserId == message.UserId
                                                                            && ur.UserRight == message.UserRight
                                                                            select ur).SingleOrDefault();
                if (userRight == null)
                    throw new UseCaseException("User right is not provided to this user", null);

                GateawayResponse<string> deletedUserRight = await this.repositories.UserHasRight.Delete(userRight.Id);

                if (!deletedUserRight.Success)
                    throw new UseCaseException("User right not provided to this user", deletedUserRight.Errors);

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
