using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class GetAvailableShelvesUseCase : UseCase, IGetAvailableShelvesUseCase
    {
        public GetAvailableShelvesUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetAvailableShelvesRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Shelf>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Shelf>> shelves = await this.repositories.Shelves.Get();

                if (!shelves.Success)
                    throw new UseCaseException("Shelves not found", shelves.Errors);

                IEnumerable<Shelf> availableShelves = (from s in shelves.Data
                                                        where s.BookCategory == message.Category
                                                        && s.QtyOfRemainingPlaces > 0
                                                        //&& (s.QtyOfRemainingPlaces + message.NumberOfCopies) <= s.MaxQtyOfBooks
                                                        select s);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Shelf>>(availableShelves, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Shelf>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
