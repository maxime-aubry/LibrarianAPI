using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class UpdateShelfUseCase : IUpdateShelfUseCase
    {
        public UpdateShelfUseCase(IShelfRepository shelfRepository)
        {
            this.shelfRepository = shelfRepository;
        }

        private readonly IShelfRepository shelfRepository;

        public async Task<bool> Handle(UpdateShelfRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id) &&
                message.MaxQtyOfBooks != 0 &&
                message.Floor != EFloor.Default &&
                message.BookCategory != EBookCategory.Default)
            {
                Librarian.Core.Domain.Entities.Shelf reader = new Librarian.Core.Domain.Entities.Shelf(
                    message.Id,
                    string.Empty,
                    message.MaxQtyOfBooks,
                    message.Floor,
                    message.BookCategory
                );
                GateawayResponse<string> response = await this.shelfRepository.Update(message.Id, reader);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<string>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<string>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
