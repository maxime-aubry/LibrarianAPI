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
        IUsersUseCasesProvider Users { get; set; }
    }
}
