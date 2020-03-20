using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBooksByFiltersUseCase : IGetBooksByFiltersUseCase
    {
        public GetBooksByFiltersUseCase(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        private readonly IBookRepository bookRepository;

        public async Task<bool> Handle(GetBooksByFiltersRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>> outputPort)
        {
            if (!string.IsNullOrEmpty(message.Title) &&
                message.Categories != null &&
                message.Categories.Any() &&
                message.AuthorIds != null &&
                message.AuthorIds.Any())
            {

                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>> response = await this.bookRepository.GetByFilters(
                    message.Title,
                    message.Categories.Select(c => (int)c),
                    message.AuthorIds
                );

                if (response.Success)
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>(response.Data, true));
                else
                    outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>(response.Errors.Select(e => e.Description)));

                return response.Success;
            }

            return false;
        }
    }
}
