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
    public class BooksUseCasesProvider : IBooksUseCasesProvider
    {
        public BooksUseCasesProvider(
            IGetBookByIdUseCase getById,
            IGetBooksUseCase getList,
            ICreateBookUseCase create,
            IUpdateBookUseCase update,
            IDeleteBookUseCase delete,
            IGetBooksByFiltersUseCase getBooksByFilters,
            IAddCopiesUseCase addCopiesUseCase,
            IRemoveCopiesUseCase reduceCopiesUseCase
        )
        {
            this.GetById = getById;
            this.GetList = getList;
            this.Create = create;
            this.Update = update;
            this.Delete = delete;
            this.GetBooksByFilters = getBooksByFilters;
            this.AddCopiesUseCase = addCopiesUseCase;
            this.ReduceCopiesUseCase = reduceCopiesUseCase;
        }

        public IGetBookByIdUseCase GetById { get; set; }

        public IGetBooksUseCase GetList { get; set; }

        public ICreateBookUseCase Create { get; set; }

        public IUpdateBookUseCase Update { get; set; }

        public IDeleteBookUseCase Delete { get; set; }
        public IGetBooksByFiltersUseCase GetBooksByFilters { get; set; }
        public IAddCopiesUseCase AddCopiesUseCase { get; set; }
        public IRemoveCopiesUseCase ReduceCopiesUseCase { get; set; }
    }
}
