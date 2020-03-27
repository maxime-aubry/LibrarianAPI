using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook
{
    public class GetAuthorsByBookIdUseCase : IGetAuthorsByBookIdUseCase
    {
        public GetAuthorsByBookIdUseCase(
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

        public async Task<bool> Handle(GetAuthorsByBookIdRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Author>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId))
            {
                try
                {
                    GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.authorWritesBookRepository.Get();

                    if (!properties.Success)
                        throw new UseCaseException("Properties not found", properties.Errors);

                    GateawayResponse<IEnumerable<Author>> authors = await this.authorRepository.Get();

                    if (!authors.Success)
                        throw new UseCaseException("Authors not found", authors.Errors);

                    IEnumerable<Author> authorsOfBook = (from awb in properties.Data
                                                           join a in authors.Data on awb.AuthorId equals a.Id
                                                           where awb.BookId == message.BookId
                                                           select a);

                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Author>>(authorsOfBook, true));
                    return true;
                }
                catch (UseCaseException e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Author>>(null, false, e.Message, e.Errors));
                }
            }

            return false;
        }
    }
}
