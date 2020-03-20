using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;

namespace Librarian.Core.UseCases
{
    public interface IAuthorWritesBookUseCasesProvider
    {
        IGetBooksByAuthorIdUseCase GetBooks { get; set; }
        IGetAuthorsByBookIdUseCase GetAuthors { get; set; }
        IAddAuthorsUseCase AddAuthors { get; set; }
        IDeleteAuthorsUseCase DeleteAuthors { get; set; }
    }
}
