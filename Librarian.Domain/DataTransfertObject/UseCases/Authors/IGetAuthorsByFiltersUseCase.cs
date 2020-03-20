using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IGetAuthorsByFiltersUseCase : IUseCaseRequestHandler<GetAuthorsByFiltersRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindAuthorsByFilters>>>
    {
    }
}
