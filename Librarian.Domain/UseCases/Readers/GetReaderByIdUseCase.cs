using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class GetReaderByIdUseCase : IGetReaderByIdUseCase
    {
        public GetReaderByIdUseCase(IReaderRepository readerRepository)
        {
            this.readerRepository = readerRepository;
        }

        private readonly IReaderRepository readerRepository;

        public async Task<bool> Handle(GetReaderByIdRequest message, IOutputPort<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Reader>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<Librarian.Core.Domain.Entities.Reader> response = await this.readerRepository.Get(message.Id);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Reader>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Reader>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
