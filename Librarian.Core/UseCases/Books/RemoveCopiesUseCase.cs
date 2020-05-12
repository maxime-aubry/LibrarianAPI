using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class RemoveCopiesUseCase : UseCase, IRemoveCopiesUseCase
    {
        public RemoveCopiesUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(ReduceCopiesRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Book> book = await this.repositories.Books.Get(message.BookId);

                if (!book.Success)
                    throw new UseCaseException("Book not found", book.Errors);

                if ((book.Data.NumberOfCopies - message.NumberOfCopies) < 0)
                    throw new UseCaseException("No enougth book to remove", null);

                GateawayResponse<Shelf> shelf = await this.repositories.Shelves.Get(book.Data.ShelfId);

                if (!shelf.Success)
                    throw new UseCaseException("Shelf not found", shelf.Errors);

                book.Data.NumberOfCopies -= message.NumberOfCopies;
                shelf.Data.QtyOfRemainingPlaces += message.NumberOfCopies;

                GateawayResponse<string> bookId = await this.repositories.Books.Update(message.BookId, book.Data);

                if (!bookId.Success)
                    throw new UseCaseException("Copies of book not removed", bookId.Errors);

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
