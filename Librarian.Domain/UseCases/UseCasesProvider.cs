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
            this.Users = users;
        }

        public IAuthorsUseCasesProvider Authors { get; set; }
        public IAuthorWritesBookUseCasesProvider BooksOfAuthors { get; set; }
        public IBooksUseCasesProvider Books { get; set; }
        public IReadersUseCasesProvider Readers { get; set; }
        public IReaderLoansBookUseCasesProvider ReadersLoans { get; set; }
        public IReaderRatesBookUseCasesProvider ReadersRates { get; set; }
        public IShelvesUseCasesProvider Shelves { get; set; }
        public IUsersUseCasesProvider Users { get; set; }
    }
}
