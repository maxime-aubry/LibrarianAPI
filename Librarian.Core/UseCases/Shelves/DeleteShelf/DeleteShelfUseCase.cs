using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves.DeleteShelf
{
    public class DeleteShelfUseCase : UseCase, IDeleteShelfUseCase
    {
        public DeleteShelfUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(DeleteShelfRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<Shelf> shelf = await this.repositories.Shelves.Get(message.ShelfId);

                if (!shelf.Success)
                    throw new UseCaseException("Shelf not found", shelf.Errors);

                GateawayResponse<IEnumerable<Book>> books = await this.repositories.Books.Get();

                if (!books.Success)
                    throw new UseCaseException("Books not found", books.Errors);

                IEnumerable<Book> booksOfShelf = (from b in books.Data
                                                    where b.ShelfId == message.ShelfId
                                                    select b);
                if (booksOfShelf.Any())
                    throw new UseCaseException("This shelf is alreader linked to books", null);

                GateawayResponse<string> deletedShelf = await this.repositories.Shelves.Delete(message.ShelfId);

                if (!deletedShelf.Success)
                    throw new UseCaseException("Shelf not deleted", books.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(null, true));
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
