using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Authors
{
    public class GetAuthorsByFiltersUseCase : IGetAuthorsByFiltersUseCase
    {
        public GetAuthorsByFiltersUseCase(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        private readonly IAuthorRepository authorRepository;

        public async Task<bool> Handle(GetAuthorsByFiltersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.FirstName) &&
                !string.IsNullOrEmpty(message.LastName))
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>> response = await this.authorRepository.GetByFilters(message.FirstName, message.LastName);

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
