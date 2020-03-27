using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderRatesBook
{
    public class AddRateUseCase : IAddRateUseCase
    {
        public AddRateUseCase(
            IAuthorRepository authorRepository,
            IAuthorWritesBookRepository authorWritesBookRepository,
            IBookRepository bookRepository,
            IReaderLoansBookRepository readerLoansBookRepository,
            IReaderRatesBookRepository readerRatesBookRepository,
            IReaderRepository readerRepository,
            IShelfRepository shelfRepository
        )
        {
            this.authorRepository = authorRepository;
            this.authorWritesBookRepository = authorWritesBookRepository;
            this.bookRepository = bookRepository;
            this.readerLoansBookRepository = readerLoansBookRepository;
            this.readerRatesBookRepository = readerRatesBookRepository;
            this.readerRepository = readerRepository;
            this.shelfRepository = shelfRepository;
        }

        private readonly IAuthorRepository authorRepository;
        private readonly IAuthorWritesBookRepository authorWritesBookRepository;
        private readonly IBookRepository bookRepository;
        private readonly IReaderLoansBookRepository readerLoansBookRepository;
        private readonly IReaderRatesBookRepository readerRatesBookRepository;
        private readonly IReaderRepository readerRepository;
        private readonly IShelfRepository shelfRepository;

        public async Task<bool> Handle(AddRateRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>> rates = await this.readerRatesBookRepository.Get();

                if (!rates.Success)
                    throw new UseCaseException("Rates not found", rates.Errors);

                IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook> bookOfRate = (from rrb in rates.Data
                                                                                          where rrb.ReaderId == message.ReaderId
                                                                                          && rrb.BookId == message.BookId
                                                                                          select rrb);

                if (bookOfRate.Any())
                    throw new UseCaseException("Reader has already rated this book", null);

                Librarian.Core.Domain.Entities.ReaderRatesBook rate = new Librarian.Core.Domain.Entities.ReaderRatesBook(message.ReaderId, message.BookId, message.Rate, message.Comment, DateTime.UtcNow);
                GateawayResponse<string> rateId = await this.readerRatesBookRepository.Add(rate);

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
