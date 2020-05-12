using Librarian.Core.DataTransfertObject.UseCases.Readers;

namespace Librarian.Core.UseCases
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
