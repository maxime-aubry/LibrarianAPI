using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class GetShelfByIdUseCase : IGetShelfByIdUseCase
    {
        public GetShelfByIdUseCase(IShelfRepository shelfRepository)
        {
            this.shelfRepository = shelfRepository;
        }

        private readonly IShelfRepository shelfRepository;

        public async Task<bool> Handle(GetShelfByIdRequest message, IOutputPort<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Shelf>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<Librarian.Core.Domain.Entities.Shelf> response = await this.shelfRepository.Get(message.Id);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Shelf>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Shelf>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
