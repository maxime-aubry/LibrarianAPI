using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public interface IGetAuthorsUseCase : IUseCaseRequestHandler<GetAuthorsRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Author>>>
    {
    }
}
