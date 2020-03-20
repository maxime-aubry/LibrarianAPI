using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderRatesBook
{
    public class AddRateUseCase : IAddRateUseCase
    {
        public AddRateUseCase(IReaderRatesBookRepository readerRatesBookRepository)
        {
            this.readerRatesBookRepository = readerRatesBookRepository;
        }

        private readonly IReaderRatesBookRepository readerRatesBookRepository;

        public async Task<bool> Handle(AddRateRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.ReaderId) &&
                !string.IsNullOrEmpty(message.BookId) &&
                message.Rate >= 0 &&
                !string.IsNullOrEmpty(message.Comment))
            {
                Librarian.Core.Domain.Entities.ReaderRatesBook item = new Librarian.Core.Domain.Entities.ReaderRatesBook(message.ReaderId, message.BookId, message.Rate, message.Comment, DateTime.Now);
                GateawayResponse<string> response = await this.readerRatesBookRepository.Add(item);

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
