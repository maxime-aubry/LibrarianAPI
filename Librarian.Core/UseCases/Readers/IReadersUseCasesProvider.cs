using Librarian.Core.UseCases.Readers.CreateReader;
using Librarian.Core.UseCases.Readers.DeleteReader;
using Librarian.Core.UseCases.Readers.GetReaderById;
using Librarian.Core.UseCases.Readers.GetReaders;
using Librarian.Core.UseCases.Readers.UpdateReader;

namespace Librarian.Core.UseCases.Readers
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
