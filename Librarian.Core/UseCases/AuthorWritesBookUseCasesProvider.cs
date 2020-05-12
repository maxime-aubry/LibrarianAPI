using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;

namespace Librarian.Core.UseCases
{
    public class AuthorWritesBookUseCasesProvider : IAuthorWritesBookUseCasesProvider
    {
        public AuthorWritesBookUseCasesProvider(
            IGetBooksByAuthorIdUseCase getBooks,
            IGetAuthorsByBookIdUseCase getAuthors,
            IAddAuthorUseCase addAuthors,
            IDeleteAuthorUseCase deleteAuthors)
        {
            this.GetBooks = getBooks;
            this.GetAuthors = getAuthors;
            this.AddAuthor = addAuthors;
            this.DeleteAuthor = deleteAuthors;
        }

        public IGetBooksByAuthorIdUseCase GetBooks { get; set; }
        public IGetAuthorsByBookIdUseCase GetAuthors { get; set; }
        public IAddAuthorUseCase AddAuthor { get; set; }
        public IDeleteAuthorUseCase DeleteAuthor { get; set; }
    }
}
