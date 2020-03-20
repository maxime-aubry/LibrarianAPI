using Librarian.Core.DataTransfertObject.UseCases.Readers;

namespace Librarian.Core.UseCases
{
    public interface IReadersUseCasesProvider
    {
        IGetReaderByIdUseCase GetById { get; set; }
        IGetReadersUseCase GetList { get; set; }
        ICreateReaderUseCase Create { get; set; }
        IUpdateReaderUseCase Update { get; set; }
        IDeleteReaderUseCase Delete { get; set; }
    }
}
