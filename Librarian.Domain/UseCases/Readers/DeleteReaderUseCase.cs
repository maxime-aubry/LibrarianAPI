using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class DeleteReaderUseCase : IDeleteReaderUseCase
    {
        public DeleteReaderUseCase(IReaderRepository readerRepository)
        {
            this.readerRepository = readerRepository;
        }

        private readonly IReaderRepository readerRepository;

        public async Task<bool> Handle(DeleteReaderRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<string> response = await this.readerRepository.Delete(message.Id);

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
