using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        public GetBookByIdUseCase(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        private readonly IBookRepository bookRepository;

        public async Task<bool> Handle(GetBookByIdRequest message, IOutputPort<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Book>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<Librarian.Core.Domain.Entities.Book> response = await this.bookRepository.Get(message.Id);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Book>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Book>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
