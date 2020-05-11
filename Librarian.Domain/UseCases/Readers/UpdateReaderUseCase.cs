using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using Librarian.Core.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class UpdateReaderUseCase : UseCase, IUpdateReaderUseCase
    {
        public UpdateReaderUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(UpdateReaderRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                Reader reader = new Reader(message.ReaderId, message.FirstName, message.LastName, message.Birthday, message.IsForbidden);
                GateawayResponse<string> readerId = await this.repositories.Reader.Update(message.ReaderId, reader);

                if (!readerId.Success)
                    throw new Exception("Reader not found");

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
