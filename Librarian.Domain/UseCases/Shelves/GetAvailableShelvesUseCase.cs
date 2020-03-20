using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class GetAvailableShelvesUseCase : IGetAvailableShelvesUseCase
    {
        public GetAvailableShelvesUseCase(IShelfRepository shelfRepository)
        {
            this.shelfRepository = shelfRepository;
        }

        private readonly IShelfRepository shelfRepository;

        public async Task<bool> Handle(GetAvailableShelvesRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>> outputPort)
        {
            GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Shelf>> response = await this.shelfRepository.GetAvailableShelves(message.Category, message.NumberOfCopies);

            if (response.Success)
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>(response.Data, true));
            else
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>(response.Errors.Select(e => e.Description)));

            return response.Success;
        }
    }
}
