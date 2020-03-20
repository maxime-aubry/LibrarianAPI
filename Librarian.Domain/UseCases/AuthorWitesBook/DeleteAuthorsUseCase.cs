using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWitesBook
{
    public class DeleteAuthorsUseCase : IDeleteAuthorsUseCase
    {
        public DeleteAuthorsUseCase(IAuthorWritesBookRepository authorWritesBookRepository)
        {
            this.authorWritesBookRepository = authorWritesBookRepository;
        }

        private readonly IAuthorWritesBookRepository authorWritesBookRepository;

        public async Task<bool> Handle(DeleteAuthorsRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.BookId) &&
                message.AuthorIds != null &&
                message.AuthorIds.Any())
            {
                GateawayResponse<string> response = await this.authorWritesBookRepository.DeleteAuthors(message.BookId, message.AuthorIds);

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
