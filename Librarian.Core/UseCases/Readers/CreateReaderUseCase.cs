using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class CreateReaderUseCase : UseCase, ICreateReaderUseCase
    {
        public CreateReaderUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CreateReaderRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                Reader reader = new Reader(message.FirstName, message.LastName, message.Birthday, false);
                GateawayResponse<string> readerId = await this.repositories.Reader.Add(reader);

                if (!readerId.Success)
                    throw new UseCaseException("Reader not saved", readerId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(readerId.Data, true));
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
