using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public interface IDeleteAuthorsUseCase : IUseCaseRequestHandler<DeleteAuthorsRequest, UseCaseResponseMessage<string>>
    {
    }
}
