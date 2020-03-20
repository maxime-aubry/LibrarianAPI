using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public interface IGetAuthorsByBookIdUseCase : IUseCaseRequestHandler<GetAuthorsByBookIdRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>>
    {
    }
}
