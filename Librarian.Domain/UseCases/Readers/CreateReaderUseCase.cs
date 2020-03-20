using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Readers
{
    public class CreateReaderUseCase : ICreateReaderUseCase
    {
        public CreateReaderUseCase(IReaderRepository readerRepository)
        {
            this.readerRepository = readerRepository;
        }

        private readonly IReaderRepository readerRepository;

        public async Task<bool> Handle(CreateReaderRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.FirstName) &&
                !string.IsNullOrEmpty(message.LastName) &&
                message.Birthday != null &&
                message.Birthday != DateTime.MinValue)
            {
                Librarian.Core.Domain.Entities.Reader reader = new Librarian.Core.Domain.Entities.Reader(
                    message.FirstName,
                    message.LastName,
                    message.Birthday,
                    message.IsForbidden
                );
                GateawayResponse<string> response = await this.readerRepository.Add(reader);

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
