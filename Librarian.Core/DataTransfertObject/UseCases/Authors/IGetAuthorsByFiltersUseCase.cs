using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IGetAuthorsByFiltersUseCase : IUseCaseRequestHandler<GetAuthorsByFiltersRequest, UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>>
    {
    }
}
