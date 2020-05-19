using Librarian.Core.UseCases.AuthorWritesBook.AddAuthor;
using Librarian.Core.UseCases.AuthorWritesBook.DeleteAuthor;
using Librarian.Core.UseCases.AuthorWritesBook.GetAuthorsByBookId;
using Librarian.Core.UseCases.AuthorWritesBook.GetBooksByAuthorId;

namespace Librarian.Core.UseCases.AuthorWritesBook
{
    public interface IAuthorWritesBookUseCasesProvider
    {
        IGetBooksByAuthorIdUseCase GetBooks { get; set; }
        IGetAuthorsByBookIdUseCase GetAuthors { get; set; }
        IAddAuthorUseCase AddAuthor { get; set; }
        IDeleteAuthorUseCase DeleteAuthor { get; set; }
    }
}
