using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class GetShelvesRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Shelf>>>
    {
        public GetShelvesRequest()
        {
        }
    }
}
