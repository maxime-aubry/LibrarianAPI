using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        public UpdateBookUseCase(
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

        public async Task<bool> Handle(UpdateBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId) &&
                !string.IsNullOrEmpty(message.Title) &&
                message.CategoryIds != null &&
                message.CategoryIds.Any() &&
                !string.IsNullOrEmpty(message.ShelfId))
            {
                try
                {
                    GateawayResponse<Book> existingBook = await this.bookRepository.Get(message.BookId);

                    if (!existingBook.Success)
                        throw new UseCaseException("Book not found", existingBook.Errors);

                    Book book = new Book(message.BookId, message.Title, message.CategoryIds, message.ReleaseDate, existingBook.Data.NumberOfCopies, message.ShelfId);
                    GateawayResponse<string> bookId = await this.bookRepository.Update(message.BookId, book);

                    if (!bookId.Success)
                        throw new UseCaseException("Book not saved", bookId.Errors);

                    outputPort.Handle(new UseCaseResponseMessage<string>(bookId.Data, true));
                    return true;
                }
                catch (UseCaseException e)
                {
                    outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message, e.Errors));
                }
            }

            return false;
        }
    }
}
