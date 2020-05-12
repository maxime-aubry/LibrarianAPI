using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class GetReaderByIdUseCase : UseCase, IGetReaderByIdUseCase
    {
        public GetReaderByIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetReaderByIdRequest message, IOutputPort<UseCaseResponseMessage<Reader>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.ReaderId))
            {
                try
                {
                    GateawayResponse<Reader> reader = await this.repositories.Reader.Get(message.ReaderId);

                    if (!reader.Success)
                        throw new UseCaseException("Reader not found", reader.Errors);

                    outputPort.Handle(new UseCaseResponseMessage<Reader>(reader.Data, true));
                    return true;
                }
                catch (UseCaseException e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<Reader>(null, false, e.Message, e.Errors));
                }
            }

            return false;
        }
    }
}
