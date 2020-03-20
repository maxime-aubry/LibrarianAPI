using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        public DeleteAuthorUseCase(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        private readonly IAuthorRepository authorRepository;

        public async Task<bool> Handle(DeleteAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<string> response = await this.authorRepository.Delete(message.Id);

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
