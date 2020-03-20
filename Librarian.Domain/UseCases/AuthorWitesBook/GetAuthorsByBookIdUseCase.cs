using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWitesBook
{
    public class GetAuthorsByBookIdUseCase : IGetAuthorsByBookIdUseCase
    {
        public GetAuthorsByBookIdUseCase(IAuthorWritesBookRepository authorWritesBookRepository)
        {
            this.authorWritesBookRepository = authorWritesBookRepository;
        }

        private readonly IAuthorWritesBookRepository authorWritesBookRepository;

        public async Task<bool> Handle(GetAuthorsByBookIdRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId))
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>> response = await this.authorWritesBookRepository.GetAuthors(message.BookId);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
