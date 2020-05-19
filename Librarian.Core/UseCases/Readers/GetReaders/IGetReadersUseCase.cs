using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.Readers.GetReaders
{
    public interface IGetReadersUseCase : IUseCaseRequestHandler<GetReadersRequest, UseCaseResponseMessage<IEnumerable<Reader>>>
    {
    }
}
