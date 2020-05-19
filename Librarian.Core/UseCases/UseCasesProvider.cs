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
    public class UseCasesProvider : IUseCasesProvider
    {
        public UseCasesProvider(
            IAuthorsUseCasesProvider authors,
            IAuthorWritesBookUseCasesProvider booksOfAuthors,
            IBooksUseCasesProvider books,
            IReadersUseCasesProvider readers,
            IReaderLoansBookUseCasesProvider readersLoans,
            IReaderRatesBookUseCasesProvider readersRates,
            IShelvesUseCasesProvider shelves,
            IUserHasRightUseCasesProvider userRights,
            IUsersUseCasesProvider users
        )
        {
            this.Authors = authors;
            this.BooksOfAuthors = booksOfAuthors;
            this.Books = books;
            this.Readers = readers;
            this.ReadersLoans = readersLoans;
            this.ReadersRates = readersRates;
            this.Shelves = shelves;
            this.UserRights = userRights;
            this.Users = users;
        }

        public IAuthorsUseCasesProvider Authors { get; set; }
        public IAuthorWritesBookUseCasesProvider BooksOfAuthors { get; set; }
        public IBooksUseCasesProvider Books { get; set; }
        public IReadersUseCasesProvider Readers { get; set; }
        public IReaderLoansBookUseCasesProvider ReadersLoans { get; set; }
        public IReaderRatesBookUseCasesProvider ReadersRates { get; set; }
        public IShelvesUseCasesProvider Shelves { get; set; }
        public IUserHasRightUseCasesProvider UserRights { get; set; }
        public IUsersUseCasesProvider Users { get; set; }
    }
}
