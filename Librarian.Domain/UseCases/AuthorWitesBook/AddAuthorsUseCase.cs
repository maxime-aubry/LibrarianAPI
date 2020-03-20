using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWitesBook
{
    public class AddAuthorsUseCase : IAddAuthorsUseCase
    {
        public AddAuthorsUseCase(IAuthorWritesBookRepository authorWritesBookRepository)
        {
            this.authorWritesBookRepository = authorWritesBookRepository;
        }

        private readonly IAuthorWritesBookRepository authorWritesBookRepository;

        public async Task<bool> Handle(AddAuthorsRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<string>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId) &&
                message.AuthorIds != null &&
                message.AuthorIds.Any())
            {
                GateawayResponse<IEnumerable<string>> response = await this.authorWritesBookRepository.AddAuthors(message.BookId, message.AuthorIds);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<string>>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<string>>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
