using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
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
                    GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.authorWritesBookRepository.Get();

                    if (!properties.Success)
                        throw new UseCaseException("Properties not found", properties.Errors);

                    // delete author from this book
                    Librarian.Core.Domain.Entities.AuthorWritesBook property = (from awb in properties.Data
                                                                                where awb.BookId == message.BookId
                                                                                && awb.AuthorId == message.AuthorId
                                                                                select awb).SingleOrDefault();
                    if (property != null)
                        throw new UseCaseException("Author is already linked to this book", null);

                    // link author to this book
                    Librarian.Core.Domain.Entities.AuthorWritesBook authorWritesBook = new Librarian.Core.Domain.Entities.AuthorWritesBook(message.AuthorId, message.BookId);
                    GateawayResponse<string> addedProperty = await this.authorWritesBookRepository.Add(authorWritesBook);

                    if (!addedProperty.Success)
                        throw new UseCaseException("Book not linked to this book", addedProperty.Errors);

                    outputPort.Handle(new UseCaseResponseMessage<string>(addedProperty.Data, true));
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
