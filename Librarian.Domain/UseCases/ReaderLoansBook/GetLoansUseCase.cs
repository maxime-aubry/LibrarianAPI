using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.ReaderLoansBook
{
    public class GetLoansUseCase : IGetLoansUseCase
    {
        public GetLoansUseCase(
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

        public async Task<bool> Handle(GetLoansRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.ReaderId))
            {
                try
                {
                    IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook> loans = (from rlb in await this.readerLoansBookRepository.Get()
                                                                                         where rlb.ReaderId == message.ReaderId
                                                                                         select rlb);

                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>(loans, true));
                    return true;
                }
                catch (Exception e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderLoansBook>>(null, false, e.Message));
                }
            }

            return false;
        }
    }
}
