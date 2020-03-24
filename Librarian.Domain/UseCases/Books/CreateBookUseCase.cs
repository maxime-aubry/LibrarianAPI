using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System;
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
                    Shelf shelf = await this.shelfRepository.Get(message.ShelfId);

                    if (shelf == null)
                        throw new Exception("Shelf not found");

                    if (shelf.QtyOfRemainingPlaces == 0)
                        throw new Exception("Shelf is full");

                    if (shelf.QtyOfRemainingPlaces < message.NumberOfCopies)
                        throw new Exception("Shelf has no places enougth.");

                    Book book = new Book(message.Title, message.CategoryIds, message.ReleaseDate, message.NumberOfCopies, message.ShelfId);
                    string bookId = await this.bookRepository.Add(book);

                    if (string.IsNullOrEmpty(bookId))
                        throw new Exception("Book not saved");

                    shelf.QtyOfRemainingPlaces -= message.NumberOfCopies;
                    string shelfId = await this.shelfRepository.Update(message.ShelfId, shelf);

                    if (string.IsNullOrEmpty(shelfId))
                        throw new Exception("Shelf not saved");

                    outputPort.Handle(new UseCaseResponseMessage<string>(bookId, true));
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
