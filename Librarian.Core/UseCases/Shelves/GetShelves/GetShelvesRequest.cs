using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Shelves.GetShelves
{
    public class GetShelvesRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Shelf>>>
    {
        public GetShelvesRequest()
        {
        }
    }
}
