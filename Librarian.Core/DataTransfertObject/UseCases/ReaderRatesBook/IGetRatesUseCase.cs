using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook
{
    public interface IGetRatesUseCase : IUseCaseRequestHandler<GetRatesRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>>
    {
    }
}
