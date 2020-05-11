using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IGetBooksByFiltersUseCase : IUseCaseRequestHandler<GetBooksByFiltersRequest, UseCaseResponseMessage<IEnumerable<FindBooksByFilters>>>
    {
    }
}
