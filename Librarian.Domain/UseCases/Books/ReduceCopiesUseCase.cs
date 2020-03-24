using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class ReduceCopiesUseCase : IReduceCopiesUseCase
    {
        public ReduceCopiesUseCase(
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

        public async Task<bool> Handle(ReduceCopiesRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId) &&
                message.NumberOfCopies > 0)
            {
                try
                {
                    Book book = await this.bookRepository.Get(message.BookId);

                    if (book == null)
                        throw new Exception("Book not found.");

                    if ((book.NumberOfCopies - message.NumberOfCopies) < 0)
                        throw new Exception("No enougth book to remove.");

                    Shelf shelf = await this.shelfRepository.Get(book.Id);

                    if (shelf == null)
                        throw new Exception("Shelf not found");

                    book.NumberOfCopies -= message.NumberOfCopies;
                    shelf.QtyOfRemainingPlaces += message.NumberOfCopies;

                    string bookId = await this.bookRepository.Update(message.BookId, book);

                    if (string.IsNullOrEmpty(bookId))
                        throw new Exception("Book not saved");

                    string shelfId = await this.shelfRepository.Update(shelf.Id, shelf);

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
