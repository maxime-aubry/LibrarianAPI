using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBooksUseCase : IGetBooksUseCase
    {
        public GetBooksUseCase(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        private readonly IBookRepository bookRepository;

        public async Task<bool> Handle(GetBooksRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>> outputPort)
        {
            GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>> response = await this.bookRepository.Get();

            if (response.Success)
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>(response.Data, true));
            else
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>(response.Errors.Select(e => e.Description)));

            return response.Success;
        }
    }
}
