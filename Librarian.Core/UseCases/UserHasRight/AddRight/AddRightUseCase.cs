using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.UserHasRight.AddRight
{
    public class AddRightUseCase : UseCase, IAddRightUseCase
    {
        public AddRightUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(AddRightRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.UserHasRight>> rights = await this.repositories.UserHasRight.Get();

                if (!rights.Success)
                    throw new UseCaseException("Rights not found", rights.Errors);

                IEnumerable<Librarian.Core.Domain.Entities.UserHasRight> userRights = (from r in rights.Data
                                                                                          where r.UserId == message.UserId
                                                                                          && r.UserRight == message.UserRight
                                                                                          select r);

                if (userRights.Any())
                    throw new UseCaseException("Reader has already this right", null);

                Librarian.Core.Domain.Entities.UserHasRight right = new Librarian.Core.Domain.Entities.UserHasRight(message.UserId, message.UserRight, DateTime.UtcNow, null);
                GateawayResponse<string> rightId = await this.repositories.UserHasRight.Add(right);

                if (!rightId.Success)
                    throw new UseCaseException("User right not saved", rightId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(rightId.Data, true));
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
