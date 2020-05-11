using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class CreateBookUseCase : UseCase, ICreateBookUseCase
    {
        public CreateBookUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CreateBookRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Shelf> shelf = await this.repositories.Shelves.Get(message.ShelfId);

                if (!shelf.Success)
                    throw new UseCaseException("Shelf not found", shelf.Errors);

                if (shelf.Data.QtyOfRemainingPlaces == 0)
                    throw new UseCaseException("Shelf is full", null);

                if (shelf.Data.QtyOfRemainingPlaces < message.NumberOfCopies)
                    throw new UseCaseException("Shelf has no places enougth.", null);

                Book book = new Book(message.Title, message.CategoryIds, message.ReleaseDate, message.NumberOfCopies, message.ShelfId);
                GateawayResponse<string> bookId = await this.repositories.Books.Add(book);

                if (!bookId.Success)
                    throw new UseCaseException("Book not saved", bookId.Errors);

                shelf.Data.QtyOfRemainingPlaces -= message.NumberOfCopies;
                GateawayResponse<string> shelfId = await this.repositories.Shelves.Update(message.ShelfId, shelf.Data);

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
