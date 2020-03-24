using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public interface IGetReadersUseCase : IUseCaseRequestHandler<GetReadersRequest, UseCaseResponseMessage<IEnumerable<Reader>>>
    {
    }
}
