using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class GetAuthorsUseCase : IGetAuthorsUseCase
    {
        public GetAuthorsUseCase(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        private readonly IAuthorRepository authorRepository;

        public async Task<bool> Handle(GetAuthorsRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>> outputPort)
        {
            GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.Author>> response = await this.authorRepository.Get();

            if (response.Success)
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>(response.Data, true));
            else
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>(response.Errors.Select(e => e.Description)));

            return response.Success;
        }
    }
}
