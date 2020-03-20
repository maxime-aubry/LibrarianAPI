using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class CreateAuthorUseCase : ICreateAuthorUseCase
    {
        public CreateAuthorUseCase(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        private readonly IAuthorRepository authorRepository;

        public async Task<bool> Handle(CreateAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.FirstName) &&
                !string.IsNullOrEmpty(message.LastName))
            {
                Librarian.Core.Domain.Entities.Author author = new Librarian.Core.Domain.Entities.Author(message.FirstName, message.LastName);
                GateawayResponse<string> response = await this.authorRepository.Add(author);

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
