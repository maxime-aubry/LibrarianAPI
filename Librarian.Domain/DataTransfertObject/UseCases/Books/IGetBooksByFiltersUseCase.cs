using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IGetBooksByFiltersUseCase : IUseCaseRequestHandler<GetBooksByFiltersRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.FindBooksByFilters>>>
    {
    }
}
