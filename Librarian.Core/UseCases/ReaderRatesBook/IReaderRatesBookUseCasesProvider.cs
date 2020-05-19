using Librarian.Core.UseCases.ReaderRatesBook.AddRate;
using Librarian.Core.UseCases.ReaderRatesBook.GetRates;

namespace Librarian.Core.UseCases.ReaderRatesBook
{
    public interface IReaderRatesBookUseCasesProvider
    {
        IGetRatesUseCase GetRates { get; set; }
        IAddRateUseCase AddRate { get; set; }
    }
}
