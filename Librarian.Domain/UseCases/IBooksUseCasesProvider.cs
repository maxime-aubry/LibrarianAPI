using Librarian.Core.DataTransfertObject.UseCases.Books;

namespace Librarian.Core.UseCases
{
    public interface IBooksUseCasesProvider
    {
        IGetBookByIdUseCase GetById { get; set; }
        IGetBooksUseCase GetList { get; set; }
        ICreateBookUseCase Create { get; set; }
        IUpdateBookUseCase Update { get; set; }
        IDeleteBookUseCase Delete { get; set; }
        IGetBooksByFiltersUseCase GetBooksByFilters { get; set; }
    }
}
