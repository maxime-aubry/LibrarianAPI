
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
    public class GetBooksByAuthorIdUseCase : IGetBooksByAuthorIdUseCase
    {
        public GetBooksByAuthorIdUseCase(
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

        public async Task<bool> Handle(GetBooksByAuthorIdRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Book>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.AuthorId))
            {
                try
                {
                    GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.authorWritesBookRepository.Get();

                    if (!properties.Success)
                        throw new UseCaseException("Properties not found", properties.Errors);

                    GateawayResponse<IEnumerable<Book>> books = await this.bookRepository.Get();

                    if (!books.Success)
                        throw new UseCaseException("Books not found", books.Errors);

                    IEnumerable<Book> booksOfAuthor = (from awb in properties.Data
                                                       join b in books.Data on awb.BookId equals b.Id
                                                        where awb.AuthorId == message.AuthorId
                                                        select b);

                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(booksOfAuthor, true));
                    return true;
                }
                catch (UseCaseException e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(null, false, e.Message, e.Errors));
                }
            }

            return false;
        }
    }
}
