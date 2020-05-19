using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Helpers;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves.CreateShelf
{
    public class CreateShelfUseCase : UseCase, ICreateShelfUseCase
    {
        public CreateShelfUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(CreateShelfRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Shelf>> shelves = await this.repositories.Shelves.Get();

                if (!shelves.Success)
                    throw new UseCaseException("Shelves not found", shelves.Errors);

                int nbShelves = (from s in shelves.Data
                                    where s.Floor == message.Floor
                                    && s.BookCategory == message.BookCategory
                                    select s).Count();

                Shelf shelf = new Shelf(ShelfHelper.GetShelfName(message.Floor, message.BookCategory, nbShelves + 1), message.MaxQtyOfBooks, message.MaxQtyOfBooks, message.Floor, message.BookCategory);
                GateawayResponse<string> shelfId = await this.repositories.Shelves.Add(shelf);

                if (!shelfId.Success)
                    throw new UseCaseException("Shelf not saved", shelfId.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(shelfId.Data, true));
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
