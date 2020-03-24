using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;

namespace Librarian.Core.UseCases
{
    public interface IAuthorWritesBookUseCasesProvider
    {
        IGetBooksByAuthorIdUseCase GetBooks { get; set; }
        IGetAuthorsByBookIdUseCase GetAuthors { get; set; }
        IAddAuthorUseCase AddAuthor { get; set; }
        IDeleteAuthorUseCase DeleteAuthor { get; set; }
    }
}
