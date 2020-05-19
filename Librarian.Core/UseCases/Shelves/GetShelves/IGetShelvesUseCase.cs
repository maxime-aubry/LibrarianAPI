using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Shelves.GetShelves
{
    public interface IGetShelvesUseCase : IUseCaseRequestHandler<GetShelvesRequest, UseCaseResponseMessage<IEnumerable<Shelf>>>
    {
    }
}
