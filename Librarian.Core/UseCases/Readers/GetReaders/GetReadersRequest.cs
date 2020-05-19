using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Readers.GetReaders
{
    public class GetReadersRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<Reader>>>
    {
        public GetReadersRequest()
        {
        }
    }
}
