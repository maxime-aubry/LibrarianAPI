
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;

namespace Librarian.Core.UseCases
{
    public class ReaderRatesBookUseCasesProvider : IReaderRatesBookUseCasesProvider
    {
        public ReaderRatesBookUseCasesProvider(
            IAddRateUseCase addRate
        )
        {
            this.AddRate = addRate;
        }

        public IAddRateUseCase AddRate { get; set; }
    }
}
