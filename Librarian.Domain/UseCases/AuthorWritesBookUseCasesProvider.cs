using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;

namespace Librarian.Core.UseCases
{
    public class AuthorWritesBookUseCasesProvider : IAuthorWritesBookUseCasesProvider
    {
        public AuthorWritesBookUseCasesProvider(
            IGetBooksByAuthorIdUseCase getBooks,
            IGetAuthorsByBookIdUseCase getAuthors,
            IAddAuthorsUseCase addAuthors,
            IDeleteAuthorsUseCase deleteAuthors)
        {
            this.GetBooks = getBooks;
            this.GetAuthors = getAuthors;
            this.AddAuthors = addAuthors;
            this.DeleteAuthors = deleteAuthors;
        }

        public IGetBooksByAuthorIdUseCase GetBooks { get; set; }
        public IGetAuthorsByBookIdUseCase GetAuthors { get; set; }
        public IAddAuthorsUseCase AddAuthors { get; set; }
        public IDeleteAuthorsUseCase DeleteAuthors { get; set; }
    }
}
