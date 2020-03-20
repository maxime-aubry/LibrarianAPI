using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        public GetAuthorByIdUseCase(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        private readonly IAuthorRepository authorRepository;

        public async Task<bool> Handle(GetAuthorByIdRequest message, IOutputPort<UseCaseResponseMessage<Librarian.Core.Domain.Entities.Author>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Id))
            {
                GateawayResponse<Librarian.Core.Domain.Entities.Author> response = await this.authorRepository.Get(message.Id);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Author>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<Librarian.Core.Domain.Entities.Author>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
