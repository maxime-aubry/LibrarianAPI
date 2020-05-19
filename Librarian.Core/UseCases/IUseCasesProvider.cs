using Librarian.Core.UseCases.Authors;
using Librarian.Core.UseCases.AuthorWritesBook;
using Librarian.Core.UseCases.Books;
using Librarian.Core.UseCases.ReaderLoansBook;
using Librarian.Core.UseCases.ReaderRatesBook;
using Librarian.Core.UseCases.Readers;
using Librarian.Core.UseCases.Shelves;
using Librarian.Core.UseCases.UserHasRight;
using Librarian.Core.UseCases.Users;

namespace Librarian.Core.UseCases
{
    public interface IUseCasesProvider
    {
        IAuthorsUseCasesProvider Authors { get; set; }
        IAuthorWritesBookUseCasesProvider BooksOfAuthors { get; set; }
        IBooksUseCasesProvider Books { get; set; }
        IReadersUseCasesProvider Readers { get; set; }
        IReaderLoansBookUseCasesProvider ReadersLoans { get; set; }
        IReaderRatesBookUseCasesProvider ReadersRates { get; set; }
        IShelvesUseCasesProvider Shelves { get; set; }
        IUserHasRightUseCasesProvider UserRights { get; set; }
        IUsersUseCasesProvider Users { get; set; }
    }
}
