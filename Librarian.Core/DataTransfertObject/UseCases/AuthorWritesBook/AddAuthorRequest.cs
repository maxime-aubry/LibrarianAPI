namespace Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook
{
    public class AddAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public AddAuthorRequest(string bookId, string authorId)
        {
            this.BookId = bookId;
            this.AuthorId = authorId;
        }

        public string BookId { get; set; }
        public string AuthorId { get; set; }
    }
}
