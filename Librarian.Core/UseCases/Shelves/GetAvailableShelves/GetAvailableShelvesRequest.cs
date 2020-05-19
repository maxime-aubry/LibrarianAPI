using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using Librarian.Core.Domain.Enums;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Shelves.GetAvailableShelves
{
    public class GetAvailableShelvesRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Shelf>>>
    {
        public GetAvailableShelvesRequest(EBookCategory category)
        {
            this.Category = category;
        }

        public EBookCategory Category { get; set; }
    }
}
