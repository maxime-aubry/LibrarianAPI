using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public interface IGetShelvesUseCase : IUseCaseRequestHandler<GetShelvesRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>>
    {
    }
}
