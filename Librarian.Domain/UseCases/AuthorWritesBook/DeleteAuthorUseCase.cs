using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook
{
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        public DeleteAuthorUseCase(
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

        public async Task<bool> Handle(DeleteAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
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
                    if (property == null)
                        throw new UseCaseException("Author is not linked to this book", null);

                    GateawayResponse<string> deletedProperty = await this.authorWritesBookRepository.Delete(property.Id);

                    if (!deletedProperty.Success)
                        throw new UseCaseException("Book not unlinked to this book", deletedProperty.Errors);

                    outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
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
