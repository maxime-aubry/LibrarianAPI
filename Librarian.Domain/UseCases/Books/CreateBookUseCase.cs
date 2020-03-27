using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class CreateBookUseCase : ICreateBookUseCase
    {
        public CreateBookUseCase(
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

        public async Task<bool> Handle(CreateBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Title) &&
                message.CategoryIds != null &&
                message.CategoryIds.Any() &&
                !string.IsNullOrEmpty(message.ShelfId))
            {
                try
                {
                    GateawayResponse<Shelf> shelf = await this.shelfRepository.Get(message.ShelfId);

                    if (!shelf.Success)
                        throw new UseCaseException("Shelf not found", shelf.Errors);

                    if (shelf.Data.QtyOfRemainingPlaces == 0)
                        throw new UseCaseException("Shelf is full", null);

                    if (shelf.Data.QtyOfRemainingPlaces < message.NumberOfCopies)
                        throw new UseCaseException("Shelf has no places enougth.", null);

                    Book book = new Book(message.Title, message.CategoryIds, message.ReleaseDate, message.NumberOfCopies, message.ShelfId);
                    GateawayResponse<string> bookId = await this.bookRepository.Add(book);

                    if (!bookId.Success)
                        throw new UseCaseException("Book not saved", bookId.Errors);

                    shelf.Data.QtyOfRemainingPlaces -= message.NumberOfCopies;
                    GateawayResponse<string> shelfId = await this.shelfRepository.Update(message.ShelfId, shelf.Data);

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
