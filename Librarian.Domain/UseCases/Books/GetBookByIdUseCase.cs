using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        public GetBookByIdUseCase(
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

        public async Task<bool> Handle(GetBookByIdRequest message, IOutputPort<UseCaseResponseMessage<Book>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId))
            {
                try
                {
                    GateawayResponse<Book> book = await this.bookRepository.Get(message.BookId);

                    if (!book.Success)
                        throw new UseCaseException("Book not found", book.Errors);

                    outputPort.Handle(new UseCaseResponseMessage<Book>(book.Data, true));
                    return true;
                }
                catch (UseCaseException e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<Book>(null, false, e.Message, e.Errors));
                }
            }

            return false;
        }
    }
}
