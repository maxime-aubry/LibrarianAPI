using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class AddCopiesUseCase : IAddCopiesUseCase
    {
        public AddCopiesUseCase(
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

        public async Task<bool> Handle(AddCopiesRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId) &&
                message.NumberOfCopies != 0)
            {
                try
                {
                    GateawayResponse<Book> book = await this.bookRepository.Get(message.BookId);

                    if (!book.Success)
                        throw new UseCaseException("Book not found", book.Errors);

                    GateawayResponse<Shelf> shelf = await this.shelfRepository.Get(message.BookId);

                    if (!shelf.Success)
                        throw new UseCaseException("Shelf not found", shelf.Errors);

                    book.Data.NumberOfCopies += message.NumberOfCopies;
                    shelf.Data.QtyOfRemainingPlaces -= message.NumberOfCopies;

                    GateawayResponse<string> bookId = await this.bookRepository.Update(message.BookId, book.Data);

                    if (!bookId.Success)
                        throw new UseCaseException("Copies of book not added", bookId.Errors);

                    GateawayResponse<string> shelfId = await this.shelfRepository.Update(shelf.Data.Id, shelf.Data);

                    if (!shelfId.Success)
                        throw new UseCaseException("Shelf not saved", shelfId.Errors);

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
