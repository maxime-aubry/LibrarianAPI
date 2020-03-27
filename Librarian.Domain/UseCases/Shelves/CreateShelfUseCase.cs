using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class CreateShelfUseCase : ICreateShelfUseCase
    {
        public CreateShelfUseCase(
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

        public async Task<bool> Handle(CreateShelfRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (message.MaxQtyOfBooks != 0 &&
                message.Floor != EFloor.Default &&
                message.BookCategory != EBookCategory.Default)
            {
                try
                {
                    GateawayResponse<IEnumerable<Shelf>> shelves = await this.shelfRepository.Get();

                    if (!shelves.Success)
                        throw new UseCaseException("Shelves not found", shelves.Errors);

                    int nbShelves = (from s in shelves.Data
                                     where s.Floor == message.Floor
                                     && s.BookCategory == message.BookCategory
                                     select s).Count();

                    Shelf shelf = new Shelf($"F{(int)message.Floor}-BC{(int)message.BookCategory}-NB{nbShelves + 1}", message.MaxQtyOfBooks, message.MaxQtyOfBooks, message.Floor, message.BookCategory);
                    GateawayResponse<string> shelfId = await this.shelfRepository.Add(shelf);

                    if (!shelfId.Success)
                        throw new UseCaseException("Shelf not saved", shelfId.Errors);

                    outputPort.Handle(new UseCaseResponseMessage<string>(shelfId.Data, true));
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
