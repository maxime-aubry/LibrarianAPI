using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public interface IAddAuthorsUseCase : IUseCaseRequestHandler<AddAuthorsRequest, UseCaseResponseMessage<IEnumerable<string>>>
    {
    }
}
