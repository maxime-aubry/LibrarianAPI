using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWitesBook
{
    public class GetBooksByAuthorIdUseCase : IGetBooksByAuthorIdUseCase
    {
        public GetBooksByAuthorIdUseCase(IAuthorWritesBookRepository authorWritesBookRepository)
        {
            this.authorWritesBookRepository = authorWritesBookRepository;
        }

        private readonly IAuthorWritesBookRepository authorWritesBookRepository;

        public async Task<bool> Handle(GetBooksByAuthorIdRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.AuthorId))
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Book>> response = await this.authorWritesBookRepository.GetBooks(message.AuthorId);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
