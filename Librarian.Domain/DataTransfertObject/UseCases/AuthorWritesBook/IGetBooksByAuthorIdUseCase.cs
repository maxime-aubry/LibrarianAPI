using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public interface IGetBooksByAuthorIdUseCase : IUseCaseRequestHandler<GetBooksByAuthorIdRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>>
    {
    }
}
