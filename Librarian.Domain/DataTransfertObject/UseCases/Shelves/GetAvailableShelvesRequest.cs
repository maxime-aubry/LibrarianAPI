using Librarian.Core.Domain.Enums;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class GetAvailableShelvesRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>>
    {
        public GetAvailableShelvesRequest(EBookCategory category, int numberOfCopies)
        {
            this.Category = category;
            this.NumberOfCopies = numberOfCopies;
        }

        public EBookCategory Category { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
