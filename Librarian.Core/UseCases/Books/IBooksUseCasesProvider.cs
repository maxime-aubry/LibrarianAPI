using Librarian.Core.UseCases.Books.AddCopies;
using Librarian.Core.UseCases.Books.CreateBook;
using Librarian.Core.UseCases.Books.DeleteBook;
using Librarian.Core.UseCases.Books.GetBookById;
using Librarian.Core.UseCases.Books.GetBooks;
using Librarian.Core.UseCases.Books.GetBooksByFilters;
using Librarian.Core.UseCases.Books.RemoveCopies;
using Librarian.Core.UseCases.Books.UpdateBook;

namespace Librarian.Core.UseCases.Books
{
    public interface IBooksUseCasesProvider
    {
        IGetBookByIdUseCase GetById { get; set; }
        IGetBooksUseCase GetList { get; set; }
        ICreateBookUseCase Create { get; set; }
        IUpdateBookUseCase Update { get; set; }
        IDeleteBookUseCase Delete { get; set; }
        IGetBooksByFiltersUseCase GetBooksByFilters { get; set; }
        IAddCopiesUseCase AddCopiesUseCase { get; set; }
        IRemoveCopiesUseCase ReduceCopiesUseCase { get; set; }
    }
}
