using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Books
{
    public interface IGetBooksUseCase : IUseCaseRequestHandler<GetBooksRequest, UseCaseResponseMessage<IEnumerable<Book>>>
    {
    }
}
