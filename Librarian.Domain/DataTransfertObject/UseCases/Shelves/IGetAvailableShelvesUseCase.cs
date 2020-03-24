using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public interface IGetAvailableShelvesUseCase : IUseCaseRequestHandler<GetAvailableShelvesRequest, UseCaseResponseMessage<IEnumerable<Shelf>>>
    {
    }
}
