using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;

namespace Librarian.Core.UseCases
{
    public class ReaderRatesBookUseCasesProvider : IReaderRatesBookUseCasesProvider
    {
        public ReaderRatesBookUseCasesProvider(
            IGetRatesUseCase getRates,
            IAddRateUseCase addRate
        )
        {
            this.GetRates = getRates;
            this.AddRate = addRate;
        }

        public IGetRatesUseCase GetRates { get; set; }
        public IAddRateUseCase AddRate { get; set; }
    }
}
