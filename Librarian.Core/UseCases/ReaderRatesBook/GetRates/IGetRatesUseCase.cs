using HexagonalArchitecture.Core.DataTransfertObject;
using System.Collections.Generic;

namespace Librarian.Core.UseCases.ReaderRatesBook.GetRates
{
    public interface IGetRatesUseCase : IUseCaseRequestHandler<GetRatesRequest, UseCaseResponseMessage<IEnumerable<Librarian.Core.Domain.Entities.ReaderRatesBook>>>
    {
    }
}
