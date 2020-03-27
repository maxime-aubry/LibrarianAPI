using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        public DeleteBookUseCase(
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

        public async Task<bool> Handle(DeleteBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId))
            {
                try
                {
                    GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.authorWritesBookRepository.Get();

                    if (!properties.Success)
                        throw new UseCaseException("Properties not found", properties.Errors);

                    IEnumerable<string> awbIds = (from awb in properties.Data
                                                  where awb.BookId == message.BookId
                                                  select awb.Id);

                    GateawayResponse<string> deletedProperties = await this.authorWritesBookRepository.DeleteMany(awbIds);

                    if (!deletedProperties.Success)
                        throw new UseCaseException("Books not unlinked from authors", deletedProperties.Errors);

                    GateawayResponse<string> deletedBook = await this.bookRepository.Delete(message.BookId);

                    if (!deletedBook.Success)
                        throw new UseCaseException("Book not deleted", deletedBook.Errors);

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
