using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.ReaderRatesBook.AddRate
{
    public interface IAddRateUseCase : IUseCaseRequestHandler<AddRateRequest, UseCaseResponseMessage<string>>
    {
    }
}
