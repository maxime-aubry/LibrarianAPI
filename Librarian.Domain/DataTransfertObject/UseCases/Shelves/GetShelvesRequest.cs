using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Shelves
{
    public class GetShelvesRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Shelf>>>
    {
        public GetShelvesRequest()
        {
        }
    }
}
