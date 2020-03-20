using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class GetReadersUseCase : IGetReadersUseCase
    {
        public GetReadersUseCase(IReaderRepository readerRepository)
        {
            this.readerRepository = readerRepository;
        }

        private readonly IReaderRepository readerRepository;

        public async Task<bool> Handle(GetReadersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Reader>>> outputPort)
        {
            GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Reader>> response = await this.readerRepository.Get();

            if (response.Success)
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Reader>>(response.Data, true));
            else
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Reader>>(response.Errors.Select(e => e.Description)));

            return response.Success;
        }
    }
}
