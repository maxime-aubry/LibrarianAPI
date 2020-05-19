using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Authors.GetAuthorsByFilters
{
    public interface IGetAuthorsByFiltersUseCase : IUseCaseRequestHandler<GetAuthorsByFiltersRequest, UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>>
    {
    }
}
