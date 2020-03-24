using Librarian.Core.DataTransfertObject;
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
            if (!string.IsNullOrEmpty(message.ReaderId) &&
                !string.IsNullOrEmpty(message.BookId) &&
                message.Rate >= 0 &&
                !string.IsNullOrEmpty(message.Comment))
            {
                try
                {
                    IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook> rates = (from rrb in await this.readerRatesBookRepository.Get()
                                                                                         where rrb.ReaderId == message.ReaderId
                                                                                         && rrb.BookId == message.BookId
                                                                                         select rrb);

                    if (rates.Any())
                        throw new Exception("Reader has already rated this book");

                    Librarian.Core.Domain.Entities.ReaderRatesBook rate = new Librarian.Core.Domain.Entities.ReaderRatesBook(message.ReaderId, message.BookId, message.Rate, message.Comment, DateTime.UtcNow);
                    string rateId = await this.readerRatesBookRepository.Add(rate);

                    if (string.IsNullOrEmpty(rateId))
                        throw new Exception("Rate not saved");

                    outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
                    return true;
                }
                catch (Exception e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message));
                }
            }

            return false;
        }
    }
}
