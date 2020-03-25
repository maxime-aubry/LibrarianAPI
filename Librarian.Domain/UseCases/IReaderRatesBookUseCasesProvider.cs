using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;

namespace Librarian.Core.UseCases
{
    public interface IReaderRatesBookUseCasesProvider
    {
        IGetRatesUseCase GetRates { get; set; }
        IAddRateUseCase AddRate { get; set; }
    }
}
