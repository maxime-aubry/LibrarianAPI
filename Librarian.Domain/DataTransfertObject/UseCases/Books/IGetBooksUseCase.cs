using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IGetBooksUseCase : IUseCaseRequestHandler<GetBooksRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Book>>>
    {
    }
}
