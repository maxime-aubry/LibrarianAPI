using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook
{
    public class AddAuthorUseCase : IAddAuthorUseCase
    {
        public AddAuthorUseCase(
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

        public async Task<bool> Handle(AddAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId) &&
                !string.IsNullOrEmpty(message.AuthorId))
            {
                try
                {
                    // check if book exists
                    Book book = await this.bookRepository.Get(message.BookId);

                    if (book == null)
                        throw new Exception("Book not found");

                    // check if author exists
                    Author author = await this.authorRepository.Get(message.AuthorId);

                    if (author == null)
                        throw new Exception("Author not found");

                    IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook> authorsOfBook = (from awb in await this.authorWritesBookRepository.Get()
                                                                                            where awb.BookId == message.BookId
                                                                                            && awb.AuthorId == message.AuthorId
                                                                                            select awb);
                    if (authorsOfBook.Any())
                        throw new Exception("Author is already linked to this book");

                    // add author to this book
                    Librarian.Core.Domain.Entities.AuthorWritesBook authorWritesBook = new Librarian.Core.Domain.Entities.AuthorWritesBook(message.AuthorId, message.BookId);
                    string authorWritesBookId = await this.authorWritesBookRepository.Add(authorWritesBook);

                    if (string.IsNullOrEmpty(authorWritesBookId))
                        throw new Exception("Author not linked to this book");

                    outputPort.Handle(new UseCaseResponseMessage<string>(authorWritesBookId, true));
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
