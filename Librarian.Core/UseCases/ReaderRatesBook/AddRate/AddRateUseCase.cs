using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderRatesBook.AddRate
{
    public class AddRateUseCase : UseCase, IAddRateUseCase
    {
        public AddRateUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(AddRateRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>> rates = await this.repositories.ReaderRatesBook.Get();

                if (!rates.Success)
                    throw new UseCaseException("Rates not found", rates.Errors);

                IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook> bookOfRate = (from rrb in rates.Data
                                                                                          where rrb.ReaderId == message.ReaderId
                                                                                          && rrb.BookId == message.BookId
                                                                                          select rrb);

                if (bookOfRate.Any())
                    throw new UseCaseException("Reader has already rated this book", null);

                Librarian.Core.Domain.Entities.ReaderRatesBook rate = new Librarian.Core.Domain.Entities.ReaderRatesBook(message.ReaderId, message.BookId, message.Rate, message.Comment, DateTime.UtcNow);
                GateawayResponse<string> rateId = await this.repositories.ReaderRatesBook.Add(rate);

                if (!rateId.Success)
                    throw new UseCaseException("Rate not saved", rateId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(rateId.Data, true));
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
