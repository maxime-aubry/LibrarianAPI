using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class GetReadersRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Reader>>>
    {
        public GetReadersRequest()
        {
        }
    }
}
