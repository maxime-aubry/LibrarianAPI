using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Shelves.GetAvailableShelves
{
    public interface IGetAvailableShelvesUseCase : IUseCaseRequestHandler<GetAvailableShelvesRequest, UseCaseResponseMessage<IEnumerable<Shelf>>>
    {
    }
}
