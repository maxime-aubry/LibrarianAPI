using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class GetShelvesUseCase : UseCase, IGetShelvesUseCase
    {
        public GetShelvesUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetShelvesRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Shelf>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Shelf>> shelves = await this.repositories.Shelves.Get();

                if (!shelves.Success)
                    throw new UseCaseException("Shelves not found", shelves.Errors);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Shelf>>(shelves.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Shelf>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
