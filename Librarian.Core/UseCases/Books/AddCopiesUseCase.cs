using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class AddCopiesUseCase : UseCase, IAddCopiesUseCase
    {
        public AddCopiesUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(AddCopiesRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Book> book = await this.repositories.Books.Get(message.BookId);

                if (!book.Success)
                    throw new UseCaseException("Book not found", book.Errors);

                GateawayResponse<Shelf> shelf = await this.repositories.Shelves.Get(message.BookId);

                if (!shelf.Success)
                    throw new UseCaseException("Shelf not found", shelf.Errors);

                book.Data.NumberOfCopies += message.NumberOfCopies;
                shelf.Data.QtyOfRemainingPlaces -= message.NumberOfCopies;

                GateawayResponse<string> bookId = await this.repositories.Books.Update(message.BookId, book.Data);

                if (!bookId.Success)
                    throw new UseCaseException("Copies of book not added", bookId.Errors);

                GateawayResponse<string> shelfId = await this.repositories.Shelves.Update(shelf.Data.Id, shelf.Data);

                if (!shelfId.Success)
                    throw new UseCaseException("Shelf not saved", shelfId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(bookId.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
