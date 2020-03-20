using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;

namespace Librarian.Core.UseCases
{
    public interface IReaderRatesBookUseCasesProvider
    {
        IAddRateUseCase AddRate { get; set; }
    }
}
