using Librarian.Core.UseCases.Readers.CreateReader;
using Librarian.Core.UseCases.Readers.DeleteReader;
using Librarian.Core.UseCases.Readers.GetReaderById;
using Librarian.Core.UseCases.Readers.GetReaders;
using Librarian.Core.UseCases.Readers.UpdateReader;

namespace Librarian.Core.UseCases.Readers
{
    public class ReadersUseCasesProvider : IReadersUseCasesProvider
    {
        public ReadersUseCasesProvider(
            IGetReaderByIdUseCase getById,
            IGetReadersUseCase getList,
            ICreateReaderUseCase create,
            IUpdateReaderUseCase update,
            IDeleteReaderUseCase delete
        )
        {
            this.GetById = getById;
            this.GetList = getList;
            this.Create = create;
            this.Update = update;
            this.Delete = delete;
        }

        public IGetReaderByIdUseCase GetById { get; set; }

        public IGetReadersUseCase GetList { get; set; }

        public ICreateReaderUseCase Create { get; set; }

        public IUpdateReaderUseCase Update { get; set; }

        public IDeleteReaderUseCase Delete { get; set; }
    }
}
