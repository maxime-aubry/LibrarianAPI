using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Books.GetBooksByFilters
{
    public interface IGetBooksByFiltersUseCase : IUseCaseRequestHandler<GetBooksByFiltersRequest, UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>>
    {
    }
}
