using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public class CloseLoanAndDeclareAsLostUseCase : ICloseLoanAndDeclareAsLostUseCase
    {
        public CloseLoanAndDeclareAsLostUseCase(IReaderLoansBookRepository readerLoansBookRepository)
        {
            this.readerLoansBookRepository = readerLoansBookRepository;
        }

        private readonly IReaderLoansBookRepository readerLoansBookRepository;

        public async Task<bool> Handle(CloseLoanAndDeclareAsLostRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<string> response = await this.readerLoansBookRepository.CloseAndDeclareAsLost(message.Id);

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
