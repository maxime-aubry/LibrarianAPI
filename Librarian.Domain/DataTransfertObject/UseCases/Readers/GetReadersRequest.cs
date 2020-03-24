using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class GetReadersRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Reader>>>
    {
        public GetReadersRequest()
        {
        }
    }
}
