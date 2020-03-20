using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class DeleteShelfUseCase : IDeleteShelfUseCase
    {
        public DeleteShelfUseCase(IShelfRepository shelfRepository)
        {
            this.shelfRepository = shelfRepository;
        }

        private readonly IShelfRepository shelfRepository;

        public async Task<bool> Handle(DeleteShelfRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<string> response = await this.shelfRepository.Delete(message.Id);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<string>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<string>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
