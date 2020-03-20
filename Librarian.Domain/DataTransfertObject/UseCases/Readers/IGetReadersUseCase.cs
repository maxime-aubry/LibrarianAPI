using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public interface IGetReadersUseCase : IUseCaseRequestHandler<GetReadersRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.Reader>>>
    {
    }
}
